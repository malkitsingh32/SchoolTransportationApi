using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.SystemValues.Queries
{
    public class GetStatesQuery : IRequest<CommonResultResponseDto<IList<GetStatesResponseDto>>>
    {
        public int Id { get; set; }

    }
}
