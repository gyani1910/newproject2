using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newproject2.Models;

namespace newproject2.Models.ViewModels
{
    public class EditTagRequest
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public string DisplayName {get; set;}
    }
}