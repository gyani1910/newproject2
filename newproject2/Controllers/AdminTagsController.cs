using Microsoft.AspNetCore.Mvc;
using newproject2.Controllers;
using newproject2.Models.ViewModels;
using newproject2.Data;
using newproject2.Models.Domain;
using System.Linq;

namespace newproject2.Controllers
{
    public class AdminTagsController : Controller
    {

        private readonly AppDbconext appdbconext;
        public AdminTagsController(AppDbconext appdbconext)
        {
            this.appdbconext = appdbconext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            appdbconext.Tags.Add(tag);
            appdbconext.SaveChanges();

            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        
        {

            // we have to use the DB context
            var tags = appdbconext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]

        // public IActionResult Edit(Guid id)
        // {

        //     var tag = appdbconext.Tags.Find(id);
        //     if (tag != null)
        //     {
        //         var editTagRequest = new EditTagRequest()
        //         {
        //             ID = tag.ID,
        //             Name = tag.Name,
        //             DisplayName = tag.DisplayName
        //         };
        //         return View(editTagRequest);
        //     }
        //     return View();
            
        // }
        public IActionResult Edit(Guid id)
        {
            var tag = appdbconext.Tags.Find(id);
            return View(tag);

        }

        [HttpPost]

        public IActionResult Edit(Tag tag)
        {
            appdbconext.Tags.Update(tag);
            appdbconext.SaveChanges();
            return RedirectToAction("List");
        }        
    }


}