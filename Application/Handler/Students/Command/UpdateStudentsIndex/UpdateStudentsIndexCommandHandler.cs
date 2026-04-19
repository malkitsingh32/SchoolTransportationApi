using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.UpdateStudentsIndex
{
    public class UpdateStudentsIndexCommandHandler : IRequestHandler<UpdateStudentsIndexCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;
        public UpdateStudentsIndexCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(UpdateStudentsIndexCommand updateStudentsIndexCommand, CancellationToken cancellationToken)
        {
            var user = await _studentsService.UpdateStudentsIndex(updateStudentsIndexCommand.Adapt<UpdateStudentsIndexRquestDto>());
            return await Task.FromResult(user);
        }
    }
}
