namespace DTO.Request.Admin
{
    public class AddSearchLocationRequestDto
    {
        public int Id { get; set; }
        public string CurrentLocation { get; set; }
        public string CurrentLocationLongLat { get; set; }
        public int UserId { get; set; }
    }
}
