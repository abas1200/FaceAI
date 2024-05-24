using System;

namespace FaceAI.Application.Authorization.Option
{
    /// <summary>
    /// Nss token end point configuration option
    /// </summary>
    public class TokenEndPointOption
    {
        public Uri? BaseUrl { get; set; }
        public string Scope { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public string ClientId { get; set; } = "";
        public bool IsEnabled { get; set; } = true;

    }
}
