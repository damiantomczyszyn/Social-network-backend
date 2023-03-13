using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data;
using SocialNetwork.Backend.Model.Posts;
using SocialNetwork.Data.Model;
using SocialNetwork.Backend.Services;
using AutoMapper;
using SocialNetwork.Data;
using System.Linq;

namespace SocialNetwork.Backend.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<UserController> Logger;
        private readonly DefaultContext Context;
        private readonly PostsService Posts;
        private readonly IMapper Mapper;

        public PostsController(ILogger<UserController> logger, DefaultContext context, PostsService posts, IMapper mapper)
        {
            Logger = logger;
            Context = context;
            Posts = posts;
            Mapper = mapper;
        }

        [HttpGet("main-feed")]
        public async Task<ActionResult<List<ViewModel>>> GetPostsForUserMainFeed([FromQuery] Pager pager)
        {
            int userId = 1; // tymczasowo, dopóki nie doda siê autoryzacji
            var posts = (await Posts.GetPostsForUserMainFeed(userId)).AsQueryable().Paginate(pager);
            return Ok(Mapper.Map<List<ViewModel>>(posts));
        }

        [HttpGet("profile/{userId}")]
        public async Task<ActionResult<List<ViewModel>>> GetPostsForUserPage(int userId, [FromQuery] Pager pager)
        {
            var posts = (await Posts.GetPostsForUserPage(userId)).AsQueryable().Paginate(pager);
            return Ok(Mapper.Map<List<ViewModel>>(posts));
        }
 
        [HttpPost]
        public async Task<ActionResult<ViewModel>> CreatePost([FromBody] FormModel model)
        {
            var post = await Posts.CreateAsync(Mapper.Map<Post>(model));
            post.SenderId = 1; // tymczasowo, dopóki nie doda siê autoryzacji
            return Ok(Mapper.Map<ViewModel>(post));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ViewModel>> UpdatePost(int id, [FromBody] FormModel model)
        {
            var entity = Posts.Find<Post>(id);

            if (entity == null)
            {
                return NotFound();
            }

            Mapper.Map(model, entity);
            var post = await Posts.UpdateAsync(entity);
            return Ok(Mapper.Map<ViewModel>(post));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var entity = Posts.Find<Post>(id);

            if (entity == null)
            {
                return NotFound();
            }

            await Posts.RemoveAsync(entity);
            return NoContent();
        }
    }
}