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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost?> GetAsync(Guid ID)
        {
            throw new NotImplementedException();    
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string UrlHandel)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();

        }
    }
}