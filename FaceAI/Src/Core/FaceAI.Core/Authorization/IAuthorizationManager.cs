using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace FaceAI.Application.Authorization
{
    public interface IAuthorizationManager
    {
        public void CheckAppAccess(ClaimsPrincipal currentUser, OperationAuthorizationRequirement requirement);
 
    }
}
