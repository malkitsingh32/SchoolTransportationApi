namespace DTO.Request.Admin
{
    public class AddBusTypeRequestDto
    {
        public int Id { get; set; }
        public string BusType { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public bool? IsRequired { get; set; }
        public bool? ExclusivelyPay { get; set; }
        public RouteTypeRequiredRulesDto RulesPayload { get; set; }
        public string Days { get; set; }
    }
}
