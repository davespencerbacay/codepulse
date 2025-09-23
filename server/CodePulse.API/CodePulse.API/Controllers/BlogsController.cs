using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsRepository blogsRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogsController(IBlogsRepository blogsRepository, ICategoryRepository categoryRepository)
        {
            this.blogsRepository = blogsRepository;
            this.categoryRepository = categoryRepository;
        }

        // POST: /api/blogs
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogRequestDTO request)
        {
            var blog = new Blogs
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach(var category in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(category);
                if(existingCategory != null)
                {
                    blog.Categories.Add(existingCategory);
                }
            }

            blog = await blogsRepository.CreateAsync(blog);

            var response = new BlogsDTO 
            {
                Author = blog.Author,
                Content = blog.Content,
                FeaturedImageUrl = blog.FeaturedImageUrl,
                IsVisible = blog.IsVisible,
                PublishedDate = blog.PublishedDate,
                ShortDescription = blog.ShortDescription,
                Title = blog.Title,
                UrlHandle = blog.UrlHandle,
                Categories = blog.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // GET: /api/blogs
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await blogsRepository.GetAllAsync();

            var response = new List<BlogsDTO>();
            foreach(var blog in blogs)
            {
                response.Add(new BlogsDTO
                {
                    Id = blog.Id,
                    Author = blog.Author,
                    Content = blog.Content,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    IsVisible = blog.IsVisible,
                    PublishedDate = blog.PublishedDate,
                    ShortDescription = blog.ShortDescription,
                    Title = blog.Title,
                    UrlHandle = blog.UrlHandle,
                    Categories = blog.Categories.Select(x => new CategoryDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }
    }
}
