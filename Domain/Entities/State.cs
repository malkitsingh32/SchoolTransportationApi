namespace Domain.Entities
{
    public class State
    {
        public int StateId { get; set; }
        public string? StateName { get; set; } 
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
    }
}
