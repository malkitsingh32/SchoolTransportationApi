namespace Domain.Entities
{
    public class RolePermissionsMapping
    {
        public int? Id { get; set; } 
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
    }
}
