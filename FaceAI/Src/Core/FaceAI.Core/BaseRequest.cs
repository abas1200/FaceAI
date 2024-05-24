using System.Collections.Generic;
using System.Linq;


namespace FaceAI.Application
{

    public abstract class BaseRequest
    {
        public virtual IEnumerable<string> Validate()
        {
            return Enumerable.Empty<string>();
        }
    }
}

