namespace DTO.Response.SystemValues
{
    public class GetChargeStructureResponseDto
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? NtId { get; set; }
        public string NtName { get; set; }
        public bool IsFunded { get; set; }
        public decimal Price { get; set; }
        public int? SchoolId { get; set; }
        public string SchoolName { get; set; }

    }
}
