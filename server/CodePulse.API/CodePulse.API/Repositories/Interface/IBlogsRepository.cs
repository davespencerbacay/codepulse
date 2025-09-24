using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogsRepository
    {
        Task<Blogs> CreateAsync(Blogs blogs);
        Task<IEnumerable<Blogs>> GetAllAsync();
        Task<Blogs?> GetByIdAsync(Guid id);
        Task<Blogs?> UpdateAsync(Blogs blog);
    }
}
