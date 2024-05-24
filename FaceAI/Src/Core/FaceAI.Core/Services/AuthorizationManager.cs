using System.Security.Claims;
using FaceAI.Application.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Logging;
 

namespace FaceAI.Application.Services
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly ILogger<AuthorizationManager> _logger;
        private readonly IAuthorizationService _authService;        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="authService"></param>
        public AuthorizationManager(ILogger<AuthorizationManager> logger, IAuthorizationService authService)
        {
            _logger = logger;
            _authService = authService;            
        }
 

        private async Task<AuthorizationResult> CheckAccessAsync(ClaimsPrincipal currentUser,
            OperationAuthorizationRequirement requirement,
            object resource,
            string unAuthorizedMessage = "Permission Required Action",
            bool throwAccessDenied = true)
        {
            try
            {
                _logger.LogInformation("AuthorizationManager.CheckAccessAsync for current user.");
                var hasAccess = await _authService.AuthorizeAsync(currentUser, resource, PermissionRequirement.AppAccess);
                if (!hasAccess.Succeeded && throwAccessDenied)
                {
                    throw new UnauthorizedAccessException(unAuthorizedMessage);
                }
                hasAccess = await _authService.AuthorizeAsync(currentUser, resource, requirement);

                if (!hasAccess.Succeeded && throwAccessDenied)
                {
                    throw new UnauthorizedAccessException(unAuthorizedMessage);
                }

                _logger.LogInformation("AuthorizationManager.CheckAccessAsync for current user. Access Granted.");
                return hasAccess;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in CheckAccess");
                throw;
            }
        }

        public void CheckAppAccess(ClaimsPrincipal currentUser, OperationAuthorizationRequirement requirement)
        {
            CheckAccessAsync(currentUser, requirement, "Permission Required Action").GetAwaiter().GetResult();
        } 
    }
}