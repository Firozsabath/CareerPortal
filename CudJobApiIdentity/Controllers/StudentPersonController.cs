using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.DTOs;
using CUDJobAPiIdentity.VIewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CUDJobApiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentPersonController : ControllerBase
    {
        public IStudentPersnlRepository _studentrep { get; }
        public ILoggerService _Logger { get; }

        public StudentPersonController(IStudentPersnlRepository studentRepository, ILoggerService Logger)
        {
            _studentrep = studentRepository;
            _Logger = Logger;
        }
        // GET: api/<StudentPersonController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("isCreated/{Email}")]
        public async Task<bool> studentExists(string Email)
        {
            return await _studentrep.IstudentCreated(Email);
            //return new string[] { "value1", "value2" };
            //return Ok(isExists);
        }

        // GET api/<StudentPersonController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentPersonController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("UpdatePersonal/{id}")]
        public async Task<IActionResult> UpdateStudentPersonal(int id, [FromBody] StudentPersonalview Stddetails)
        {
            try
            {
                if (id < 1 || Stddetails == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateStudentPersonal(Stddetails);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPost("AddEducation/{id}")]
        public async Task<IActionResult> AddStudentEducation(int id, [FromBody] IList<StudentEducationDTO> Stddetails)
        {
            try
            {
                if (id < 1 || Stddetails == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.AddEducation(Stddetails);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        // PUT api/<StudentPersonController>/5
        [HttpPut("UpdateEducation/{id}")]
        public async Task<IActionResult> UpdateStudentEducation(int id, [FromBody] IList<StudentEducationDTO>  Stddetails)
        {
            try
            {
                if (id < 1 || Stddetails == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateEducation(Stddetails);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPost("AddExperience/{id}")]
        public async Task<IActionResult> AddStudentExperience(int id, [FromBody] IList<StudentExperienceDTO> StdExperience)
        {
            try
            {
                if (id < 1 || StdExperience == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.AddStudentExperience(StdExperience);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("UpdateExperience/{id}")]
        public async Task<IActionResult> UpdateStudentExperience(int id, [FromBody] IList<StudentExperienceDTO> StdExperience)
        {
            try
            {
                if (id < 1 || StdExperience == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateStudentExperience(StdExperience);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }


        [HttpPost("AddVolunteerExperience/{id}")]
        public async Task<IActionResult> AddVolunteerExperience(int id, [FromBody] IList<StudentVolunteerExperienceDTO> StdExperience)
        {
            try
            {
                if (id < 1 || StdExperience == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.AddVolunteerExperience(StdExperience);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("UpdateVolunteerExperience/{id}")]
        public async Task<IActionResult> UpdateVolunteerExperience(int id, [FromBody] IList<StudentVolunteerExperienceDTO> StdExperience)
        {
            try
            {
                if (id < 1 || StdExperience == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateVolunteerExperience(StdExperience);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPost("AddPortfolio/{id}")]
        public async Task<IActionResult> AddPortfolio(int id, [FromBody] IList<StudentPortfolioDTO> Stdportfolio)
        {
            try
            {
                if (id < 1 || Stdportfolio == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.Addportfolio(Stdportfolio);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPut("UpdatePortfolio/{id}")]
        public async Task<IActionResult> UpdatePortfolio(int id, [FromBody] IList<StudentPortfolioDTO> Stdportfolio)
        {
            try
            {
                if (id < 1 || Stdportfolio == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.Updateportfolio(Stdportfolio);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [HttpPost("AddMemberships/{id}")]
        public async Task<IActionResult> AddStudentMemberships(int id, [FromBody] IList<MembershipDTO> StdMemberships)
        {
            try
            {
                if (id < 1 || StdMemberships == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.AddStudentMemberships(StdMemberships);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPut("UpdateMemberships/{id}")]
        public async Task<IActionResult> UpdateStudentMemberships(int id, [FromBody] IList<MembershipDTO> StdMemberships)            
        {
            try
            {
                if (id < 1 || StdMemberships == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateStudentMemberships(StdMemberships);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPost("AddProjects/{id}")]
        public async Task<IActionResult> AddStudentProjects(int id, [FromBody] IList<ProjectDTO> StdProjects)
        {
            try
            {
                if (id < 1 || StdProjects == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.AddStudentProjectss(StdProjects);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPut("UpdateProjects/{id}")]
        public async Task<IActionResult> UpdateStudentProjects(int id, [FromBody] IList<ProjectDTO> StdProjects)
        {
            try
            {
                if (id < 1 || StdProjects == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateStudentProjectss(StdProjects);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPost("AddAwards/{id}")]
        public async Task<IActionResult> AddStudentAwards(int id, [FromBody] IList<AwardsDTO> StdAwards)
        {
            try
            {
                if (StdAwards == null)
                {
                    _Logger.LogWarn($"An empty Student Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarn($"StudentDetails was incomplete.");
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.AddStudentAwards(StdAwards);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPut("UpdateAwards/{id}")]
        public async Task<IActionResult> UpdateStudentAwards(int id, [FromBody] IList<AwardsDTO> StdAwards)
        {
            try
            {
                if (id < 1 || StdAwards == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateStudentAwards(StdAwards);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }     
       

        [HttpPost("AddLanguages/{id}")]
        public async Task<IActionResult> AddStudentLanguages(int id, [FromBody] IList<LanguagesDTO> StdLanguages)
        {
            try
            {
                if (StdLanguages == null)
                {
                    _Logger.LogWarn($"An empty Student Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarn($"StudentDetails was incomplete.");
                    return BadRequest(ModelState);
                }                
                var isScuccess = await _studentrep.AddStudentLanguages(StdLanguages);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPut("UpdateLanguages/{id}")]
        public async Task<IActionResult> UpdateStudentLanguages(int id, [FromBody] IList<LanguagesDTO> StdLanguages)
        {
            try
            {
                if (id < 1 || StdLanguages == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateStudentLanguages(StdLanguages);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }


        [HttpPost("AddStudentWorkAvailabilty/{id}")]
        public async Task<IActionResult> AddStudentWrokAvailabilty(int id, [FromBody] IList<StudentWorkAvailabilityDTO> StdAvailability)
        {
            try
            {
                if (StdAvailability == null)
                {
                    _Logger.LogWarn($"An empty Student Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarn($"StudentDetails was incomplete.");
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.AddStudentAvailability(StdAvailability);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPut("UpdateStudentWorkAvailabilty/{id}")]
        public async Task<IActionResult> UpdateStudentWrokAvailabilty(int id, [FromBody] IList<StudentWorkAvailabilityDTO> StdAvailability)
        {
            try
            {
                if (id < 1 || StdAvailability == null)
                {
                    return BadRequest();
                }
                var isExists = await _studentrep.isExists(id);
                if (!isExists)
                {
                    _Logger.LogWarn($"Student with id : {id} was not found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateStudentAvailability(StdAvailability);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPost("AddStudentSkillSet/{id}")]
        public async Task<IActionResult> AddStudentSkillSet(int id, [FromBody] SkillSetVM StdAvailability)
        {
            try
            {
                if (StdAvailability == null)
                {
                    _Logger.LogWarn($"An empty Student Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarn($"StudentDetails was incomplete.");
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.AddStudentSkills(StdAvailability);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        [HttpPut("UpdateStudentComputerSkills/{id}")]
        public async Task<IActionResult> UpdateStudentComputerSkills(int id, [FromBody] StudentComputerSkillsDTO StdCmpSkill)
        {
            try
            {
                if (StdCmpSkill == null)
                {
                    _Logger.LogWarn($"An empty Student Creation request has been send.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _Logger.LogWarn($"StudentDetails was incomplete.");
                    return BadRequest(ModelState);
                }
                var isScuccess = await _studentrep.UpdateStudentComputerSkill(StdCmpSkill);
                if (!isScuccess)
                {
                    return InternalError($"Something went wrong during update!");
                }
                return NoContent();
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }

        }

        // DELETE api/<StudentPersonController>/5
        [HttpDelete("{id}/{Modules}")]
        public async Task<IActionResult> DeleteStudentmodules(int id,string Modules)
        {
            try
            {
                if(Modules == "Education")
                {
                    if (id < 1)
                    {
                        _Logger.LogWarn($"There is no proper id Passed.");
                        return BadRequest();
                    }
                    //var isExists = await _studentrep.isExists(id);
                    //if (!isExists)
                    //{
                    //    _Logger.LogWarn($"Student with id : {id} was not found.");
                    //    return NotFound();
                    //}
                    var studentmod = await _studentrep.FindbyEduID(id);
                    var isScuccess = await _studentrep.DeleteEducation(studentmod);
                    if (!isScuccess)
                    {
                        _Logger.LogWarn($"Student deletion failed .");
                        return InternalError($"Something went wrong during update!");
                    }
                    return NoContent();
                }
                if (Modules == "Experience")
                {
                    if (id < 1)
                    {
                        _Logger.LogWarn($"There is no proper id Passed.");
                        return BadRequest();
                    }
                    var studentmod = await _studentrep.FindbyExpID(id);
                    var isScuccess = await _studentrep.DeleteStudentExperience(studentmod);
                    if (!isScuccess)
                    {
                        _Logger.LogWarn($"Student deletion failed .");
                        return InternalError($"Something went wrong during update!");
                    }
                    return NoContent();
                }
                if (Modules == "Memberships")
                {
                    if (id < 1)
                    {
                        _Logger.LogWarn($"There is no proper id Passed.");
                        return BadRequest();
                    }
                    var studentmod = await _studentrep.FindbyMembershipID(id);
                    var isScuccess = await _studentrep.DeleteStudentMemberships(studentmod);
                    if (!isScuccess)
                    {
                        _Logger.LogWarn($"Student deletion failed .");
                        return InternalError($"Something went wrong during update!");
                    }
                    return NoContent();
                }
                if (Modules == "Projects")
                {
                    if (id < 1)
                    {
                        _Logger.LogWarn($"There is no proper id Passed.");
                        return BadRequest();
                    }
                    var studentmod = await _studentrep.FindbyProjectsID(id);
                    var isScuccess = await _studentrep.DeleteStudentProjectss(studentmod);
                    if (!isScuccess)
                    {
                        _Logger.LogWarn($"Student deletion failed .");
                        return InternalError($"Something went wrong during update!");
                    }
                    return NoContent();
                }
                if (Modules == "Awards")
                {
                    if (id < 1)
                    {
                        _Logger.LogWarn($"There is no proper id Passed.");
                        return BadRequest();
                    }
                    var studentmod = await _studentrep.FindbyAwardID(id);
                    var isScuccess = await _studentrep.DeleteStudentAwards(studentmod);
                    if (!isScuccess)
                    {
                        _Logger.LogWarn($"Student deletion failed .");
                        return InternalError($"Something went wrong during update!");
                    }
                    return NoContent();
                }
                if (Modules == "Languages")
                {
                    if (id < 1)
                    {
                        _Logger.LogWarn($"There is no proper id Passed.");
                        return BadRequest();
                    }
                    var studentmod = await _studentrep.FindbyLanguageID(id);
                    var isScuccess = await _studentrep.DeleteStudentLanguages(studentmod);
                    if (!isScuccess)
                    {
                        _Logger.LogWarn($"Student deletion failed .");
                        return InternalError($"Something went wrong during update!");
                    }
                    return NoContent();
                }
                if (Modules == "Availability")
                {
                    if (id < 1)
                    {
                        _Logger.LogWarn($"There is no proper id Passed.");
                        return BadRequest();
                    }
                    var studentmod = await _studentrep.FindbyAvailabilityID(id);
                    var isScuccess = await _studentrep.DeleteStudentAvailability(studentmod);
                    if (!isScuccess)
                    {
                        _Logger.LogWarn($"Student deletion failed .");
                        return InternalError($"Something went wrong during update!");
                    }
                    return NoContent();
                }
                if (Modules == "HardSkill" || Modules == "SoftSkill" || Modules == "ComputerSkill")
                {
                    if (id < 1)
                    {
                        _Logger.LogWarn($"There is no proper id Passed.");
                        return BadRequest();
                    }
                    var studentmodexists = await _studentrep.isSkillExists(id,Modules);
                    bool isScuccess = true;
                    if (studentmodexists) { 
                        isScuccess = await _studentrep.DeleteStudentSkills(id, Modules);
                    }
                    if (!isScuccess)
                    {
                        _Logger.LogWarn($"Student deletion failed.");
                        return InternalError($"Something went wrong during update!");
                    }
                    return NoContent();
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                return InternalError($"{ex.Message} - {ex.InnerException}");
            }
        }

        private ObjectResult InternalError(string message)
        {
            _Logger.LogError(message);
            return StatusCode(500, "Somethin went wrong. Please contact the administrator.");
        }
    }
}
