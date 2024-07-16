using KPUserManagementAPI.Dtos;
using KPUserManagementAPI.Interfaces;
using KPUserManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KPUserManagementAPI.BusinessLogic
{

    //Implements my Interface
    //Initializes db context and handles all business logic.
    //Returns appropriate result after operations.
    public class UsersBusinessLogic : IUserInterface
    {
        private readonly AppDbContext _dbContext;
        public UsersBusinessLogic(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<IActionResult> GetUsers()
        {
            try
            {

                var users = await _dbContext.Users
                    .Select(u => new
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserGroups = u.UserGroups.Select(ug => new
                        {
                            GroupId = ug.Group.GroupId,
                            GroupName = ug.Group.GroupName,
                            Permissions = ug.Group.GroupPermissions.Select(gp => gp.Permission.PermissionName).ToList()
                        }).ToList()
                    })
                    .ToListAsync();

                return new OkObjectResult(users);



                if (users == null || users.Count == 0)
                    return new NotFoundObjectResult("No Users found");

   
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true // Optional: for pretty-printing JSON
                };

                // Serialize users to JSON using the options
                var jsonUsers = JsonSerializer.Serialize(users, options);

                return new OkObjectResult(jsonUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception within Get Users " + ex.Message);
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                // Check if the id is zero or negative
                if (id <= 0) return new BadRequestObjectResult("Invalid User Id");

                var user = await _dbContext.Users
                    .Where(u => u.UserId == id)
                    .Select(u => new
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserGroups = u.UserGroups.Select(ug => new
                        {
                            GroupId = ug.Group.GroupId,
                            GroupName = ug.Group.GroupName,
                            Permissions = ug.Group.GroupPermissions.Select(gp => gp.Permission.PermissionName).ToList()
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (user == null) return new NotFoundObjectResult("Could not find user");

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception within GetUserById: " + ex.Message);
                return new StatusCodeResult(500);
            }
        }


        public async Task<IActionResult> CreateUser(AddUser user)
        {
            try
            {
                // Check if null data was sent
                if (user == null)
                    return new BadRequestObjectResult("Please ensure all fields are populated");

                // Check if user already exists with that userName
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (existingUser != null)
                    return new BadRequestObjectResult("This username is not available");

                // Create a new User object and add it to the DbContext
                var newUser = new User
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,

                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup { GroupId = user.GroupId }
                    }
                };

                // Add the user to the Users DbSet and save changes
                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                return new OkObjectResult("User successfully created");
            }


            catch (Exception ex)
            {
                Console.WriteLine("Exception within Create User " + ex.Message);
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> UpdateUserDetails(UpdateUser receivedUser)
        {
            try
            {
                //Check if null data was sent
                if (receivedUser == null) return new BadRequestObjectResult("Please ensure all fields are populated");

                //get user from table 
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == receivedUser.UserId);
                if (user == null) return new NotFoundObjectResult("Could not find user");

                user.UserName = receivedUser.UserName;
                user.FirstName = receivedUser.FirstName;
                user.LastName = receivedUser.LastName;  

                await _dbContext.SaveChangesAsync();

                //send back the updated user
                return new OkObjectResult("Successfully updated user details");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception within Update User " + ex.Message);
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> UpdateUserGroup(int userId, int newGroupId)
        {
            try
            {
                // Check if userId or newGroupId is zero or negative
                if (userId <= 0) return new BadRequestObjectResult("Invalid UserId");
                if (newGroupId <= 0) return new BadRequestObjectResult("Invalid New GroupId");

                // Check if the user exists
                var user = await _dbContext.Users
                    .Include(u => u.UserGroups)
                    .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null) return new NotFoundObjectResult("User not found");

                // Check if the new group exists
                var newGroup = await _dbContext.Groups.FirstOrDefaultAsync(g => g.GroupId == newGroupId);
                if (newGroup == null) return new BadRequestObjectResult("The specified group does not exist");

                // Remove existing group associations
                user.UserGroups.Clear();

                // Add new group association
                user.UserGroups.Add(new UserGroup
                {
                    UserId = userId,
                    GroupId = newGroupId
                });

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return new OkObjectResult("User group successfully updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception within UpdateUserGroup: " + ex.Message);
                return new StatusCodeResult(500);
            }
        }


        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(u => u.UserGroups)
                    .FirstOrDefaultAsync(u => u.UserId == id);

                if (user == null) return new NotFoundObjectResult("User not found");

                // Remove entries from UserGroups
                _dbContext.UserGroups.RemoveRange(user.UserGroups);

                // Remove the user
                _dbContext.Users.Remove(user);

                await _dbContext.SaveChangesAsync();
               

                return new OkObjectResult("User deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception within Delete User" + ex.Message);
                return new StatusCodeResult(500);
            }
        }


        public async Task<IActionResult> GetUserCount()
        {
            try
            {
                var count = await _dbContext.Users.CountAsync();
                return new OkObjectResult(count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception within Get User Count" + ex.Message);
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> GetUsersCountByGroup(int groupId)
        {
            try
            {
                // Check if the group exists
                var groupExists = await _dbContext.Groups.AnyAsync(g => g.GroupId == groupId);
                if (!groupExists) return new NotFoundResult();

                // Get the count of users in the specified group
                var count = await _dbContext.UserGroups.CountAsync(ug => ug.GroupId == groupId);
                return new OkObjectResult(count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception within Get Group Count" + ex.Message);
                return new StatusCodeResult(500);
            }
        }

    }
}
