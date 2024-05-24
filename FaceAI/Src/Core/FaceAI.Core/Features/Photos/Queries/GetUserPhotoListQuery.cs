using FaceAI.Application.Features.Photos.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Features.Photos.Queries
{
    public class GetUserPhotoListQuery : IRequest<List<PhotoResponse>>
    {
        public string UserName { get; set; }

        public GetUserPhotoListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
