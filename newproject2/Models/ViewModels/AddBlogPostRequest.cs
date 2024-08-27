using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using newproject2.Models;


namespace newproject2.Models.ViewModels
{
    public class AddBlogPostRequest
    {

        public string Heading {get ; set;}

        public string PageTitle {get ; set;}

        public string Content {get ; set;}

        public string shortDescription {get; set;}

        public string FeaturedImageUrl {get; set;}

        public string UrlHandel {get; set;}

        public DateTime PublishedDate {get; set;}

        public string Author {get; set;}

        public bool Visible {get; set;}
        // This is a commond from ASp.net core , so nothing Fancy here 
    //Display the list of tags in the dropdown
        public IEnumerable<SelectListItem> Tags {get; set;}

        // COLLECT TAG HERE
        public string[] SelectedTags {get; set;} = Array.Empty<string>();

        
    }
}