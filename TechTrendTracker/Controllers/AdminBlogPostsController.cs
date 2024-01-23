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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Retrieve the reult from the repository
            var blogPost = await _blogPostRepository.GetAsync(id);
            var tagsDomainModel = await _tagRepository.GetAllAsync();

            //check
            if (blogPost != null)
            {
                //Map the Domain model into the view Model
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeauredImageUrl = blogPost.FeauredImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()

                };
                return View(model);

            }
            else
            //Pass data to view
            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //Map view Model back to Domain Model
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeauredImageUrl = editBlogPostRequest.FeauredImageUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible,
            };

            //Map tags into Domain Model
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTags)

               
            {
                if (Guid.TryParse(selectedTag,out var tag))
                {
                     var foundTag  =await _tagRepository.GetAsync(tag);

                    //check

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;

            //Submit infomation to repo to update
             var updatedBlog=await _blogPostRepository.UpdateAsync(blogPostDomainModel);

            //check
            if (updatedBlog != null)
            {
                //Show success notification
                return RedirectToAction("Edit");
            }

            //Show error notification
            return RedirectToAction("Edit");
        }
    }
}
