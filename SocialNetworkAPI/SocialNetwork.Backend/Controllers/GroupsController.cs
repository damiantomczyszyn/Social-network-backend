using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data;
using SocialNetwork.Data.Model;
using SocialNetwork.Backend.Services;
using AutoMapper;
using System.Linq;
using SocialNetwork.Data.Model;
using SocialNetwork.Backend.Services;
using SocialNetwork.Backend.Model.Groups;

namespace SocialNetwork.Backend.Controllers
{   [ApiController]
    [Route("groups")]

    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> Logger;
        private readonly DefaultContext Context;
        private readonly GroupsService Groups;
        private readonly IMapper Mapper;
        public GroupsController(ILogger<GroupsController> logger, DefaultContext context, GroupsService groups, IMapper mapper)
        {
            Logger = logger;
            Context = context;
            Groups = groups;
            Mapper = mapper;            
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            var entity = Groups.Find<Group>(id);

            if (entity == null)
            {
                return NotFound();
            }
            //if admin
            await Groups.RemoveAsync(entity);
            return NoContent();
        }

        [HttpGet("profile/{userId}")]
        public async Task<ActionResult<List<ViewModel>>> GetGroupsForUser(int userId, [FromQuery] Pager pager)
        {
            var groups = (await Groups.GetGroupsForUser(userId)).AsQueryable().Paginate(pager);
            return Ok(Mapper.Map<List<ViewModel>>(groups));
        }

        [HttpGet("list")]
        public IEnumerable<Group> GetGroups()
        {
            return Context.Groups.ToList();
        }

        [HttpPut("{id}")]//update
        public async Task<ActionResult<ViewModel>> UpdateGroup(int id, [FromBody] FormModel model)
        {
            var entity = Groups.Find<Group>(id);

            if (entity == null)
            {
                return NotFound();
            }

            Mapper.Map(model, entity);

            var group = await Groups.UpdateAsync(entity);// if admin
            return Ok(Mapper.Map<ViewModel>(group));
        }

        [HttpPost]
        public async Task<ActionResult<ViewModel>> CreateGroup([FromBody] FormModel model,int userId)
        {
            var entity = Mapper.Map<Group>(model);
            entity.CreatorId = userId;

            var group = await Groups.CreateAsync(entity);

            return Ok(Mapper.Map<ViewModel>(group));
        }


    }
}
