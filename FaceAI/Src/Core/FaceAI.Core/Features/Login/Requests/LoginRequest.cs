using FaceAI.Application;

namespace FaceAI.Application.Features.Login.Requests
{
    public class LoginRequest : BaseRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public override IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(UserName))
                return new[] { $"UserName is required for {nameof(LoginRequest)} " };

            if (string.IsNullOrEmpty(Password))
                return new[] { $"Password is required for {nameof(LoginRequest)} " };

            return Enumerable.Empty<string>();
        }

    }
}
