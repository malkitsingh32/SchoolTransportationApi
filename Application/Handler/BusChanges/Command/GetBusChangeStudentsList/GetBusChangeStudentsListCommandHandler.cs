using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using Application.Handler.Students.Command;
using DTO.Response.Students;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.BusChanges.Command.GetBusChangeStudentsList
{
    internal class GetBusChangeStudentsListCommandHandler : IRequestHandler<GetBusChangeStudentsListCommand, CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>>
    {
        private readonly IBusChangesService _busChangesService;
        private readonly IRequestBuilder _requestBuilder;
        public GetBusChangeStudentsListCommandHandler(IBusChangesService busChangesService, IRequestBuilder requestBuilder)
        {
            _busChangesService = busChangesService;
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


        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> Handle(GetBusChangeStudentsListCommand request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _busChangesService.GetBusChangeStudents(filterModel.GetFilters(), request.CommonRequest, request.RouteId, filterModel.GetSorts(), request.StreetId, request.FamilyId, request.NtId,
                request.Dob, request.District, request.SchoolStudentId, request.SchoolId, request.Grade, request.Gender);
        }
    }
}

