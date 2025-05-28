using Microsoft.AspNetCore.Mvc;
using PersonalFinanceAPI.Models;
using PersonalFinanceAPI.Services;

namespace PersonalFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(GroupService groupService): ControllerBase 
    {
        private readonly GroupService _groupService = groupService;

        [HttpGet]
        public async Task<Group[]> GetGroups()
        {
            return await _groupService.GetGroups();
        }

        [HttpGet("{id:int}")]
        public async Task<Group> GetGroup(int id)
        {
            return await _groupService.GetGroup(id);
        }

        [HttpPost()]
        public async Task<Group> AddGroup([FromBody] Group group){
            var groupDb = await _groupService.AddGroup(group);
            return groupDb;
        }
    }
}
