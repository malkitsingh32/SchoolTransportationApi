using DTO.Response;
using MediatR;

namespace Application.Handler.BusChanges.Command.AddUpdateBusChanges
{
    public class AddUpdateBusChangesCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int? Id { get; set; }
        public int? StudentId { get; set; }
        public int? FamilyId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentOriginalAddress { get; set; }
        public string? BusChangeAddress { get; set; }
        public string? Area { get; set; }
        public int? SchoolId { get; set; }
        public int? Gender { get; set; }
        public int? Grade { get; set; }
        public int? BranchId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? StartFrom { get; set; }
        public string? Include { get; set; }
        public string? BusChangePhone { get; set; }
        public string? MotherCell { get; set; }
        public string? RouteId { get; set; }
        public string? Payment { get; set; }
        public string? ProcessPaymentMethod { get; set; }
        public bool isSave { get; set; }
        public string? PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string? CustomerId { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVV { get; set; }
        public string? CardHolderName { get; set; }
    }
}
