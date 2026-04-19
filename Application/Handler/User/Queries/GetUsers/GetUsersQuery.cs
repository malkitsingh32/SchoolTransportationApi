using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.User;
using MediatR;

namespace Application.Handler.User.Queries.GetUsers
{
    public class GetUsersQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetUsersResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        //public int UserId { get; set; }
    }
}
