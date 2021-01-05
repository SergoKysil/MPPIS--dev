using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Services.Implementation
{
    public class UserResolverService : IUserResolverService
    {
        private readonly HttpContextAccessor _context;
        public UserResolverService(HttpContextAccessor context)
        {
            _context = context;
        }

        public IEnumerable<Claim> GetClaims()
        {
            return _context.HttpContext.User.Claims;
        }

        public int GetUserId()
        {
            var claimsIdentity = _context.HttpContext.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst("id")?.Value;
            return int.Parse(userId);
        }

        public bool IsUserAdmin()
        {
            return _context.HttpContext.User.IsInRole("Admin");
        }
    }
}
