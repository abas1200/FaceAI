using System;
using System.Collections.Generic;
using System.Text;

namespace FaceAI.Application.Authorization.Option
{
    public class AccessRequestApiOption: ITokenEndPointOption
    {
        public const string SectionName = AppConstant.Security.TokenEndpointSectionName;
        public AccessRequestApiOption()
        {
            TokenEndpoint = new TokenEndPointOption();
        }
        public  TokenEndPointOption TokenEndpoint { get; set; }
        public Uri BaseUrl { get; set; }
        //public string AccessRequestScope { get; set; } = "";
        //public string AccessRequestClientSecret { get; set; } = "";
        //public string AccessRequestClientId { get; set; } = "";
    }
}
