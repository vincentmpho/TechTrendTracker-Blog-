using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public  IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task <IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag Domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName
             };

           await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use dbContext to read the tags

           var tags = await bloggieDbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task <IActionResult> Edit(Guid id)
        {
            var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task <IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var existingTag =  await bloggieDbContext.Tags.FindAsync(editTagRequest.Id);

            if (existingTag != null)
            {
                existingTag.Name = editTagRequest.Name;
                existingTag.DisplayName = editTagRequest.DisplayName;

                //save the changes
                 await bloggieDbContext.SaveChangesAsync();
                 
                //show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }

            //Show Failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = await bloggieDbContext.Tags.FindAsync(editTagRequest.Id);

            if (tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
               await bloggieDbContext.SaveChangesAsync();

                // show a success notification
                return RedirectToAction("List");
            }

            // show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
