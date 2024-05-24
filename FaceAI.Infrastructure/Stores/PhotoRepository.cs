using FaceAI.Application.Stores;
using FaceAI.Domain.Entities;
using FaceAI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Infrastructure.Stores
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoMetaData
    {
        public PhotoRepository(PhotoDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Photo>> GetUserPhotos(string userName)
        {
            var result = await _dbContext.Photos.Where(item => item.CreatedBy == userName).ToListAsync();
            return result;

        }
    }

}
