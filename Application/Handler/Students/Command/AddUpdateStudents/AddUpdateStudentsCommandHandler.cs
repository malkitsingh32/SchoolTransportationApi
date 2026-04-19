using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.AddUpdateStudents
{
    public class AddUpdateStudentsCommandHandler : IRequestHandler<AddUpdateStudentsCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;
        public AddUpdateStudentsCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddUpdateStudentsCommand addUpdateStudentsCommand, CancellationToken cancellationToken)
        {
            var user = await _studentsService.AddUpdateStudents(addUpdateStudentsCommand.Adapt<AddUpdateStudentsDto>());
            return await Task.FromResult(user);
        }
    }
}

