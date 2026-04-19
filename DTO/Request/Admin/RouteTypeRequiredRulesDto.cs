namespace DTO.Request.Admin
{
    public class RouteTypeRequiredRulesDto
    {
        public int RouteTypeId { get; set; }
        public List<RouteTypeRule> Rules { get; set; } = new List<RouteTypeRule>();
        public int CreatedBy { get; set; }
    }

    public class RouteTypeRule
    {
        public int SchoolId { get; set; }
        public int GenderId { get; set; }
        public int GradeId { get; set; }
    }
}
