namespace Domain.Entities
{
    public class Country
    {
        public int? CountryId { get; set; }
        public string CountryCode { get; set; } 
        public string CountryName { get; set; } 
        public int PhoneCode { get; set; }
        public bool IsActive { get; set; }
    }
}
