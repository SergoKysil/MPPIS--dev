using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MPPIS.Models;
using System.Threading.Tasks;

namespace MPPIS.Controllers
{


    public class LoginController : Controller
    {
        readonly IUserService _userService;


        public LoginController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<LoginViewModel>> Login([FromForm] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginDto loginDto = new LoginDto
                {
                    Email = model.Email,
                    PasswordHash = model.Password
                };
                var user = _userService.Login(loginDto);
                if (user.Result == null /*|| user.IsEmailConfirmed == false*/)
                {
                    ModelState.AddModelError("", "There is no user with that email or password!");
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

       
    }
}
