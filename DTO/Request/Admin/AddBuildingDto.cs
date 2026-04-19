namespace DTO.Request.Admin
{
    public class AddBuildingDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int SchoolId { get; set; }
        public int UserId { get; set; }
        public string? BuildingName { get; set; }
    }
}
