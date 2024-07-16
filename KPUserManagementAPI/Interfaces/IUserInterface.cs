using KPUserManagementAPI.Dtos;
using KPUserManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KPUserManagementAPI.Interfaces
{
    public interface IUserInterface
    {
        Task<IActionResult> GetUsers();
        Task<IActionResult> GetUserById(int id);
        Task<IActionResult> CreateUser(AddUser user);
        Task<IActionResult> UpdateUserDetails(UpdateUser user);
        Task<IActionResult> UpdateUserGroup(int userId, int newGroupId);
        Task<IActionResult> DeleteUser(int id);
        Task<IActionResult> GetUserCount();
        Task<IActionResult> GetUsersCountByGroup(int groupId);
    }
}
