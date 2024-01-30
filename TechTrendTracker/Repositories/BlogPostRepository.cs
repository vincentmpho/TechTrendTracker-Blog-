using Microsoft.EntityFrameworkCore;
using TechTrendTracker.Data;
using TechTrendTracker.Models.Domain;
using TechTrendTracker.Repositories.Interface;

namespace TechTrendTracker.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _bloggieDbContext.AddAsync(blogPost);
            await _bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await _bloggieDbContext.BlogPosts.FindAsync(id);

            //check if is there
            if (existingBlog != null)
            {
                _bloggieDbContext.BlogPosts.Remove(existingBlog);
                await _bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
                                                  
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await _bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
           return  await _bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHanlde)
        {
            return  await _bloggieDbContext.BlogPosts
                .Include(X =>X.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHanlde);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
              var existingBlog=await  _bloggieDbContext.BlogPosts.Include(x=> x.Tags)
                .FirstOrDefaultAsync(x=>x.Id ==blogPost.Id);

            //check

            if (existingBlog !=null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeauredImageUrl = blogPost.FeauredImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;


                await _bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
