using System.ComponentModel.DataAnnotations;

namespace KPUserManagementAPI.Models
{
    public class UserGroup
    {
        [Key]
        public int UserGroupId { get; set; }

        // Foreign key for Users
        public int UserId { get; set; }

        // Navigation property for Users
        public User User { get; set; }

        // Foreign key for Groups
        public int GroupId { get; set; }

        // Navigation property for Groups
        public Group Group { get; set; }
    }
}
