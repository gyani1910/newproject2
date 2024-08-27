using Microsoft.AspNetCore.Mvc;
using newproject2.Controllers;
using Microsoft.EntityFrameworkCore; 
using newproject2.Models.ViewModels;
using newproject2.Data;
using newproject2.Models.Domain;
using System.Linq;
using newproject2.Repositories;

namespace newproject2.Controllers
{
    public class AdminTagsController : Controller
    {

        private readonly ITagRepository tagRepository;

        
        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            Tag tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);


            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        
        {

            // we have to use the DB context
            var tags = await tagRepository.GetALLAsync();

            return View(tags);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid ID)
        {
            var tag = await tagRepository.GetAsync(ID);
            return View(tag);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Tag tag)
        {
            await tagRepository.UpdateAsync(tag);
            return RedirectToAction("List");
        }        

        [HttpPost]
        public async Task<IActionResult> Delete(Guid ID)
        {
            await tagRepository.DeleteAsync(ID);
            return RedirectToAction("List");
        }
    }


}


// using Microsoft.AspNetCore.Mvc;
// using newproject2.Controllers;
// using newproject2.Models.ViewModels;
// using newproject2.Data;
// using newproject2.Models.Domain;
// using System.Linq;

// namespace newproject2.Controllers
// {
//     public class AdminTagsController : Controller
//     {

//         private readonly AppDbconext appdbconext;
//         public AdminTagsController(AppDbconext appdbconext)
//         {
//             this.appdbconext = appdbconext;
//         }

//         [HttpGet]
//         public IActionResult Add()
//         {
//             return View(); 
//         }

//         [HttpPost]
//         public IActionResult Add(AddTagRequest addTagRequest)
//         {
//             var tag = new Tag()
//             {
//                 Name = addTagRequest.Name,
//                 DisplayName = addTagRequest.DisplayName
//             };

//             appdbconext.Tags.Add(tag);
//             appdbconext.SaveChanges();

//             return RedirectToAction("List");
//         }
//         [HttpGet]
//         public IActionResult List()
        
//         {

//             // we have to use the DB context
//             var tags = appdbconext.Tags.ToList();

//             return View(tags);
//         }

//         [HttpGet]
//         public IActionResult Edit(Guid id)
//         {
//             var tag = appdbconext.Tags.Find(id);
//             return View(tag);

//         }

//         [HttpPost]

//         public IActionResult Edit(Tag tag)
//         {
//             appdbconext.Tags.Update(tag);
//             appdbconext.SaveChanges();
//             return RedirectToAction("List");
//         }    

//         [HttpPost] 
//         public IActionResult Delete(Guid id)
//         {
//             var tag = appdbconext.Tags.Find(id);
//             appdbconext.Tags.Remove(tag);
//             appdbconext.SaveChanges();
//             return RedirectToAction("List");   
//         }
//     }


// }