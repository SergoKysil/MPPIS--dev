using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MPPIS.Models;
using System.Threading.Tasks;

namespace MPPIS.Controllers
{

    //[Route("api/[controller]")]
    //[ApiController]
    public class AddNewUserController : Controller
    {
        readonly IUserService _userService;
        readonly ILocationService _locationService;
        public AddNewUserController(IUserService userService, ILocationService locationService)
        {
            this._userService = userService;
            this._locationService = locationService;
        }

        public IActionResult AddNewUser()
        {
            return View();
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AddUserViewModel>> AddNewUser([FromForm] AddUserViewModel user)
        {
            // _logger.LogInformation("Create user {user}", user);
            if (ModelState.IsValid)
            {
                LocationDto location = new LocationDto()
                {
                    City = user.City,
                    District = user.District,
                    HouseNumber = user.HouseNumber,
                    Street = user.Street,
                    Village = user.Village
                };

                var locationcreated = await _locationService.AddNewLocation(location);

                AddUserDto addUserDto = new AddUserDto()
                {
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PasswordHash = user.Password,
                    LocationId = locationcreated.Id   
                };
                var createUser = await _userService.AddUser(addUserDto);

                if (createUser != null)
                {
                    RedirectToAction("Index", "Home");
                    //return CreatedAtAction(nameof(UserController.GetUserId), new { id = createUser.Id }, createUser);
                }
                //_logger.LogWarning("User already EXISTS!");
            }
            ModelState.AddModelError("", "Error");
            return View(user);
            //return BadRequest();
        }
    }
}
