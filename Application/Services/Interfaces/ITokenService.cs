using Application.Dto;
using Domain.RDBMS.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<User> VerifyUserCredentials(LoginDto loginModel);

        Task<TokenRefresh> VerifyRefreshToken(string token);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        Task<string> GenerateRefreshToken(User user);

        Task<string> UpdateRefreshRecord(TokenRefresh refreshToken);

        string GenerateJWT(IEnumerable<Claim> claims);
    }
}
