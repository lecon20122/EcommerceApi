using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class UploadService : IApplicationFile
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public UploadService(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task Delete(int id)
        {
            var image = _context.ProductImages.Find(id);

            if (image == null) throw new Exception("image not found");

            var webRootPath = _env.WebRootPath;

            var imagePath = Path.Combine(webRootPath, image.ImageUrl);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                _context.ProductImages.Remove(image);
            }
            else
            {
                throw new Exception("image file not found");
            }
        }

        public string GenerateImagePath(string fileName, string folderPath = "images")
        {

            var fileExt = Path.GetExtension(fileName).ToLower();

            var randomFileName = $"{DateTime.Now.ToFileTime()}{fileExt}";

            return Path.Combine(folderPath, randomFileName);
        }

        public async Task UploadImages(List<IFormFile> files, int productId, string? customFilePath)
        {
            foreach (var file in files)
            {
                if (file.Length <= 0) throw new Exception("no file exists");

                var webRootPath = _env.WebRootPath;

                var ImagePath = GenerateImagePath(file.FileName);

                customFilePath ??= Path.Combine(webRootPath, ImagePath);

                var stream = new FileStream(customFilePath, FileMode.Create);

                await file.CopyToAsync(stream);

                await _context.ProductImages.AddAsync(new ProductImage
                {
                    ImageUrl = ImagePath,
                    ProductId = productId,
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
