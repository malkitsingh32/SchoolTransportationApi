using Microsoft.AspNetCore.Http;

namespace DTO.Request.Admin
{
    public class ImportKmlRequestDto
    {
        public IFormFile File { get; set; }
        public string KmlType { get; set; }
        public int AreaId { get; set; }
        public int UserId { get; set; }

    }
}
