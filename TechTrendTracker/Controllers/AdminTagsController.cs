using Microsoft.AspNetCore.Mvc;
using TechTrendTracker.Data;
using TechTrendTracker.Models.Domain;
using TechTrendTracker.Models.ViewModels;

namespace TechTrendTracker.Controllers
{
    public class AdminTagsController : Controller
    {

        private BloggieDbContext bloggieDbContext;

        public AdminTagsController (BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag Domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName
             };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            //use dbContext to read the tags

           var tags = bloggieDbContext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]

        public IActionResult Edit(Guid id)
        {
            return View();
        }
    }
}
