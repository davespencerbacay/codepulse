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

        public BlogsController(IBlogsRepository blogsRepository)
        {
            this.blogsRepository = blogsRepository;
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
                UrlHandle = request.UrlHandle
            };

            await blogsRepository.CreateAsync(blog);

            var response = new CreateBlogRequestDTO
            {
                Author = blog.Author,
                Content = blog.Content,
                FeaturedImageUrl = blog.FeaturedImageUrl,
                IsVisible = blog.IsVisible,
                PublishedDate = blog.PublishedDate,
                ShortDescription = blog.ShortDescription,
                Title = blog.Title,
                UrlHandle = blog.UrlHandle
            };

            return Ok(response);
        }
    }
}
