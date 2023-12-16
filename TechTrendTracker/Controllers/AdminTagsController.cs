using Microsoft.AspNetCore.Mvc;
using TechTrendTracker.Models.ViewModels;

namespace TechTrendTracker.Controllers
{
    public class AdminTagsController : Controller
    {

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
       public IActionResult Add(AddTagRequest  addTagRequest)
        {
            var name = addTagRequest.Name;
            var displayName= addTagRequest.DisplayName;

            return View("Add");
        }
    }
}
