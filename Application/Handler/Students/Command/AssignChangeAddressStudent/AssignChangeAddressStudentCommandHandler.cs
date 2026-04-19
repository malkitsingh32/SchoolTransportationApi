using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.AssignChangeAddressStudent
{
    public class AssignChangeAddressStudentCommandHandler : IRequestHandler<AssignChangeAddressStudentCommand, CommonResultResponseDto<IList<AssignChangeAddressStudentResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        public AssignChangeAddressStudentCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<IList<AssignChangeAddressStudentResponseDto>>> Handle(AssignChangeAddressStudentCommand assignChangeAddressStudentCommand, CancellationToken cancellationToken)
        {
            return await _studentsService.AssignChangeAddressStudent(assignChangeAddressStudentCommand.Adapt<AssignChangeAddressStudentRequestDto>());
        }
    }
}
