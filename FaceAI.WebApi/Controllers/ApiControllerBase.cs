using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaceAI.WebApi.Controllers
{
    /// <summary>
    /// base controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiScope")]
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        protected ApiControllerBase(ILogger logger) => Logger = logger;
    }
}
