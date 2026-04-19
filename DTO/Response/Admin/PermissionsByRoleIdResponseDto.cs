namespace DTO.Response.User
{
    public class PermissionsByRoleIdResponseDto
    {
        public PermissionsByRoleIdResponseDto()
        {
            permissions = new List<Permissions>();
            otherPermissions = new List<Permissions>();
        }

        public List<Permissions> permissions { get; set; }
        public List<Permissions> otherPermissions { get; set; }

    }

    public class Permissions
    {
        public int Id { get; set; }
        public string PermissionType { get; set; }
        public int RoleId { get; set; }
        public string PermissionName { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
    }
}
