using System.ComponentModel.DataAnnotations;

namespace KPUserManagementAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        // Navigation property for UserGroups
        public ICollection<UserGroup> UserGroups { get; set; }

    }
}
