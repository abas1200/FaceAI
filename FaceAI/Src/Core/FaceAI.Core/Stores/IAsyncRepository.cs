using System.Linq.Expressions;
using FaceAI.Domain.Common;

namespace FaceAI.Application.Stores
{ 
    public interface IAsyncRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size);
        Task<IList<T>> AddRangeAsync(IList<T> entity);
        
    }
}