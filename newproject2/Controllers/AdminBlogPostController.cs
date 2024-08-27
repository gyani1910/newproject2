using Microsoft.AspNetCore.Mvc;
using newproject2.Controllers;
using Microsoft.EntityFrameworkCore; 
using newproject2.Models.ViewModels;
using newproject2.Data;
using newproject2.Models.Domain;
using System.Linq;
using newproject2.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace newproject2.Controllers
{
    public class AdminBlogPostController:  Controller
    {

        private readonly ITagRepository tagRepository ;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository , IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Add()


        {
            var tags = await tagRepository.GetALLAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.ID.ToString() }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogPost = new BlogPost{
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                shortDescription = addBlogPostRequest.shortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandel = addBlogPostRequest.UrlHandel,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible
            };

            var selectedTags = new List<Tag>();

            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var tag = await tagRepository.GetAsync(Guid.Parse(selectedTagId));
                if (tag != null)
                {
                    selectedTags.Add(tag);
                }
            }
            blogPost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }
    }
}