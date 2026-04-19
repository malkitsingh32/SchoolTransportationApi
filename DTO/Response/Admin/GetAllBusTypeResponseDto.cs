namespace DTO.Response.Admin
{
    public class GetAllBusTypeResponseDto
    {
        public int Id { get; set; }
        public string RouteType { get; set; }
        public decimal Amount { get; set; }
        public bool IsRequired { get; set; }
        public string Days { get; set; }
    }
}
