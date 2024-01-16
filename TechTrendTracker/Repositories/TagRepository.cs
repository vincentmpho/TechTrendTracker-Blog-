using Microsoft.EntityFrameworkCore;
using TechTrendTracker.Data;
using TechTrendTracker.Models.Domain;
using TechTrendTracker.Models.ViewModels;
using TechTrendTracker.Repositories.Interface;

namespace TechTrendTracker.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }
        public async Task<Tag> AddAync(Tag tag)
        {
            await _bloggieDbContext.Tags.AddAsync(tag);
            await _bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAync(Guid id)
        {
            var existingTag = await _bloggieDbContext.Tags.FindAsync(id);

            //check

            if (existingTag !=null)
            {
                _bloggieDbContext.Tags.Remove(existingTag);
                await _bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public  async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _bloggieDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
              return _bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateAync(Tag tag)
        {
            var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save the changes
                await _bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
