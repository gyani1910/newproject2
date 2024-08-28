using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newproject2.Models.Domain;
using newproject2.Data;
using Microsoft.EntityFrameworkCore;

namespace newproject2.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AppDbconext dbContext;

        public BlogPostRepository(AppDbconext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid ID)
        {
            var exblogPost = await dbContext.BlogPosts.FindAsync(ID);
            if (exblogPost != null)
            {
                dbContext.BlogPosts.Remove(exblogPost);
                await dbContext.SaveChangesAsync();
                return exblogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid ID)
        {
               return await dbContext.BlogPosts.FindAsync(ID);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string UrlHandel)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await dbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.ID == blogPost.ID);

            if (existingBlog != null)
            {
                existingBlog.ID = blogPost.ID;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.shortDescription = blogPost.shortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandel = blogPost.UrlHandel;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await dbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}