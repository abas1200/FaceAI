using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace FaceAI.Application.Services
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {


            var sub = principal.Claims.SingleOrDefault(c => c.Type == AppConstant.Security.UserId) ??
                    principal.Claims.SingleOrDefault(c => c.Type == AppConstant.Security.SubjectId) ??
                    principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var firstname = principal.Claims.SingleOrDefault(c => c.Type == AppConstant.Security.FirstName);
            var lastname = principal.Claims.SingleOrDefault(c => c.Type == AppConstant.Security.LastName);
            var username = principal.Claims.SingleOrDefault(c => c.Type == AppConstant.Security.UserName);

            if (sub == null || username == null)
            {
                return Task.FromResult(principal);
            }

            if (!principal.HasClaim(c => c.Type == AppConstant.Security.UserId))
                principal.Identities.First().AddClaim(new Claim(AppConstant.Security.UserId, sub.Value));
            if (!principal.HasClaim(c => c.Type == AppConstant.Security.UserName))
                principal.Identities.First().AddClaim(new Claim(AppConstant.Security.UserName, username.Value));
            if (firstname != null && !principal.HasClaim(c => c.Type == AppConstant.Security.FirstName))
                principal.Identities.First().AddClaim(new Claim(AppConstant.Security.FirstName, firstname.Value));
            if (lastname != null && !principal.HasClaim(c => c.Type == AppConstant.Security.LastName))
                principal.Identities.First().AddClaim(new Claim(AppConstant.Security.LastName, lastname.Value));


            return Task.FromResult(principal);

        }
    }

}
