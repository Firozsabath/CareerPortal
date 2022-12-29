using CUDJobApiIdentity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileServicesController : ControllerBase
    {
        private readonly Fileoperations _fileoperations;
        public FileServicesController(Fileoperations fileoperations)
        {
            _fileoperations = fileoperations;
        }

        [HttpPost("upload")]
        public IActionResult UploadFile([FromForm(Name = "files")] List<IFormFile> files, string subDirectory)
        {
            try
            {
                _fileoperations.SaveFile(files, subDirectory);

                return Ok(new { files.Count });
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}
