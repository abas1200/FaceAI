namespace FaceAI.Application
{
    public class AppConstant
    { 
        public class Security
        {
            public const string SubjectId = "sub";
            public const string UserId = "userid";
            public const string UserName = "preferred_username";
            public const string FirstName = "given_name";
            public const string LastName = "last_name";
            public const string TokenEndpointSectionName = "IdentitySettings";
            public static class Permissions
            {
                public const string AppAccess = "app_access";
 
            }

        }
    }
}