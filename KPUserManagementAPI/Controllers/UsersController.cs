using KPUserManagementAPI.BusinessLogic;
using KPUserManagementAPI.Dtos;
using KPUserManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KPUserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //Only initialize business logic. Db context initialized within business Service
        private readonly UsersBusinessLogic _usersBusinessLogic;

        public UsersController( UsersBusinessLogic usersBusinessLogic)
        {
               _usersBusinessLogic = usersBusinessLogic;
        }

        // GET: api/Users
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {

            return await _usersBusinessLogic.GetUsers();
  
        }

        // GET: api/Users/5
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
           return await _usersBusinessLogic.GetUserById(id);
        }

        // POST: api/Users
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] AddUser user)
        {
            return await _usersBusinessLogic.CreateUser(user);
        }

        // PUT: api/Users/5
        [HttpPut("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUser user)
        {
            return await _usersBusinessLogic.UpdateUserDetails(user);
        }

        // PUT: api/Users/5/permissions
        [HttpPut("UpdateUserGroup/{userId}/{newGroupId}")]
        public async Task<IActionResult> UpdateUserGroup(int userId, int newGroupId)
        {
            return await _usersBusinessLogic.UpdateUserGroup(userId, newGroupId);
        }

        // DELETE: api/Users/5
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
           return await _usersBusinessLogic.DeleteUser(id); 
        }

        // GET: api/Users/count
        [HttpGet("GetUserCount")]
        public async Task<IActionResult> GetUserCount()
        {
        return await _usersBusinessLogic.GetUserCount();
        }

        // GET: api/Users/{groupId}/count
        [HttpGet("GetUserCountByGroup/{groupId}")]
        public async Task<IActionResult> GetUsersCountByGroup(int groupId)
        {
            return await _usersBusinessLogic.GetUsersCountByGroup(groupId);
        }
    }
}