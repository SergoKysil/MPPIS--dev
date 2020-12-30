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
        [Authorize]
        public async Task<ActionResult<int>> GetUserId()
        {
            _logger.LogInformation("Getting user id");
            var userId = _userResolverService.GetUserId();
            return Ok(userId);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AddUserViewModel>> AddNewUser([FromBody] AddUserViewModel user)
        {
            _logger.LogInformation("Create user {user}", user);
            AddUserDto addUserDto = new AddUserDto()
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.Password,
                LocationDTO = new LocationDto()
                {
                    City = user.City,
                    District = user.District,
                    Village = user.Village,
                    Street = user.Street,
                    HouseNumber = user.HouseNumber
                }
            };
            if (ModelState.IsValid) {
                var createUser = await _userService.AddUser(addUserDto);
                if (createUser != null)
                {
                    return CreatedAtAction(nameof(GetUserId), new { id = createUser.Id }, createUser);
                }
                _logger.LogWarning("User already EXISTS!");
            }
            return BadRequest();
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
