using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPPIS.Models;
using System.Threading.Tasks;

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


        [HttpGet("id")]
        //[Authorize]
        public async Task<ActionResult<int>> GetUserId()
        {
            _logger.LogInformation("Getting user id");
            var userId = _userResolverService.GetUserId();
            return Ok(userId);
        }
       

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RemoveUser([FromRoute] int id)
        {
            _logger.LogInformation("Remove user {id}!");
            await _userService.RemoveUser(id);
            return Ok();
        }


    }
}
