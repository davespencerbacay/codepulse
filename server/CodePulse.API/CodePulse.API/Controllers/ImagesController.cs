using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // GET: api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await imageRepository.GetAll();

            var response = new List<BlogImageDTO>();
            foreach(var image in images)
            {
                response.Add(new BlogImageDTO
                {
                    id = image.id,
                    FileName = image.FileName,
                    FileExtension = image.FileExtension,
                    Title = image.Title,
                    Url = image.Url,
                    CreatedAt = image.CreatedAt
                });
            }

            return Ok(response);
        }

        // POST: api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequestDTO request)
        {
            ValidateFileUpload(request.File);

            if (ModelState.IsValid)
            {
                var blogImage = new BlogImage
                {
                    FileName = request.FileName,
                    Title = request.Title,
                    CreatedAt = DateTime.Now,
                    FileExtension = Path.GetExtension(request.File.FileName)
                };

                blogImage = await imageRepository.Upload(request.File, blogImage);

                var response = new BlogImageDTO
                {
                    id = blogImage.id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    CreatedAt = blogImage.CreatedAt
                };
                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
            if(!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format.");
            }

            if(file.Length > 10485760) // 10 MB
            {
                ModelState.AddModelError("file", "File size exceeds the 10 MB limit.");
            }
        }
    }
}
