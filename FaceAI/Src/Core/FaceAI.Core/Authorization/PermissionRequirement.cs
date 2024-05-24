using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace FaceAI.Application.Authorization
{
    public class PermissionRequirement : OperationAuthorizationRequirement
    { 
        public static PermissionRequirement GetPermission(string name)
        {
            return new PermissionRequirement() { Name = name };
        }

        public static PermissionRequirement AppAccess =
            new PermissionRequirement() { Name = AppConstant.Security.Permissions.AppAccess };
     
    }

}
