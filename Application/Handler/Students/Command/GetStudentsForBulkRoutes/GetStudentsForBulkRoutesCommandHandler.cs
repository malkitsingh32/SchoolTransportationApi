using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command.GetStudentsForBulkRoutes
{
    public class GetStudentsForBulkRoutesCommandHandler : IRequestHandler<GetStudentsForBulkRoutesCommand, CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        private readonly IRequestBuilder _requestBuilder;
        public GetStudentsForBulkRoutesCommandHandler(IStudentsService studentsService, IRequestBuilder requestBuilder)
        {
            _studentsService = studentsService;
            _requestBuilder = requestBuilder;
        }

        public class GetStudentsForBulkRouteRequestDto
        {
            public ServerRowsRequest CommonRequest { get; set; }
            public string? Area { get; set; }
            public string? School { get; set; }
            public string? Grade { get; set; }
            public int? Gender { get; set; }
            public string? Building { get; set; }
            public string? Branch { get; set; }
            public string? Street { get; set; }
            public string? UniqueId { get; set; }
            public int UserId { get; set; }
            public int? RouteId { get; set; }

        }


        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> Handle(GetStudentsForBulkRoutesCommand request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _studentsService.GetStudentsForBulkRoute(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.Area, request.School, request.Grade, request.Gender, request.Building, request.Branch,request.Street, request.UserId,request.UniqueId,request.RouteId);
        }
    }
}
