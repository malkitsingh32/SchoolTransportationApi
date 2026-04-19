using DTO.Response.Students;

namespace Application.Abstraction.Services
{
    public interface IPdfBuilderService
    {
        Task<byte[]> GeneratePrintStudentPdf(GetStudentByIdResponse model);

    }
}
