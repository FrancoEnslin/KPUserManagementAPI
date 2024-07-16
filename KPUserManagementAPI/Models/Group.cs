using System.ComponentModel.DataAnnotations;

namespace KPUserManagementAPI.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        public string GroupName { get; set; }

        // Navigation property for UserGroups
        public ICollection<UserGroup> UserGroups { get; set; }

        // Navigation property for GroupPermissions
        public ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
