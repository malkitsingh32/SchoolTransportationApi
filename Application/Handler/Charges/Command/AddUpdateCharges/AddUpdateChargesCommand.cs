using DTO.Response;
using MediatR;

namespace Application.Handler.Charges.Command.AddUpdateCharges
{
    public class AddUpdateChargesCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int ChargeId { get; set; }
        public int StudentId { get; set; }
        public decimal ChargeAmount { get; set; }
        public DateTime ChargeDate { get; set; }
        public string ChargeStatus { get; set; }
        public int UserId { get; set; }
        public bool? IsDelete { get; set; }
    }
}
