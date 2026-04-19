namespace DTO.Response.Driver
{
    public class DeleteDriversRequestDto
    {
        public int Id { get; set; }
        public bool IsFromRoute { get; set; }
        public int? RouteId { get; set; }
    }
}
