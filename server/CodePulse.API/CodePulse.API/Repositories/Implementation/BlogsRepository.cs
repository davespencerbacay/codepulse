using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogsRepository: IBlogsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BlogsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Blogs> CreateAsync(Blogs blog)
        {
            await dbContext.Blogs.AddAsync(blog);
            await dbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<IEnumerable<Blogs>> GetAllAsync()
        {

            return await dbContext.Blogs.ToListAsync();

        }
    }
}
