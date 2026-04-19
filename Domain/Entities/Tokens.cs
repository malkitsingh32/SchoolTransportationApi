namespace Domain.Entities
{
    public class Tokens
    {
        public int Id { get; set; }
        public int UserID { get; set; }

        public Guid MemberID { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
