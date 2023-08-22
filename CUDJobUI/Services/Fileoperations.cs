using CudJobUI.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.Services
{
    public class Fileoperations : IFileoperations
    {
        public Fileoperations(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IWebHostEnvironment _env { get; }

        public string SaveFile(IFormFile  file, string FileType)
        {            
            string target = string.Empty;           
            if (FileType == "StudentProfile") { 
            target = _env.WebRootPath + "\\Images\\Student\\profile";
            }
            else if (FileType == "StudentResume")
            {
                target = _env.WebRootPath + "\\Images\\Student\\Resume";
            }
            else if (FileType == "StudentCertificate")
            {
                target = _env.WebRootPath + "\\Images\\Student\\certificate";
            }
            else if (FileType == "CompanyProfile")
            {
                target = _env.WebRootPath + "\\Images\\Company\\profile";
            }
            else if (FileType == "CompanyResume")
            {
                target = _env.WebRootPath + "\\Images\\Company\\certificate";
            }
            else if (FileType == "JobDocs")
            {
                target = _env.WebRootPath + "\\Images\\Jobs\\docs";
            }

            if (!Directory.Exists(target))
            { 
                Directory.CreateDirectory(target);
            }


            try
            {
                string filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                if (file.Length <= 0) return "error";
                string filePath = Path.Combine(target, filename);              
                using (var stream = new FileStream(filePath, FileMode.Create))
                {                    
                    file.CopyTo(stream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                return $"{ex.Message} - {ex.InnerException}";
                throw;
            }
           
        }
    }
}
