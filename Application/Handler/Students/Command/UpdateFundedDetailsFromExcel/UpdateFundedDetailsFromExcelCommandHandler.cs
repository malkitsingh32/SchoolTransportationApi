using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command.UpdateFundedDetailsFromExcel
{
    public class UpdateFundedDetailsFromExcelCommandHandler : IRequestHandler<UpdateFundedDetailsFromExcelCommand, CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>>
    {
        private readonly IStudentsService _studentsService;

        public UpdateFundedDetailsFromExcelCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>> Handle(UpdateFundedDetailsFromExcelCommand request, CancellationToken cancellationToken)
        {
            var dto = new UpdateFundedDetailsFromExcelRequestDto
            {
                File = request.File,
                Month = request.Month,
                DistrictId = request.DistrictId
            };

            return await _studentsService.UpdateFundedDetailsFromExcel(dto);
        }
    }
}
