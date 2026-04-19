namespace Domain.Entities
{
    public class BusDetails
    {
        public int? BusDetailID { get; set; }  
        public string BusType { get; set; }
        public string BusName { get; set; }
        public string Station { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string BusNumber { get; set; }
        public string YearOfManufacture { get; set; }
        public int? DriverID { get; set; }
        public string RouteId { get; set; }
    }
} 
