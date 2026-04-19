namespace Domain.Entities
{
    public class Cities
    {
        public int? CityId { get; set; }
        public string CityName { get; set; } 
        public int StateId { get; set; }
        public bool IsActive { get; set; }
    }
}
