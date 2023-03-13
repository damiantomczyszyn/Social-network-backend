using SocialNetwork.Data.Model;
using SocialNetwork.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Backend.Services
{

    public class CommentsService : BaseService
    {
        public CommentsService(DefaultContext context) : base(context)
        { }
        public async Task<List<Comment>> GetCommentForPost(int postId)
        {
            var post = await Context.Posts.FindAsync(postId)!;
            var comments = await Context.Comments.Where(
                    p => p.PostId == postId).ToListAsync();

            return comments;
        }


    }
}
