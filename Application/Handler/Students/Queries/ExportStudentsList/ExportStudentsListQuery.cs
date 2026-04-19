using MediatR;

namespace Application.Handler.Students.Queries.ExportStudentsList
{
    public class ExportStudentsListQuery : IRequest<ExportFileResult>
    {
        public string? NtId { get; set; }
        public string? Dob { get; set; }
        public string? District { get; set; }
        public string? SchoolStudentId { get; set; }
        public string? SchoolId { get; set; }
        public string? Grade { get; set; }
        public string? Gender { get; set; }
        public string? SearchText { get; set; }
    }
}
