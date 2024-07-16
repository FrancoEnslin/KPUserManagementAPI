using KPUserManagementAPI.BusinessLogic;
using KPUserManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPUserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        //Only initialize business logic. Db context initialized within business Service
        private readonly GroupsBusinessLogic _groupsBusinessLogic;

        public GroupsController(GroupsBusinessLogic groupsBusinessLogic)
        {
            _groupsBusinessLogic = groupsBusinessLogic;
        }

        // GET: api/Groups
        [HttpGet("GetGroups")]
        public async Task<IActionResult> GetGroups()
        {

            return await _groupsBusinessLogic.GetGroups();

        }

        // GET: api/Groups/5
        [HttpGet("GetGroupById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return await _groupsBusinessLogic.GetGroupById(id);
        }

        //// POST: api/Groups
        //[HttpPost("CreateGroup")]
        //public async Task<IActionResult> CreateGroup([FromBody] Group group)
        //{
        //    return await _groupsBusinessLogic.CreateGroup(group);
        //}


    }
}
