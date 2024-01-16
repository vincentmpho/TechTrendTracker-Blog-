using System.Collections;
using TechTrendTracker.Models.Domain;

namespace TechTrendTracker.Repositories.Interface
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>>GetAllAsync();
        Task<Tag?>GetAsync(Guid id);
        Task<Tag>AddAync(Tag tag);
        Task<Tag?>UpdateAync(Tag tag);
        Task<Tag?> DeleteAync(Guid id);
    }

}
