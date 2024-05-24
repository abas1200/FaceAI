using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
 
namespace FaceAI.Application.Authorization
{
    public class ApiAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {
        private readonly ILogger<ApiAuthorizationHandler> _logger;
 
        public ApiAuthorizationHandler(
          ILogger<ApiAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, object resource)
        {

            _logger.LogInformation("handing requirement");
  
            if (requirement is PermissionRequirement)
            { 
                var userId = context.User.FindFirst(c => c.Type == AppConstant.Security.UserId)?.Value;
                _logger.LogDebug("permission requirement handling for user #{userId}", userId);
                if (userId != null)
                {  
                  bool  hasAccess = await UserHasPermissionAsync(userId, requirement.Name);
                     
                    if (hasAccess) context.Succeed(requirement);
                        return;
                }
                _logger.LogWarning("user #{userId} cannot be found", userId);
            }
            _logger.LogWarning("there is no requirement, context will be failed");
            context.Fail();
        }

        private async Task<bool> UserHasPermissionAsync(string userId, string permissionName)
        {
            _logger.LogInformation("checking user #{userId} has permission {permissionName}", userId, permissionName);
            if (String.IsNullOrEmpty(permissionName)) return false;
           
            var permissions =  new List<string>(); //await _clientPolicyService.GetGrantedPermissionsAsync(userId);
            permissions.Add("app_access");


            _logger.LogDebug("these {permissions} permissions ared to the user #{userId} ", String.Join(",", permissions), userId);
            var result = permissions.Any(s => s.Equals(permissionName, StringComparison.InvariantCultureIgnoreCase));
            _logger.LogInformation("result {result}: user #{userId} has  {permissionName} ", result, userId, permissionName);
            return result;
        }
    }
}
