using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class Fileoperations
    {
        public void SaveFile(List<IFormFile> files, string FileType)
        {
            //subDirectory = subDirectory ?? string.Empty;
            string subDirectory = string.Empty;
            var target = Path.Combine("D:\\webroot\\", subDirectory);

            Directory.CreateDirectory(target);

            files.ForEach(async file =>
            {
                if (file.Length <= 0) return;
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            });
        }
    }
}
