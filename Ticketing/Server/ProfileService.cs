using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ticketing.Server.Models;

namespace Ticketing.Server
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;
        public ProfileService(UserManager<ApplicationUser> userManager/*, RoleManager<ApplicationUser> roleManager*/)
        {
            _userManager = userManager;
            //_roleManager = roleManager;
        }

        public ProfileService()
        {
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            var nameClaim = context.Subject.FindAll(JwtClaimTypes.Name);
            context.IssuedClaims.AddRange(nameClaim);

            var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
            context.IssuedClaims.AddRange(roleClaims);
            var user = await _userManager.GetUserAsync(context.Subject);

           

            var claims = new List<Claim>
        {
            new Claim("userId", user.Id),
        };
            context.IssuedClaims.AddRange(claims);

            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            await Task.CompletedTask;
        }
    }
}
