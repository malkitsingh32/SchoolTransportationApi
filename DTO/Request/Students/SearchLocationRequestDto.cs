namespace DTO.Request.Students
{
    public class SearchLocationRequestDto
    {
        public string? Area { get; set; }
        public string? School { get; set; }
        public string? Grade { get; set; }
        public int? Gender { get; set; }
        public string? Building { get; set; }
        public string? Branch { get; set; }
        public string? Street { get; set; }
        public string? UniqueId { get; set; }
        public string? Filter { get; set; }
        public int UserId { get; set; }
        public int? SearchCount { get; set; }
    }
}
