using DTO.Request.Admin;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddBusType
{
    public class AddBusTypeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string BusType { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsRequired { get; set; }
        public bool ExclusivelyPay { get; set; }
        public RouteTypeRequiredRulesDto RulesPayload { get; set; }
        public string Days { get; set; }

    }
}
