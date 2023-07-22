using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class ImageService : IApplicationFile
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public ImageService(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Upload(List<IFormFile> images, int productId)
        {
            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    var fileExt = Path.GetExtension(image.FileName).ToLower();

                    var randomFileName = $"{DateTime.Now.ToFileTime()}{fileExt}";

                    var webRootPath = _env.WebRootPath;

                    var filePath = Path.Combine(webRootPath, "images", randomFileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    var newProductImage = new ProductImage
                    {
                        ProductId = productId,
                        ImageUrl = filePath,
                    };

                    await _context.ProductImages.AddAsync(newProductImage);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
