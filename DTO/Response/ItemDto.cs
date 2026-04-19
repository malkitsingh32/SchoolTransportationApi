namespace DTO.Response
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EMail { get; set; }
        public string LastLogin { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public bool ThankYouMessagePermission { get; set; }
    }
}
