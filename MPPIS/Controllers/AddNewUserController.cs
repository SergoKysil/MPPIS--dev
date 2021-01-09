using Application.Dto;
using Application.Services.Implementation;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPPIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<ActionResult<AddUserViewModel>> AddNewUser(AddUserViewModel user)
        {
            // _logger.LogInformation("Create user {user}", user);
            // if (ModelState.IsValid)
            // {
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
                return CreatedAtAction(nameof(UserController.GetUserId), new { id = createUser.Id }, createUser);
            }
            ModelState.AddModelError("", "Error");
            return View(user);
            //_logger.LogWarning("User already EXISTS!");
            //}
            // return BadRequest();
        }
    }
}
