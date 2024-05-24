using System.Text.Json.Serialization;

namespace FaceAI.Application.Features.Login.Response
{
    public class TokenApiResponse  
    {
        [JsonPropertyName("access_token")] public string Token { get; set; } = "";
        [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }
        [JsonPropertyName("token_type")] public string TokenType { get; set; } = "";
        [JsonPropertyName("scope")] public string Scope { get; set; } = "";
        [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; } = "";
    }
}
