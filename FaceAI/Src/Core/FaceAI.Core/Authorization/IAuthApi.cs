using FaceAI.Application.Features.Login.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Authorization
{
    public interface IAuthApi
    {
        [Post("/connect/token")]
        Task<TokenApiResponse> GetToken([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
    }
}
