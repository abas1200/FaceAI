using AutoMapper;
using FaceAI.Application.Features.Photos.Queries;
using FaceAI.Application.Features.Photos.Responses;
using FaceAI.Application.Stores;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Features.Photos.Handlers
{
    public class GetUserPhotoListQueryHandler : IRequestHandler<GetUserPhotoListQuery, List<PhotoResponse>>
    {
        private readonly IPhotoMetaData _photoRepository;
        private readonly IMapper _mapper;

        public GetUserPhotoListQueryHandler(IPhotoMetaData photoRepository, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
        }
        public async Task<List<PhotoResponse>> Handle(GetUserPhotoListQuery request, CancellationToken cancellationToken)
        {
            var photoList = await _photoRepository.GetUserPhotos(request.UserName);
            return _mapper.Map<List<PhotoResponse>>(photoList);
        }
         
    } 
}