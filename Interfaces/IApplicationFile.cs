namespace Ecommerce.Interfaces
{
    public interface IApplicationFile
    {
        Task Upload(List<IFormFile> image, int productId);

        Task Delete(int id);
    }
}
