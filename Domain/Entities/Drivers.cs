namespace Domain.Entities
{
    public class Drivers
    {
        public int? DriverID { get; set; }
        public int? UserId { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? RouteId { get; set; }
        public int? BusId { get; set; }
        public decimal? ReservedAccountBalance { get; set; }
        public int? DefaultRouteId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int? OtpCode { get; set; }

    }
}
