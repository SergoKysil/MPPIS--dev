using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MPPIS.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MPPIS.Controllers
{


    public class LoginController : Controller
    {
        readonly ITokenService _tokenService;


        public LoginController(ITokenService tokenService)
        {
            this._tokenService = tokenService;
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

                var user = await _tokenService.VerifyUserCredentials(loginDto);
                if (user != null)
                {
                    var claims = new[]
                    {
                new Claim("id",user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Role.Name)
                    };
                    var jwt = _tokenService.GenerateJWT(claims);
                    var refreshToken = await _tokenService.GenerateRefreshToken(user);


                    UserTokenDto userTokenDto = new UserTokenDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Token = new TokenDto() { JWT = jwt, TokenRefresh = refreshToken }
                    };
                    RedirectToAction("Index", "Home");
                    return Ok(userTokenDto);
                }
                return NotFound();

            }
            return View(model);
        }


        [HttpPost("refresh")]
        public async Task<ActionResult<UserTokenDto>> Refresh([FromBody] TokenDto refreshTokenModel)
        {
            var refresh = await _tokenService.VerifyRefreshToken(refreshTokenModel.TokenRefresh);

            var claims = _tokenService.GetPrincipalFromExpiredToken(refreshTokenModel.JWT);
            var jwt = _tokenService.GenerateJWT(claims.Claims);
            var refreshToken = await _tokenService.UpdateRefreshRecord(refresh);

            UserTokenDto userToken = new UserTokenDto
            {
                Id = refresh.User.Id,
                FirstName = refresh.User.FirstName,
                LastName = refresh.User.LastName,
                Token = new TokenDto { JWT = jwt, TokenRefresh = refreshToken }
            };
            return Ok(userToken);
        }

    }
}
