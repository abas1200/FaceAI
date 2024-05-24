
using FaceAI.Application.Features.Login.Queries;
using FaceAI.Application.Features.Login.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FaceAI.WebApi.Controllers.V1
{
    [V1]
    [AllowAnonymous]
    public class LoginController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IMediator mediator, ILogger<LoginController> logger) : base(logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// authenticate user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            _logger.LogDebug("start login username: {0}", request.UserName);

            var response = await _mediator.Send(new GetTokenQuery(request.UserName, request.Password));

            if (response == null) return BadRequest();

            if (String.IsNullOrEmpty(response.Token)) return Unauthorized();


            return Ok(new
            {
                response.Token,
                response.ExpiresIn,
                response.RefreshToken
            });
        }
         
    }
}
