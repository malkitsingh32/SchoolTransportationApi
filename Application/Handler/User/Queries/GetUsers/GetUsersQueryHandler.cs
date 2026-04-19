using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.User;
using MediatR;

namespace Application.Handler.User.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, CommonResultResponseDto<PaginatedList<GetUsersResponseDto>>>
    {
        private readonly IUserService _userService;
        private readonly IRequestBuilder _requestBuilder;

        public GetUsersQueryHandler(IUserService userService, IRequestBuilder requestBuilder)
        {
            _userService = userService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetUsersResponseDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _userService.GetUsers(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}
