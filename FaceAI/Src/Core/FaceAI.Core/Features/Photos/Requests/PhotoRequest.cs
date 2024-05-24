using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Features.Photos.Requests
{
    public class PhotoRequest: BaseRequest
    { 
        public string FileName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public override IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(FileName))
                return new[] { $"FileName is required for {nameof(PhotoRequest)} " };


            return Enumerable.Empty<string>();
        }
    }
}
