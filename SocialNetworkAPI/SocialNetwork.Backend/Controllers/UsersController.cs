using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data;
using SocialNetwork.Backend.Model.Users;
using SocialNetwork.Data.Model;
using SocialNetwork.Backend.Services;
using AutoMapper;

namespace SocialNetwork.Backend.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> Logger;
        private readonly DefaultContext Context;
        private readonly IMapper Mapper;
        private readonly UsersService UsersService;

        public UserController(ILogger<UserController> logger, DefaultContext context, IMapper mapper, UsersService usersService)
        {
            Logger = logger;
            Context = context;
            Mapper = mapper;
            UsersService = usersService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<ViewModel>> GetLoggedUser()
        {
            int userId = 1; // tymczasowo, dopóki nie doda siê autoryzacji
            var user = await UsersService.FindAsync<User>(userId);

            return Ok(Mapper.Map<ViewModel>(user));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ViewModel>> GetUser(int userId)
        {
            var user = await UsersService.FindAsync<User>(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Mapper.Map<ViewModel>(user);
        }
    }
}