using System.ComponentModel.DataAnnotations;

namespace KPUserManagementAPI.Models
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        public string PermissionName { get; set; }

        // Navigation property for GroupPermissions
        public ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
