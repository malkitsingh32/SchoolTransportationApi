namespace DTO.Response.Admin
{
    public class GetBuildingListResponseDto
    {

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Building_Address { get; set; }
        public string Address { get; set; }
        public string BranchId { get; set; }
    }
}
