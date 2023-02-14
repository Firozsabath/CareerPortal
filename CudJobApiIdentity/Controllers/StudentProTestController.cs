using CUDJobAPiIdentity.VIewModels;
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
    public class StudentProTestController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateProfile([FromBody] studentProfilephase1 data)
        {

            return Ok();
        }
    }
}
