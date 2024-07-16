using KPUserManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KPUserManagementAPI
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        //Models
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }

        //Seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission { PermissionId = 1, PermissionName = "Admin" },
                new Permission { PermissionId = 2, PermissionName = "User" }
            );

            // Configure Groups
            modelBuilder.Entity<Group>().HasData(
                new Group { GroupId = 1, GroupName = "Admin Group" },
                new Group { GroupId = 2, GroupName = "User Group" }
            );

            // Configure GroupPermissions
            modelBuilder.Entity<GroupPermission>().HasData(
                new GroupPermission { GroupPermissionId = 1, GroupId = 1, PermissionId = 1 }, // Admin Group has Admin permission
                new GroupPermission { GroupPermissionId = 2, GroupId = 1, PermissionId = 2 }, // Admin Group has User permission
                new GroupPermission { GroupPermissionId = 3, GroupId = 2, PermissionId = 2 }  // User Group has User permission
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
