using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using newproject2.Repositories;
using System.Net;

namespace newproject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;

            
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {

            var imageURL =  await imageRepository.UploadAsync(file);
            if (imageURL == null)
            {
                return Problem("Image upload failed" , null , (int)HttpStatusCode.InternalServerError);  
            }
            return new JsonResult(new { link = imageURL });

        }

        
    }
    
}