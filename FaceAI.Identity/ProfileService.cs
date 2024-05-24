using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using FaceAI.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FaceAI.Identity
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }



        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
            {
            new Claim("preferred_username", user.UserName),
            new Claim("userid", user.Id),
            new Claim("email", user.Email)
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {

            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null);
        }
    }
}
