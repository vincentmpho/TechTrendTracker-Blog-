namespace TechTrendTracker.Repositories.Interface
{
    public interface IImageRepository
    {
         Task<string> UploadAsync(IFormFile file);
    }
}
