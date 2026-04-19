namespace DTO.Response.SystemValues
{
    public class GetBusTypeResponseDto
    {
        public int Id { get; set; }
        public string RouteType { get; set; }
        public string GenderNames { get; set; }
        public string GenderIds { get; set; }
        public decimal Amount { get; set; }
        public bool IsRequired { get; set; }
        public bool ExclusivelyPay { get; set; }
        public string SchoolNames { get; set; }
        public string SchoolIds { get; set; }
        public string GradeNames { get; set; }
        public string GradeIds { get; set; }
        public bool HasRequiredRules { get; set; }
        public string Days { get; set; }
    }
}
