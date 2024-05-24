using FaceAI.Domain.Entities;

namespace FaceAI.Application.Stores
{
    public interface IPhotoMetaData : IAsyncRepository<Photo>
    {
        // Task SavePhotoMetaData(string userName, string desciption, string fileName);
        Task<List<Photo>> GetUserPhotos(string userName);
    }
}