using Microsoft.AspNetCore.Mvc;

namespace FaceAI.WebApi.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    public class V1Attribute : ApiVersionAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public V1Attribute() : base(new ApiVersion(1, 0))
        {
        }
    }
}
