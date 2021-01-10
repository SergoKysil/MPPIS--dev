using Application.Dto;
using Application.Services.Interfaces;
using Domain.RDBMS;
using Domain.RDBMS.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPPIS.Models;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Security.Claims;
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

        private async Task Authenticate(UserDto user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
