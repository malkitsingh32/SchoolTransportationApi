using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.AddUpdateBulkStudentsRouteId
{
    public class AddUpdateBulkStudentsRouteIdCommandHandler : IRequestHandler<AddUpdateBulkStudentsRouteIdCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;
        public AddUpdateBulkStudentsRouteIdCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddUpdateBulkStudentsRouteIdCommand addUpdateBulkStudentsRouteIdCommand, CancellationToken cancellationToken)
        {
            var students = await _studentsService.AddUpdateBulkStudentsRouteId(addUpdateBulkStudentsRouteIdCommand.Adapt<AddUpdateBulkStudentsRouteIdDto>());
            return await Task.FromResult(students);
        }
    }
}
