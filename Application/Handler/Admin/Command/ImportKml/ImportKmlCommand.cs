using DTO.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Handler.Admin.Command.ImportKml
{
    public class ImportKmlCommand : IRequest<CommonResultResponseDto<string>>
    {
        public IFormFile File { get; set; }
        public string KmlType { get; set; }
        public int UserId { get; set; }
    }
}
