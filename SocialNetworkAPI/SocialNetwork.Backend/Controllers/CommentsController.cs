using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data;
using SocialNetwork.Backend.Model.Comments;
using SocialNetwork.Data.Model;
using SocialNetwork.Backend.Services;
using AutoMapper;
using System.Linq;

namespace SocialNetwork.Backend.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<UserController> Logger;
        private readonly DefaultContext Context;
        private readonly CommentsService Comments;
        private readonly PostsService Posts;
        private readonly IMapper Mapper;

        public CommentsController(ILogger<UserController> logger, DefaultContext context, CommentsService comments, PostsService posts, IMapper mapper)
        {
            Logger = logger;
            Context = context;
            Comments = comments;
            Mapper = mapper;
            Posts = posts;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var entity = Comments.Find<Comment>(id);

            if (entity == null)
            {
                return NotFound();
            }

            await Comments.RemoveAsync(entity);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ViewModel>> UpdatePost(int id, [FromBody] FormModel model)
        {
            var entity = Comments.Find<Comment>(id);

            if (entity == null)
            {
                return NotFound();
            }

            Mapper.Map(model, entity);
            var comment = await Comments.UpdateAsync(entity);
            return Ok(Mapper.Map<ViewModel>(comment));
        }
        
        [HttpPost]
        public async Task<ActionResult<ViewModel>> CreateComment([FromBody] FormModel model)
        {
            var post = await Posts.FindAsync<Post>(model.PostId);
            if (post == null)
            {
                return NotFound();
            }

            var entity = Mapper.Map<Comment>(model);
            entity.SenderId = 1; // tymczasowo, dopóki nie doda się autoryzacji
            entity.DateTime = DateTime.Now;

            var comment = await Comments.CreateAsync(entity);

            return Ok(Mapper.Map<ViewModel>(comment));
        }
    }
}
