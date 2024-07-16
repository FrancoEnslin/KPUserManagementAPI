using System.ComponentModel.DataAnnotations;

namespace KPUserManagementAPI.Models
{
    public class GroupPermission
    {
        [Key]
        public int GroupPermissionId { get; set; }

        // Foreign key for Groups
        public int GroupId { get; set; }

        // Navigation property for Groups
        public Group Group { get; set; }

        // Foreign key for Permissions
        public int PermissionId { get; set; }

        // Navigation property for Permissions
        public Permission Permission { get; set; }
    }
}
