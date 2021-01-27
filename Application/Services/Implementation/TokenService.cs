using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Services.Interfaces;
using Domain.RDBMS;
using Domain.RDBMS.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<TokenRefresh> _tokenRefreshRepository;
        private readonly IRepository<User> _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public TokenService(IConfiguration configuration, IRepository<TokenRefresh> tokenRepository, IRepository<User> userRepossitory)
        {
            this._configuration = configuration;
            this._tokenRefreshRepository = tokenRepository;
            this._userRepository = userRepossitory;
            this._passwordHasher = new PasswordHasher<User>();
        }

        public string GenerateJWT(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _configuration["JwtToken:Issuer"];
            var audience = _configuration["JwtToken:Audience"];
            var jwtValidity = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtToken:TokenExpiry"]));

            var token = new JwtSecurityToken(issuer,
              audience,
              claims,
              expires: jwtValidity,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var randomNumber = new byte[32];
            string refreshToken;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
                TokenRefresh refreshTokenModel = new TokenRefresh
                {
                    UserId = user.Id,
                    Token = refreshToken
                };
                _tokenRefreshRepository.Add(refreshTokenModel);
            }

            await _tokenRefreshRepository.SaveChangesAsync();
            return refreshToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public async Task<string> UpdateRefreshRecord(TokenRefresh refreshToken)
        {
            var randomNumber = new byte[32];
            string refresh;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refresh = Convert.ToBase64String(randomNumber);
                refreshToken.Token = refresh;
                _tokenRefreshRepository.Update(refreshToken);
            }
            await _tokenRefreshRepository.SaveChangesAsync();
            return refresh;
        }

        public async Task<TokenRefresh> VerifyRefreshToken(string token)
        {
            var refreshToken = await _tokenRefreshRepository.GetAll().Include(p => p.User).FirstOrDefaultAsync(p => p.Token.Equals(token));
            if (refreshToken == null) throw new SecurityTokenException("Invalid refresh token");
            return refreshToken;
        }

        public async Task<User> VerifyUserCredentials(LoginDto loginModel)
        {
            var user = await _userRepository.GetAll()
               .Include(r => r.Role)
               .FirstOrDefaultAsync(p => p.Email == loginModel.Email);

            if (user == null)
            {
                return null;
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.PasswordHash) == PasswordVerificationResult.Failed)
            {

                return null;
            }

            return user;
        }
    }
}
