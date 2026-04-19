using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.SystemValues.Queries.GetCities
{
    public class GetCitiesQuery : IRequest<CommonResultResponseDto<IList<GetCitiesResponseDto>>>
    {
        public int Id { get; set; }
    }
}
