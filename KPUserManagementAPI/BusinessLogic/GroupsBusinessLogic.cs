using KPUserManagementAPI.Dtos;
using KPUserManagementAPI.Interfaces;
using KPUserManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPUserManagementAPI.BusinessLogic
{
    public class GroupsBusinessLogic : IGroupInterface
    {
        private readonly AppDbContext _dbContext;
        public GroupsBusinessLogic(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var groups = await _dbContext.Groups
                    .Include(g => g.GroupPermissions)     // Include GroupPermissions navigation property
                        .ThenInclude(gp => gp.Permission) // Then include Permission navigation property within GroupPermissions
                    .Include(g => g.UserGroups)          // Include UserGroups navigation property
                        .ThenInclude(ug => ug.User)      // Then include User navigation property within UserGroups
                    .ToListAsync();

                if (groups == null || groups.Count == 0)
                    return new NotFoundObjectResult("No Groups found");

                var groupsDto = new List<ReturnGroup>();
                foreach (var group in groups)
                {
                    var groupDto = new ReturnGroup
                    {
                        GroupId = group.GroupId,
                        GroupName = group.GroupName,
                        Permissions = new List<string>(),
                        Users = new List<string>()
                    };
                    foreach (var groupPermission in group.GroupPermissions)
                    {
                        groupDto.Permissions.Add(groupPermission.Permission.PermissionName);
                    }
                    foreach (var userGroup in group.UserGroups)
                    {
                        groupDto.Users.Add(userGroup.User.UserName);
                    }
                    groupsDto.Add(groupDto);
                }

                return new OkObjectResult(groupsDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception within GetGroups: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }


        public async Task<IActionResult> GetGroupById(int id)
        {
            try
            {
                if (id <= 0) return new BadRequestObjectResult("Invalid Group Id");

                var group = await _dbContext.Groups
                    .Include(g => g.GroupPermissions)     // Include GroupPermissions navigation property
                        .ThenInclude(gp => gp.Permission) // Then include Permission navigation property within GroupPermissions
                    .Include(g => g.UserGroups)          // Include UserGroups navigation property
                        .ThenInclude(ug => ug.User)      // Then include User navigation property within UserGroups
                    .FirstOrDefaultAsync(g => g.GroupId == id);

                if (group == null) return new NotFoundObjectResult("Group not found");

                var groupDto = new ReturnGroup
                {
                    GroupId = group.GroupId,
                    GroupName = group.GroupName,
                    Permissions = group.GroupPermissions.Select(gp => gp.Permission.PermissionName).ToList(),
                    Users = group.UserGroups.Select(ug => ug.User.UserName ).ToList()
                };

                return new OkObjectResult(groupDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception within GetGroupById: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }

        //public async Task<IActionResult> CreateGroup(Group group)
        //{
        //    try
        //    {
        //        //Check if null data was sent
        //        if (group == null) return new BadRequestObjectResult("Please ensure all fields are populated");

        //        //Check if group already exists with that userName
        //        var existingGroup = await _dbContext.Groups.FirstOrDefaultAsync(u => u.GroupName == group.GroupName);
        //        if (existingGroup != null) return new BadRequestObjectResult("This Group Name is not available");

        //        _dbContext.Groups.Add(group);
        //        await _dbContext.SaveChangesAsync();
        //        return new OkObjectResult("Group successfully created");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception within Create Group " + ex.Message);
        //        return new StatusCodeResult(500);
        //    }
        //}
    }
}
