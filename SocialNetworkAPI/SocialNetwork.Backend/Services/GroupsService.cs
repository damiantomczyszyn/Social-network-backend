using SocialNetwork.Data.Model;
using SocialNetwork.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Backend.Services
{
    public class GroupsService : BaseService
    {
        public GroupsService(DefaultContext context) : base(context)
        { }
        public async Task<List<Group>> GetGroupsForUser(int userId)
        {
            var user = await Context.Users.FindAsync(userId)!;
            var groups = await Context.Groups.Where(
                    p => p.GroupId == userId).ToListAsync();

            return groups;
        }




    }
}
