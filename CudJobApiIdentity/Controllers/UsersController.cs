using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILoggerService _Logger;

        private readonly IConfiguration _config;

        private readonly IAuthenticationService _authenticationService;

        private readonly IStudentRepository _studentRepository;

        private readonly ICompanyRepository _company;
        private readonly IExternalFunctions _externalFunctions;
        private readonly IEmailConfig _emailConfig;

        public UsersController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            ILoggerService logger, 
            IConfiguration configuration,
            IAuthenticationService authenticationService,
            IStudentRepository studentRepository,
            IEmailConfig emailConfig,
            ICompanyRepository company,
            IExternalFunctions externalFunctions)

        {
            _signInManager = signInManager;
            _userManager = userManager;
            _Logger = logger;
            _config = configuration;
            _authenticationService = authenticationService;
            _studentRepository = studentRepository;
            _company = company;
            _externalFunctions = externalFunctions;
            _emailConfig = emailConfig;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            try
            
            {                
                //var result = "";
                if (userDTO.LoginType == "Student") {
                    var result = _authenticationService.Login(userDTO.EmailID, userDTO.Password);                    
                    if (result != null)
                    {
                        var Student = await _studentRepository.FindBystring(userDTO.EmailID);
                        var tokenstring = await GenerateJsonTokenforstudents(result);
                        var userLog = new Tbl_UserloginlogsDTO { Username = userDTO.EmailID, UserType = userDTO.LoginType, LoginDate = DateTime.Now };
                        var logUpdate = await _externalFunctions.userLogsIn(userLog);
                        if(Student.StudentPersonal != null) { 
                        return Ok(new { token = tokenstring, Id = Student.StudentPersonal.StudentID });
                        }
                        else {
                        return Ok(new { token = tokenstring, Id = 0 });
                        }
                    }
                }
                else if(userDTO.LoginType == "Company") {
                    //EmailDTO email = new EmailDTO { Recepient = "firoz.sabath@cud.ac.ae", Subject = "New Company Registered", CompanyName ="Test", Type = "NewCompany", Name = "Firoz Sabath" };
                    //var emailsend = _emailConfig.SendEmail(email);
                   //InternalError($"{emailsend}");
                    var result = await _signInManager.PasswordSignInAsync(userDTO.EmailID, userDTO.Password, false, false);
                    if (result.Succeeded)
                    {
                        var userLog = new Tbl_UserloginlogsDTO { Username = userDTO.EmailID, UserType = userDTO.LoginType, LoginDate = DateTime.Now };
                        var logUpdate = await _externalFunctions.userLogsIn(userLog);
                        var Company = await _company.FindBystring(userDTO.EmailID);
                        var user = await _userManager.FindByNameAsync(userDTO.EmailID);
                        var tokenstring = await GenerateJsonToken(user);
                        return Ok(new { token = tokenstring, Id = Company.Companies.CompanyID });
                    }
                }
                else if (userDTO.LoginType == "Admin")
                {
                    var result = await _signInManager.PasswordSignInAsync(userDTO.EmailID, userDTO.Password, false, false);
                    if (result.Succeeded)
                    {
                        //var Company = await _company.FindBystring(userDTO.EmailID);                        
                        var user = await _userManager.FindByNameAsync(userDTO.EmailID);
                        var Roles = await _userManager.GetRolesAsync(user);
                        if(Roles.FirstOrDefault() != "Administrator")
                        {
                            return Unauthorized(userDTO);
                        }
                        var userLog = new Tbl_UserloginlogsDTO { Username = userDTO.EmailID, UserType = userDTO.LoginType, LoginDate = DateTime.Now };
                        var logUpdate = await _externalFunctions.userLogsIn(userLog);
                        var tokenstring = await GenerateJsonToken(user);
                        return Ok(new { token = tokenstring, Id = 1 });
                    }
                }
                //if (result.Email == userDTO.EmailID)                
                return Unauthorized(userDTO);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userDTO)
        {
            try
            {
                //var result = "";
                if (userDTO != null)
                {
                    if (await _userManager.FindByEmailAsync(userDTO.EmailID) == null)
                    {
                        var user = new IdentityUser
                        {
                            UserName = userDTO.EmailID,
                            Email = userDTO.EmailID
                        };
                        var result = await _userManager.CreateAsync(user, userDTO.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Employer");
                            return Ok(user.Email);
                        }
                        return BadRequest(result.Errors);
                    }
                    else
                    {
                        return Conflict();
                    }
                }               
                //if (result.Email == userDTO.EmailID)                
                return Unauthorized(userDTO);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        [AllowAnonymous]
        [HttpPost("GeneratepasswordToken")]
        public async Task<IActionResult> GeneratepasswordToken([FromBody] PasswordReset data)
        {
            string token = string.Empty;

            var user = await _userManager.FindByEmailAsync(data.Email);
            if(user != null)
            {
                token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return Ok(token);
            }
            else
            {
                return NotFound(data);
            }

           // return Ok(token);
        }


        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] Passwordconfirmation userdata)
        {
            try
            {
                //var result = "";
                if (userdata != null)
                {
                    var user = await _userManager.FindByEmailAsync(userdata.Email);
                    if (user != null)
                    {
                        //var user = new IdentityUser
                        //{
                        //    UserName = userdata.Email,
                        //    Email = userdata.Email
                        //};
                        var result = await _userManager.ResetPasswordAsync(user,userdata.token, userdata.Password);
                        if (result.Succeeded)
                        {                        
                            return Ok(user.Email);
                        }
                        else
                        {
                            return BadRequest(result.Errors);
                        }                        
                    }
                    else
                    {
                        return Conflict();
                    }
                }                          
                return Unauthorized(userdata);
            }
            catch (Exception Ex)
            {
                return InternalError($"{Ex.Message} - {Ex.InnerException}");
            }
        }

        private async Task<string> GenerateJsonToken(IdentityUser user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
             var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(Roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(_config["JWT:ValidIssuer"]
                ,_config["JWT:ValidAudience"]
                ,claims
                ,null
                ,expires:DateTime.Now.AddHours(8)
                ,signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<string> GenerateJsonTokenforstudents(CUDJobAPiIdentity.Ldap.IAppUser user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Username)
            };
            IList<string> Roles = new List<string> { "Student" };
            claims.AddRange(Roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(_config["JWT:ValidIssuer"]
                , _config["JWT:ValidAudience"]
                , claims
                , null
                , expires: DateTime.Now.AddHours(8)
                , signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ObjectResult InternalError(string message)
        {
            _Logger.LogError(message);
            return StatusCode(500, "Somethin went wrong. Please contact the administrator.");
        }
    }
}
