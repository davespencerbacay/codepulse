using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogsRepository
    {
        Task<Blogs> CreateAsync(Blogs blogs);
    }
}
