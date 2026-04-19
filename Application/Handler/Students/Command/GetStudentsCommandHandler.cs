using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command
{
    public class GetStudentsCommandHandler : IRequestHandler<GetStudentsCommand, CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        private readonly IRequestBuilder _requestBuilder;
        public GetStudentsCommandHandler(IStudentsService studentsService, IRequestBuilder requestBuilder)
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
            public string? NtId { get; set; }
            public string? RouteTypeIds { get; set; }
            public int? GenderId { get; set; }
            public string? Dob { get; set; }
            public string? District { get; set; }
            public string? SchoolStudentId { get; set; }
            public string? SchoolId { get; set; }
            public string? Grade { get; set; }
            public string? Gender { get; set; }

        }


        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> Handle(GetStudentsCommand request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _studentsService.GetStudents(filterModel.GetFilters(), request.CommonRequest, request.RouteId,  filterModel.GetSorts() ,request.StreetId, request.FamilyId, request.NtId,
                request.Dob, request.District, request.SchoolStudentId, request.SchoolId, request.Grade, request.Gender);
        }
    }
}
