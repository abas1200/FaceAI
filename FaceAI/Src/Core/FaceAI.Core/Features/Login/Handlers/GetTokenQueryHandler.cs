using AutoMapper;
using FaceAI.Application.Authorization;
using FaceAI.Application.Authorization.Option;
using FaceAI.Application.Exceptions;
using FaceAI.Application.Features.Login.Queries;
using FaceAI.Application.Features.Login.Response;
using FaceAI.Application.Features.Photos.Responses;
using FaceAI.Application.Stores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Features.Login.Handlers
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, AuthReponse>
    {
        private readonly IAuthApi _authApi;
        private readonly IMapper _mapper;
        private readonly IOptions<AccessRequestApiOption> _tokenOption;
        public GetTokenQueryHandler(IAuthApi authApi, IOptions<AccessRequestApiOption> tokenOption, IMapper mapper)
        {
            this._authApi = authApi;
            this._mapper = mapper;
            this._tokenOption = tokenOption;
        }
        public async Task<AuthReponse> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {

            var data = new Dictionary<string, object>
                {
                    {"grant_type", "password"},
                    {"scope", _tokenOption.Value.TokenEndpoint.Scope} ,
                    {"client_id", _tokenOption.Value.TokenEndpoint.ClientId},
                    {"client_secret", _tokenOption.Value.TokenEndpoint.ClientSecret},
                    {"username", request.UserName},
                    {"password", request.Password}
                };
            try
            {
                var response = await _authApi.GetToken(data);   
                return _mapper.Map<AuthReponse>(response);
            }
            catch (Exception ex) { throw new InvalidCredentialException(ex.Message); }
           
        }
          
    } 
}