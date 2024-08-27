using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newproject2.Models.Domain;
using newproject2.Data;
using Microsoft.EntityFrameworkCore;


namespace newproject2.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbconext appdbconext;
        public TagRepository(AppDbconext appdbconext)
        {
            this.appdbconext = appdbconext;
            
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await appdbconext.Tags.AddAsync(tag);
            await appdbconext.SaveChangesAsync();
            return tag;
        }
        
        public async Task<Tag?> DeleteAsync(Guid ID)
        {
            Tag tag = await appdbconext.Tags.FindAsync(ID);
            if (tag != null)
            {
                appdbconext.Tags.Remove(tag);
                await appdbconext.SaveChangesAsync();
                return tag;
            }
            return null;
        }
        
        public async Task<IEnumerable<Tag>> GetALLAsync()
        {
            return await appdbconext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid ID)
        {
            return await appdbconext.Tags.FindAsync(ID);
        }

        // public async Task<Tag?> UpdateAsync(Tag tag)
        // {
        //     appdbconext.Tags.Update(tag);
        //     await appdbconext.SaveChangesAsync();
        //     return tag;
        // }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await appdbconext.Tags.FindAsync(tag.ID);
            if (existingTag != null)
            {
                existingTag.DisplayName = tag.DisplayName;
                existingTag.Name = tag.Name;
                await appdbconext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

    }
}