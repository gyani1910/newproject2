using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newproject2.Models.Domain;

using Microsoft.AspNetCore.Mvc.Rendering;






namespace newproject2.Repositories
{
    public interface ITagRepository

    {
        //Basically this is there only to define the defination of the methods not the logic of the methods
       Task<IEnumerable <Tag>> GetALLAsync();
       Task<Tag?> GetAsync(Guid ID);

       Task<Tag> AddAsync(Tag tag);

       Task<Tag?> UpdateAsync(Tag tag);

       Task<Tag?> DeleteAsync(Guid ID);
    }
}