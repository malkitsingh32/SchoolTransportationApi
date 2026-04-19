namespace DTO.Request.Admin
{
    public class AreasKMLRequestDto
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public int UserId { get; set; }
        public decimal? CenterLatitude { get; set; }
        public decimal? CenterLongitude { get; set; }
    }
}
