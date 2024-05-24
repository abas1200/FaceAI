using AutoMapper;
using FaceAI.Application.Authorization;
using FaceAI.Application.Features.Login.Queries;
using FaceAI.Application.Features.Login.Response;
using FaceAI.Application.Features.Photos.Commands;
using FaceAI.Application.Features.Photos.Responses;
using FaceAI.Domain.Entities;

namespace FaceAI.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Photo, PhotoResponse>().ReverseMap();
            CreateMap<Photo, CreatePhotoCommand>().ReverseMap();
            CreateMap<GetTokenQuery, AuthDataRequest>();
            CreateMap<TokenApiResponse, AuthReponse>();
        }
    }
}
