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

            tagRepository.AddAsync(tag);


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
            tagRepository.UpdateAsync(tag);
            return RedirectToAction("List");
        }        

        [HttpPost]
        public async Task<IActionResult> Delete(Guid ID)
        {
            tagRepository.DeleteAsync(ID);
            return RedirectToAction("List");
        }
    }


}