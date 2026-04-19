
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command.GetUnassignedStudents
{
    public class GetUnassignedStudentsCommandHandler : IRequestHandler<GetUnassignedStudentsCommand, CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        private readonly IRequestBuilder _requestBuilder;
        public GetUnassignedStudentsCommandHandler(IStudentsService studentsService, IRequestBuilder requestBuilder)
        {
            _studentsService = studentsService;
            _requestBuilder = requestBuilder;
        }

        public class GetStudentsRequestDto
        {
            public ServerRowsRequest CommonRequest { get; set; }
            public int RouteId { get; set; }
            public int streetId { get; set; }
            public int FamilyId { get; set; }

        }


        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> Handle(GetUnassignedStudentsCommand request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _studentsService.GetUnassignedStudents(filterModel.GetFilters(), request.CommonRequest, request.RouteId, filterModel.GetSorts(), request.StreetId, request.FamilyId, request.RouteTypeIds, request.GenderId);
        }
    }
}
