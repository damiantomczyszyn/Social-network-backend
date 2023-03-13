using SocialNetwork.Data.Model;
using SocialNetwork.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LinqKit.Core;
using LinqKit;

namespace SocialNetwork.Backend.Services
{
    public class PostsService : BaseService
    {
        public PostsService(DefaultContext context)
            : base(context)
        { }

        public async Task<List<Post>> GetPostsForUserMainFeed(int userId)
        {
            var predicate = PredicateBuilder.New<Post>(false);

            var user = await Context.Users
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .Include(u => u.Groups)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            predicate.Or(p => p.SenderId == userId);
            predicate.Or(p => Context.Friends.Where(p => p.User1Id == userId || p.User2Id == userId).Select(p => p.User1Id == userId ? p.User2Id : p.User1Id).Contains(p.SenderId));
            predicate.Or(p => p.GroupId.HasValue && user.Groups.Select(p => p.GroupId).Contains(p.GroupId.Value));

            return await Context.Posts.Where(predicate).ToListAsync();
        }

        public async Task<List<Post>> GetPostsForUserPage(int userId)
        {
            return await Context.Posts.Where(p => p.ProfileId.HasValue && p.ProfileId.Value == userId).ToListAsync();
        }
    }
}
