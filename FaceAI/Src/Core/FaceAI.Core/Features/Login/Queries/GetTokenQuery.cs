using FaceAI.Application.Features.Login.Response;
using FaceAI.Application.Features.Photos.Queries;
using FaceAI.Application.Features.Photos.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Features.Login.Queries
{
    public class GetTokenQuery : IRequest<AuthReponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public GetTokenQuery(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
