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

        // GET: /api/blogs/{urlHandle}
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogByUrlhandle([FromRoute] String urlHandle)
        {
            var blog = await blogsRepository.GetByUrlHandleAsync(urlHandle);

            if(blog is null)
            {
                return NotFound();
            }

            var response = new BlogsDTO
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

        // GET: /api/blogs/{id}
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBlogById([FromRoute] Guid id)
        {
            var blog = await blogsRepository.GetByIdAsync(id);

            if(blog is null)
            {
                return NotFound();
            }

            var response = new BlogsDTO
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
            };
            return Ok(response);

        }

        // PUT: /api/blogs/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogById([FromRoute] Guid id, UpdateBlogRequestDTO request)
        {
            var blog = new Blogs
            {
                Id = id,
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

            var updatedBlog = await blogsRepository.UpdateAsync(blog);

            if(updatedBlog is null)
            {
                return NotFound();
            }

            var response = new BlogsDTO
                            {
                Id = updatedBlog.Id,
                Author = updatedBlog.Author,
                Content = updatedBlog.Content,
                FeaturedImageUrl = updatedBlog.FeaturedImageUrl,
                IsVisible = updatedBlog.IsVisible,
                PublishedDate = updatedBlog.PublishedDate,
                ShortDescription = updatedBlog.ShortDescription,
                Title = updatedBlog.Title,
                UrlHandle = updatedBlog.UrlHandle,
                Categories = updatedBlog.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // DEL: /api/blogs/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogById([FromRoute] Guid id)
        {
            var deletedBlog = await blogsRepository.DeleteAsync(id);
            if(deletedBlog is null)
            {
                return NotFound();
            }

            var response = new BlogsDTO
            {
                Id = deletedBlog.Id,
                Author = deletedBlog.Author,
                Content = deletedBlog.Content,
                FeaturedImageUrl = deletedBlog.FeaturedImageUrl,
                IsVisible = deletedBlog.IsVisible,
                PublishedDate = deletedBlog.PublishedDate,
                ShortDescription = deletedBlog.ShortDescription,
                Title = deletedBlog.Title,
                UrlHandle = deletedBlog.UrlHandle
            };

            return Ok(response);
        }
    }
}
