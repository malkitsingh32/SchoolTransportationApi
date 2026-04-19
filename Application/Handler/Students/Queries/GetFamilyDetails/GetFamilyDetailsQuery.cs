using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetFamilyDetails
{
    public class GetFamilyDetailsQuery : IRequest<CommonResultResponseDto<IList<GetFamilyDetailsResponseDto>>>
    {
        public string Phone { get; set; }
        public string? Address { get; set; }
    }
}
