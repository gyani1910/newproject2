using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newproject2.Repositories
{
    public interface IImageRepository
    {

        Task<string> UploadAsync(IFormFile file);
        
    }
}