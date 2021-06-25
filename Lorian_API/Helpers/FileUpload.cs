using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Helpers
{
    public class FileUpload
    {
        public IFormFile file { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
