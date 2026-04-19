using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Routes.Queries.GetAreasBySchoolAndGrade
{
    public class GetAreasBySchoolAndGradeQuery : IRequest<CommonResultResponseDto<IList<GetAreaListResponseDto>>>
    {
        public string? School { get; set; }
        public string? Grade { get; set; }
        public string? Building { get; set; }
        public string? Branch { get; set; }
        public int? Gender { get; set; }
    }
}
