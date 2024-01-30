using TechTrendTracker.Models.Domain;

namespace TechTrendTracker.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost?> GetByUrlHandleAsync(string urlHanlde);
        Task<BlogPost>AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
