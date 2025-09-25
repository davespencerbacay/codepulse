using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogsRepository
    {
        Task<Blogs> CreateAsync(Blogs blogs);
        Task<IEnumerable<Blogs>> GetAllAsync();
        Task<Blogs?> GetByIdAsync(Guid id);
        Task<Blogs?> GetByUrlHandleAsync(string urlHandle);
        Task<Blogs?> UpdateAsync(Blogs blog);
        Task<Blogs?> DeleteAsync(Guid id);
    }
}
