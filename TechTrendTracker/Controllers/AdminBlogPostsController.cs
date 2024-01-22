using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechTrendTracker.Models.Domain;
using TechTrendTracker.Models.ViewModels;
using TechTrendTracker.Repositories.Interface;

namespace TechTrendTracker.Controllers
{
    public class AdminBlogPostsController : Controller 
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }
        [HttpGet]
       public async Task< IActionResult> Add()
        {
            //get tags from repository

            var tags = await _tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Map view model to domain moel

            var blogpostModel = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeauredImageUrl = addBlogPostRequest.FeauredImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };
            
            //Map Tags  from selected tags

            var selectedTags = new List<Tag>();

            foreach(var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsId = Guid.Parse(selectedTagId);
               var existingTag = await _tagRepository.GetAsync(selectedTagIdAsId);

                //check

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            //Mapping tags back to domain model
            blogpostModel.Tags = selectedTags;

            await _blogPostRepository.AddAsync(blogpostModel);
            return RedirectToAction("Add");
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            //Call the Repository
             var blogPosts= await _blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }
    }
}
