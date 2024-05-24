using AutoMapper;
using FaceAI.Application.Features.Photos.Commands;
using FaceAI.Application.Stores;
using FaceAI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Features.Photos.Handlers
{
    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, List<Guid>>
    {
        private readonly IPhotoMetaData _photoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePhotoCommandHandler> _logger;

        public CreatePhotoCommandHandler(IPhotoMetaData photoRepository, IMapper mapper, ILogger<CreatePhotoCommandHandler> logger)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<Guid>> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        { 
            var result = _mapper.Map<IList<Photo>>(request);
             
            result = await _photoRepository.AddRangeAsync(result);

            return result?.Select(item => item.PhotoId).ToList();
        }
    }
}
