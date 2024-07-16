using KPUserManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KPUserManagementAPI.Interfaces
{
    public interface IGroupInterface
    {
        Task<IActionResult> GetGroups();
        Task<IActionResult> GetGroupById(int id);
        //Task<IActionResult> CreateGroup(Group group);
    }
}
