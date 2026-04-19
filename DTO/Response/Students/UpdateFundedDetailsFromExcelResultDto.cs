namespace DTO.Response.Students
{
    public class UpdateFundedDetailsFromExcelResultDto
    {
        public List<UpdateFundedDetailsFromExcelResponseDto> Rows { get; set; } = new();
        public byte[] FileBytes { get; set; } = Array.Empty<byte>();
        public string FileName { get; set; } = string.Empty;
    }
}
