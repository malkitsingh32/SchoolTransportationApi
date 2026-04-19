using Microsoft.AspNetCore.Http;

namespace DTO.Request.Students
{
    public class UpdateFundedDetailsFromExcelRequestDto
    {
        public IFormFile File { get; set; }
        public int Month { get; set; }
        public int DistrictId { get; set; }
    }
}
