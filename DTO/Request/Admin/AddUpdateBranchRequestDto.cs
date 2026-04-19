

namespace DTO.Request.Admin
{
    public class AddUpdateBranchRequestDto
    {
        public string BranchName { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalCell { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BuildingSys { get; set; }
        public int Gender { get; set; }
        public List<int> Grade { get; set; }
    }
}
