using FaceAI.Application.Features.Photos.Queries;
using FaceAI.Application.Features.Photos.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaceAI.WebApi.Controllers.V1
{
    [V1]
    public class PhotoController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger logger;
         
        public PhotoController(IMediator mediator, ILogger<PhotoController> logger) : base(logger)
        {
            _mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<List<PhotoResponse>>> GetAllUserPhoto(string UserName)
        { 
            var result = await _mediator.Send(new GetUserPhotoListQuery(UserName));
            return Ok(result);
        }


    }
}
