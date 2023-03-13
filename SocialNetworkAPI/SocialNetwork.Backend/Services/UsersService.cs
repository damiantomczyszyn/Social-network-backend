using SocialNetwork.Data.Model;
using SocialNetwork.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Backend.Services
{
    public class UsersService : BaseService
    {
        public UsersService(DefaultContext context) : base(context)
        { }

    }
}
