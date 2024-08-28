using Microsoft.AspNetCore.Mvc;
using newproject2.Controllers;
using Microsoft.EntityFrameworkCore; 
using newproject2.Models.ViewModels;
using newproject2.Data;
using newproject2.Models.Domain;
using System.Linq;
using newproject2.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using newproject2.Models.ViewModels;

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

            public async Task<IActionResult> List()
            {

                var blogPosts = await blogPostRepository.GetAllAsync();
                return View(blogPosts);
            }

            [HttpGet]
            public async Task<IActionResult> Edit(Guid ID)
            {
                var blogPost = await blogPostRepository.GetAsync(ID);
                var tagsDomainModel = await tagRepository.GetALLAsync();

                var model = new EditBlogPostRequest
                {
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    shortDescription = blogPost.shortDescription,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandel = blogPost.UrlHandel,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.ID.ToString() }).ToList()
                };
                
                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
            {

                var blogPostDomainModel = new BlogPost
                {
                    ID = editBlogPostRequest.ID,
                    Heading = editBlogPostRequest.Heading,
                    PageTitle = editBlogPostRequest.PageTitle,
                    Content = editBlogPostRequest.Content,
                    shortDescription = editBlogPostRequest.shortDescription,
                    FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                    UrlHandel = editBlogPostRequest.UrlHandel,
                    PublishedDate = editBlogPostRequest.PublishedDate,
                    Author = editBlogPostRequest.Author,
                    Visible = editBlogPostRequest.Visible
                };
                
                var selectedTags = new List<Tag>();
                foreach (var selectedTag in editBlogPostRequest.SelectedTags)
                {
                    if (Guid.TryParse(selectedTag, out var tag))
                    {
                        var foundTag = await tagRepository.GetAsync(tag);

                        if (foundTag != null)
                        {
                            selectedTags.Add(foundTag);
                        }
                    }
                }
                blogPostDomainModel.Tags = selectedTags;
                var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);
                if (updatedBlog != null)
                {
                    return RedirectToAction("List");
                }
                return RedirectToAction("List");
            }

            [HttpPost]
            public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
            {
                var blogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.ID);
                if (blogPost != null)
                {
                    return RedirectToAction("List");
                }
                return RedirectToAction("List");
            }
    }
}
