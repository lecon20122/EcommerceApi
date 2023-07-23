namespace Ecommerce.Interfaces
{
    public interface IApplicationFile
    {
        Task UploadImages(List<IFormFile> files, int productId, string? customFilePath);

        Task Delete(int id);
    }
}
