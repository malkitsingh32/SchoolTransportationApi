using DTO.Response;
using DTO.Response.Students;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Handler.Students.Command.UpdateFundedDetailsFromExcel
{
    public class UpdateFundedDetailsFromExcelCommand : IRequest<CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>>
    {
        public IFormFile File { get; set; }
        public int Month { get; set; }
        public int DistrictId { get; set; }
    }
}
