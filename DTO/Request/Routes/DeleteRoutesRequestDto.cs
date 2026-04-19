namespace DTO.Request.Routes
{
    public class DeleteRoutesRequestDto
    {
        public int Id { get; set; }
        public int? DeleteAll { get; set; }
        public DateTime? RouteDate { get; set; }
    }
}
