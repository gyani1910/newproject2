using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using newproject2.Data;
using newproject2.Models.Domain;


namespace newproject2.Data
{
    public class AppDbconext: DbContext 
    {
        public AppDbconext(DbContextOptions<AppDbconext> options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts {get; set;}
        public DbSet<Tag> Tags {get; set;}

        
    }
}