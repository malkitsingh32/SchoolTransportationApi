namespace DTO.Response.CardknoxCustomers
{
    public class CardknoxCustomersResponseDto
    {
        public string? DefaultPaymentMethodId { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string? CustomerNumber { get; set; }
        public List<object>? BasicScheduleData { get; set; }  // Replace 'object' with actual type if known
        public string? CustomerNotes { get; set; }
        public string CreatedDate { get; set; } = string.Empty;
        public int Revision { get; set; }
        public string Email { get; set; } = string.Empty;

        public string? BillFirstName { get; set; }
        public string? BillLastName { get; set; }
        public string? BillMiddleName { get; set; }
        public string? BillCompany { get; set; }
        public string? BillStreet { get; set; }
        public string? BillStreet2 { get; set; }
        public string? BillCity { get; set; }
        public string? BillState { get; set; }
        public string? BillZip { get; set; }
        public string? BillCountry { get; set; }
        public string? BillPhone { get; set; }

        public string? ShipFirstName { get; set; }
        public string? ShipMiddleName { get; set; }
        public string? ShipLastName { get; set; }
        public string? ShipCompany { get; set; }
        public string? ShipStreet { get; set; }
        public string? ShipStreet2 { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipState { get; set; }
        public string? ShipZip { get; set; }
        public string? ShipCountry { get; set; }
        public string? ShipEmail { get; set; }

        public string? CustomerCustom01 { get; set; }
        public string? CustomerCustom02 { get; set; }
        public string? CustomerCustom03 { get; set; }
    }
    public class CustomerResponseDto
    {
        public string RefNum { get; set; }
        public string Result { get; set; }
        public string Error { get; set; }
        public List<CardknoxCustomersResponseDto> Customers { get; set; }
        public string NextToken { get; set; }
    }
}
