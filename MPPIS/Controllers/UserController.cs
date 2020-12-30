using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MPPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        readonly IUserService _userService;
        readonly IUserResolverService _userResolverService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, IUserResolverService userResolverService, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._userResolverService = userResolverService;
            this._logger = logger;
        }


    }
}
