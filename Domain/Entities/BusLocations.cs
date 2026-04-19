namespace Domain.Entities
{
    public class BusLocations
    {
        public int? Id { get; set; }  
        public string BusId { get; set; }
        public string BusName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Time { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string FormattedLocation { get; set; }
    }
}
