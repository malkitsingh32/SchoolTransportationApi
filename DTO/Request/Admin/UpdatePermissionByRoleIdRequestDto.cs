namespace DTO.Request.Admin
{
    public class UpdatePermissionByRoleIdRequestDto
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public string PermissionType { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
    }
}
