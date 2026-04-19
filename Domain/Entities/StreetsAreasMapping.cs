namespace Domain.Entities
{
    public class StreetsAreasMapping
    {
        public int? Id { get; set; }              
        public string? StreetName { get; set; }     
        public int? AreaId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
