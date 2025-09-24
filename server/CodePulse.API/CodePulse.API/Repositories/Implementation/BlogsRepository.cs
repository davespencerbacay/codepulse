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

        public async Task<Blogs?> DeleteAsync(Guid id)
        {
            var existingBlog = await dbContext.Blogs.FirstOrDefaultAsync(x => x.Id == id);

            if(existingBlog != null)
            {
                dbContext.Blogs.Remove(existingBlog);
                await dbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<Blogs>> GetAllAsync()
        {

            return await dbContext.Blogs.Include(x => x.Categories).ToListAsync();

        }
        public async Task<Blogs?> GetByIdAsync(Guid id)
        {
            return await dbContext.Blogs.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Blogs?> UpdateAsync(Blogs blog)
        {
            var existingBlog = await dbContext.Blogs.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blog.Id);

            if(existingBlog == null)
            {
                return null;
            }

            dbContext.Entry(existingBlog).CurrentValues.SetValues(blog);
            existingBlog.Categories = blog.Categories;
            await dbContext.SaveChangesAsync();

            return blog;
        }
    }
}
