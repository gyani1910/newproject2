using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newproject2.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace newproject2.Models.Domain
{
    public class Tag 
    {
        public Guid ID {get; set;}

        public string Name {get; set;}

        public string DisplayName {get; set;}

        public ICollection<BlogPost> BlogPosts {get; set;}

    }
}