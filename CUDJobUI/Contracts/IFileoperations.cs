using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.Contracts
{
    public interface IFileoperations
    {
        public string SaveFile(IFormFile file, string FileType);
    }
}
