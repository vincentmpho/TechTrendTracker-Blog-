using Microsoft.AspNetCore.Mvc;
using TechTrendTracker.Repositories.Interface;

namespace TechTrendTracker.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task <IActionResult> Index(String urlHandle)
        {

           var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlHandle);

            return View(blogPost);
        }
    }
}
