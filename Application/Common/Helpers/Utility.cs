using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using DTO.Response.SystemValues;
using Helper.Constant;
using Newtonsoft.Json;
using System.Web;

namespace Application.Common.Helpers
{
    public class Utility
    {
        private Task<IList<GetSearchLocationResponseDto>> _cachedMapCentersTask;
        private readonly IAdminRepository _adminRepository;
        public Utility(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<SearchAutocompleteResult> SearchAddress(string query, int? maxRecordCount = null, string neighborhoodCenterLatLong = null)
        {
            try
            {
                if (_cachedMapCentersTask == null)
                {
                    _cachedMapCentersTask = _adminRepository.GetSearchLocation();
                }
                var cachedMapCenters = await _cachedMapCentersTask;
                string mapCenter = neighborhoodCenterLatLong ?? cachedMapCenters.FirstOrDefault()?.CurrentLocationLongLat;

                //query = "meron & b";
                query = HttpUtility.UrlEncode(query);

                int radius = 500; // start with 500 meters
                int maxRadius = 50000; // 50 km max
                SearchAutocompleteResult searchLocation = null;

                HttpClient client = new HttpClient();

                while (radius <= maxRadius)
                {
                    string param = $"?input={query}&components=country:us&key={ConstantVariables.GoogleApiKey}"; //+
                    if (!string.IsNullOrEmpty(mapCenter))
                    {
                        param += $"&location={mapCenter}&radius={radius}";
                    }
                    string url = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
                    HttpResponseMessage response = await client.GetAsync(url + param);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    searchLocation = JsonConvert.DeserializeObject<SearchAutocompleteResult>(json);

                    if (searchLocation?.predictions?.Any() == true)
                    {
                        break;
                    }
                    radius *= 2;
                }
                if (searchLocation?.predictions == null || !searchLocation.predictions.Any()) return null;


                searchLocation.predictions = searchLocation.predictions
                    .Take(maxRecordCount ?? int.MaxValue)
                    .ToList();

                // Fetch lat/lng for each prediction
                foreach (var prediction in searchLocation.predictions.ToList())
                {
                    string detailsUrl = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={prediction.place_id}&key={ConstantVariables.GoogleApiKey}";
                    HttpResponseMessage detailsResponse = await client.GetAsync(detailsUrl);
                    detailsResponse.EnsureSuccessStatusCode();

                    string detailsJson = await detailsResponse.Content.ReadAsStringAsync();
                    var placeDetails = JsonConvert.DeserializeObject<AddressDetails>(detailsJson);

                    if (placeDetails?.result?.geometry?.location != null)
                    {
                        var result = placeDetails.result;

                        prediction.Latitude = result.geometry?.location?.lat;
                        prediction.Longitude = result.geometry?.location?.lng;
                        prediction.FormattedAddress = result.formatted_address;
                        prediction.Name = result.name;
                        prediction.InternationalPhoneNumber = result.international_phone_number;
                        prediction.Url = result.url;
                        prediction.TypesFromDetails = result.types;
                        prediction.AddressComponents = result.address_components;
                    }
                }
                return searchLocation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Prediction> SortPredictions(List<Prediction> predictions)
        {
            return predictions
                .OrderByDescending(p => p.matched_substrings?.Count ?? 0)
                .ToList();
        }

    }
}
