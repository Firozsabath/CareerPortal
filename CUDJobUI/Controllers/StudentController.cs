using CudJobUI.Contracts;
using CudJobUI.Data;
using CudJobUI.Services;
using CudJobUI.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmartBreadcrumbs.Attributes;
// using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CudJobUI.Controllers
{
    //[SessionTimeout]
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStaticEndPoints _endPoints;
        private readonly IFileoperations _fileoperations;
        private readonly IWebHostEnvironment _env;
        private readonly ICustomFunctions _CustFunction;

        public StudentController(IConfiguration configuration, IStaticEndPoints endPoints
            , IFileoperations fileoperations
            , IWebHostEnvironment env
            ,ICustomFunctions CustFunction
            )
        {
            _configuration = configuration;
            _endPoints = endPoints;
            _fileoperations = fileoperations;
            _CustFunction = CustFunction;
        }

        // GET: StudentController
        public async Task<IActionResult> Index()
        {
            List<StudentProfile> Studentlist = new List<StudentProfile>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Studentlist = JsonConvert.DeserializeObject<List<StudentProfile>>(apiResponse);
                }
            }
            return View(Studentlist);
        }

       // [DefaultBreadcrumb("My home")]
        // GET: StudentController/Details/5
        public async Task<IActionResult> Profile(int id)
        {         

            if (id == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            StudentProfile studentprofile = new StudentProfile();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    studentprofile = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                }
            }            
            //if (studentprofile.StudentPersonal.UpdatedDate != null)
            //{
            //    HttpContext.Session.SetString("Lastupdated", _CustFunction.Lastupdated(DateTime.Now, Convert.ToDateTime(studentprofile.StudentPersonal.UpdatedDate)));
            //    ViewBag.Lastupdated = _CustFunction.Lastupdated(DateTime.Now, Convert.ToDateTime(studentprofile.StudentPersonal.UpdatedDate));
            //    //ViewBag.updatedtime = _CustFunction.Converttolastupdated(Convert.ToDateTime(studentprofile.StudentPersonal.UpdatedDate));
            //}
            //if (studentprofile.StudentPersonal.Resumepath != null)
            //{
            //    HttpContext.Session.SetString("ResumeName", Path.GetFileName(studentprofile.StudentPersonal.Resumepath));
            //    ViewBag.ResumeName = Path.GetFileName(studentprofile.StudentPersonal.Resumepath);
            //}
            //if (studentprofile.StudentPersonal.profileImgpath != null && studentprofile.StudentPersonal.profileImgpath != string.Empty)
            //{
            //    HttpContext.Session.SetString("ProfileImg", Path.GetFileName(studentprofile.StudentPersonal.profileImgpath));
            //    ViewBag.ProfileImg = Path.GetFileName(studentprofile.StudentPersonal.profileImgpath);
            //}
            //HttpContext.Session.SetString("usrFirstName", studentprofile.StudentPersonal.FirstName);
            //HttpContext.Session.SetString("usrLastName", studentprofile.StudentPersonal.LastName);

            return View(studentprofile);
        }

        [HttpGet("Profileforemployee/{id}/{jobid?}/{applyid?}")]
        public async Task<IActionResult> Profileforemployee(int id, string jobid = null, string applyid = null)
        {
            if(!String.IsNullOrEmpty(jobid) && !String.IsNullOrEmpty(applyid)){
                AppliedJobs AppliedJobs = new AppliedJobs { ID = Convert.ToInt32(applyid), jobID = Convert.ToInt32(jobid), Description = "", StatusID = Convert.ToInt32(9) };
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.jobApplicationEndpoints + "/UpdateJobStatus/" + applyid, AppliedJobs))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            

            StudentProfile studentprofile = new StudentProfile();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    studentprofile = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                }
            }

            if (studentprofile.StudentPersonal.Resumepath != null)
            {
                ViewBag.ResumeName = Path.GetFileName(studentprofile.StudentPersonal.Resumepath);
            }
            if (studentprofile.StudentPersonal.profileImgpath != null && studentprofile.StudentPersonal.profileImgpath != string.Empty)
            {
                ViewBag.ProfileImg = Path.GetFileName(studentprofile.StudentPersonal.profileImgpath);
            }
            return View(studentprofile);
        }

        public async Task<IActionResult> Refeerence(Student_Cams studentdetaisl)
        {
            var std = TempData["student"].ToString();
            var CudStdDetails = JsonConvert.DeserializeObject<StudentDetails_Cams>(std);
            ViewBag.Nostud = false;
            if (String.IsNullOrEmpty(CudStdDetails.studentdetails.CudStudentID))
            {
                ViewBag.Nostud = true;
            }
            return View(CudStdDetails);
        }

        // GET: StudentController/Create
        public async Task<IActionResult> Create(string id)
        {
            StudentProfile studentprofile = new StudentProfile();
            List<CountryCode> cl = new List<CountryCode>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCountry"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cl = JsonConvert.DeserializeObject<List<CountryCode>>(apiResponse);
                }
            }
            cl.Insert(0, new CountryCode { CountryID = "", Description = "--Select Country Name--" });
            ViewBag.message = cl;

            List<Hardskills> Hskills = new List<Hardskills>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetHardSkills"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Hskills = JsonConvert.DeserializeObject<List<Hardskills>>(apiResponse);
                }
            }           
            ViewBag.Hskills = Hskills;           

            List<Softskills> Sskills = new List<Softskills>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetSoftSkills"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Sskills = JsonConvert.DeserializeObject<List<Softskills>>(apiResponse);
                }
            }            
            ViewBag.Sskills = Sskills;

            List<Computerskills> Cskills = new List<Computerskills>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetComputerSkills"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Cskills = JsonConvert.DeserializeObject<List<Computerskills>>(apiResponse);
                }
            }
            ViewBag.Cskills = Cskills;


            List<LanguageNames> langNames = new List<LanguageNames>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetLanguages"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    langNames = JsonConvert.DeserializeObject<List<LanguageNames>>(apiResponse);
                }
            }
            langNames.Insert(0, new LanguageNames { LanguageID = 0, Language = "--Select a Language--" });
            ViewBag.langNames = langNames;

            List<LanguageLevels> langlevels = new List<LanguageLevels>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetLanguagelevels"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    langlevels = JsonConvert.DeserializeObject<List<LanguageLevels>>(apiResponse);
                }
            }
            langlevels.Insert(0, new LanguageLevels { LevelID = 0, Levels = "--Select a Language level--" });
            ViewBag.langlevels = langlevels;

            List<CompanyCategory> Ccategory = new List<CompanyCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCompanyCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Ccategory = JsonConvert.DeserializeObject<List<CompanyCategory>>(apiResponse);
                }
            }
            Ccategory.Insert(0, new CompanyCategory { CategoryID = "", Category = "--Select a Category--" });
            ViewBag.Categories = Ccategory;

            List<DaysPerWeekOptions> DaysOption = new List<DaysPerWeekOptions>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetDaysPerWeek"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DaysOption = JsonConvert.DeserializeObject<List<DaysPerWeekOptions>>(apiResponse);
                }
            }
            DaysOption.Insert(0, new DaysPerWeekOptions { DaysPerWeekID = "", Options = "--Select a Category--" });
            ViewBag.DaysOption = DaysOption;

            List<HoursPerWeekOptions> HoursOption = new List<HoursPerWeekOptions>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetHoursPerWeek"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    HoursOption = JsonConvert.DeserializeObject<List<HoursPerWeekOptions>>(apiResponse);
                }
            }
            HoursOption.Insert(0, new HoursPerWeekOptions { HoursPerWeekID = "", Options = "--Select a Category--" });
            ViewBag.HoursOption = HoursOption;

            List<Programs> Program = new List<Programs>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetPrograms"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Program = JsonConvert.DeserializeObject<List<Programs>>(apiResponse);
                }
            }
            Program.Insert(0, new Programs { ProgramID = "", ProgramName = "--Select a Program--" });
            ViewBag.Programs = Program;
            
            using (var httpClient = new HttpClient())
            {
                using (var cams_response = await httpClient.GetAsync(_endPoints.Cams_Integration + "/GetByID/" + id))
                {
                    StudentDetails_Cams CudStdDetails = new StudentDetails_Cams();
                    string cams_apiResponse = await cams_response.Content.ReadAsStringAsync();
                    CudStdDetails = JsonConvert.DeserializeObject<StudentDetails_Cams>(cams_apiResponse);

                    if (CudStdDetails != null)
                    {
                        StudentModel stdpersonal = new StudentModel
                        {
                            CudStudentID = CudStdDetails.studentdetails.CudStudentID,
                            FirstName = CudStdDetails.studentdetails.FirstName,
                            LastName = CudStdDetails.studentdetails.LastName,
                            MobileNumber = CudStdDetails.studentdetails.MobileNumber,
                            DateOfBirth = CudStdDetails.studentdetails.DateOfBirth,
                            Gender = CudStdDetails.studentdetails.Gender,
                            EmailID = CudStdDetails.studentdetails.EmailID

                        };
                        // List<StudentEducation> stdedu = new List<StudentEducation>(); stdedu[0].Institution = CudStdDetails.studentEducation.Institution;

                        IList<StudentEducation> stded = new List<StudentEducation>();
                        stded.Add(new StudentEducation
                        {
                            Institution = CudStdDetails.studentEducation.Institution
                            ,
                            Degree = CudStdDetails.studentEducation.Institution
                            ,
                            CompletionPercent = CudStdDetails.studentEducation.CompletionPercent
                            ,
                            StartDate = CudStdDetails.studentEducation.StartDate
                            ,
                            CompletionDate = CudStdDetails.studentEducation.CompletionDate
                        });
                        //IList<StudentDegree> stdDegree = new List<StudentDegree>();
                        //stdDegree.Add(new StudentDegree
                        //{
                        //    Degree = CudStdDetails.studentEducation.Institution
                        //    ,CompletionPercent = CudStdDetails.studentEducation.CompletionPercent
                        //    ,StartDate = CudStdDetails.studentEducation.StartDate
                        //    ,CompletionDate = CudStdDetails.studentEducation.CompletionDate
                        //});
                        Address stdAddress = new Address
                        {
                            City = CudStdDetails.address.City
                            ,
                            State = CudStdDetails.address.State
                            ,
                            CountryID = CudStdDetails.address.CountryID
                            ,
                            PinCode = CudStdDetails.address.PinCode
                        };

                        studentprofile.StudentPersonal = stdpersonal;
                        studentprofile.StudentEducation = stded;
                        //studentprofile.StudentDegree = stdDegree;
                        studentprofile.StudentAddress = stdAddress;
                        //studentprofile                       
                    }
                }
            }
            return View(studentprofile);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        public async Task<IActionResult> Create(StudentProfile collection, List<IFormFile> file)
        {
            if (ModelState.IsValid)
            {
                var isExists = await _CustFunction.isStudentCreated(collection.StudentPersonal.EmailID);

                if (!isExists)
                {
                    string Resumepath = string.Empty;
                    //collection.StudentDegree[0].CreateddDate = DateTime.Now;
                    collection.StudentEducation[0].CreateddDate = DateTime.Now;
                    collection.StudentExperience[0].CreatedDate = DateTime.Now;
                    collection.StudentPersonal.CreatedDate = DateTime.Now;
                    //var Certificatepath = _fileoperations.SaveFile(file[0], "StudentCertificate");

                    if (file.Count != 0)
                        Resumepath = _fileoperations.SaveFile(file[0], "StudentResume");

                    collection.StudentPersonal.Resumepath = Resumepath;
                    //collection.StudentDegree[0].CertificateDiploma = Certificatepath;
                    try
                    {
                        StudentProfile NewStudent = new StudentProfile();
                        Studentdt Student = new Studentdt();
                        //string studentid = "1012";
                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentEndpoints, collection))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                var Studentid = JsonConvert.DeserializeObject<string>(apiResponse);

                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    HttpContext.Session.SetInt32("ProfileID", Convert.ToInt32(Studentid));
                                    //return RedirectToAction("Profile", "Student", new { id = Studentid });
                                    ViewBag.isexists = false;
                                    return RedirectToAction("RegistrationSuccess", "Student");
                                }
                            }
                        }

                        return View(collection);
                    }
                    catch (Exception Ex)
                    {
                        var er = $"{Ex.Message} - {Ex.InnerException}";
                        return View();
                    }
                }
                else
                {
                    ViewBag.isexists = true;
                    return View(collection);
                }                
            }
            return View();
        }

        public async Task<IActionResult> StudentCreate()
        {
            StudentProfile studentprofile = new StudentProfile();

            List<CountryCode> cl = new List<CountryCode>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCountry"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cl = JsonConvert.DeserializeObject<List<CountryCode>>(apiResponse);
                }
            }
            cl.Insert(0, new CountryCode { CountryID = "", Description = "--Select Country Name--" });
            ViewBag.message = cl;

            List<Programs> Program = new List<Programs>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetPrograms"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Program = JsonConvert.DeserializeObject<List<Programs>>(apiResponse);
                }
            }
            Program.Insert(0, new Programs { ProgramID = "", ProgramName = "--Select a Program--" });
            ViewBag.Programs = Program;

            List<DaysPerWeekOptions> DaysOption = new List<DaysPerWeekOptions>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetDaysPerWeek"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DaysOption = JsonConvert.DeserializeObject<List<DaysPerWeekOptions>>(apiResponse);
                }
            }
            DaysOption.Insert(0, new DaysPerWeekOptions { DaysPerWeekID = "", Options = "--Select a Category--" });
            ViewBag.DaysOption = DaysOption;

            List<HoursPerWeekOptions> HoursOption = new List<HoursPerWeekOptions>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetHoursPerWeek"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    HoursOption = JsonConvert.DeserializeObject<List<HoursPerWeekOptions>>(apiResponse);
                }
            }
            HoursOption.Insert(0, new HoursPerWeekOptions { HoursPerWeekID = "", Options = "--Select a Category--" });
            ViewBag.HoursOption = HoursOption;

            //using (var httpClient = new HttpClient())
            //{
            //    using (var cams_response = await httpClient.GetAsync(_endPoints.Cams_Integration + "/GetByID/" + id))
            //    {
            //        StudentDetails_Cams CudStdDetails = new StudentDetails_Cams();
            //        string cams_apiResponse = await cams_response.Content.ReadAsStringAsync();
            //        CudStdDetails = JsonConvert.DeserializeObject<StudentDetails_Cams>(cams_apiResponse);

            //        if (CudStdDetails != null)
            //        {
            //            StudentModel stdpersonal = new StudentModel
            //            {
            //                CudStudentID = CudStdDetails.studentdetails.CudStudentID,
            //                FirstName = CudStdDetails.studentdetails.FirstName,
            //                LastName = CudStdDetails.studentdetails.LastName,
            //                MobileNumber = CudStdDetails.studentdetails.MobileNumber,
            //                DateOfBirth = CudStdDetails.studentdetails.DateOfBirth,
            //                Gender = CudStdDetails.studentdetails.Gender,
            //                EmailID = CudStdDetails.studentdetails.EmailID

            //            };
            //            // List<StudentEducation> stdedu = new List<StudentEducation>(); stdedu[0].Institution = CudStdDetails.studentEducation.Institution;

            //            IList<StudentEducation> stded = new List<StudentEducation>();
            //            stded.Add(new StudentEducation
            //            {
            //                Institution = CudStdDetails.studentEducation.Institution
            //                ,
            //                Degree = CudStdDetails.studentEducation.Institution
            //                ,
            //                CompletionPercent = CudStdDetails.studentEducation.CompletionPercent
            //                ,
            //                StartDate = CudStdDetails.studentEducation.StartDate
            //                ,
            //                CompletionDate = CudStdDetails.studentEducation.CompletionDate
            //            });
            //            //IList<StudentDegree> stdDegree = new List<StudentDegree>();
            //            //stdDegree.Add(new StudentDegree
            //            //{
            //            //    Degree = CudStdDetails.studentEducation.Institution
            //            //    ,CompletionPercent = CudStdDetails.studentEducation.CompletionPercent
            //            //    ,StartDate = CudStdDetails.studentEducation.StartDate
            //            //    ,CompletionDate = CudStdDetails.studentEducation.CompletionDate
            //            //});
            //            Address stdAddress = new Address
            //            {
            //                City = CudStdDetails.address.City
            //                ,
            //                State = CudStdDetails.address.State
            //                ,
            //                CountryID = CudStdDetails.address.CountryID
            //                ,
            //                PinCode = CudStdDetails.address.PinCode
            //            };

            //            studentprofile.StudentPersonal = stdpersonal;
            //            studentprofile.StudentEducation = stded;
            //            //studentprofile.StudentDegree = stdDegree;
            //            studentprofile.StudentAddress = stdAddress;
            //            //studentprofile                       
            //        }
            //    }
            //}

            return View(studentprofile);
        }

        public async Task<IActionResult> EduandExperience(int id, string Typeof)
        {
            try
            {
                IList<StudentEducation> stud = new List<StudentEducation>();
                StudentProfile Student = new StudentProfile();
                if (Typeof == "Edit")
                {
                    //string studentid = "1012";
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    stud = Student.StudentEducation.ToList();
                }
                else
                {
                    stud.Add(new StudentEducation { StudentID = id });
                }
                return View(stud);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        public async Task<IActionResult> EduandExperience(int id, IList<StudentEducation> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].EducationId == 0)
                {
                    Type = "Add";
                }
            }
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateEducation/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                return Json(new { isValid = true, message = "success" });
                                //return RedirectToAction("Profile", "Student", new { id = id });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddEducation/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                return Json(new { isValid = true, message = "success" });
                                //return RedirectToAction("Profile", "Student", new { id = id });
                            }
                        }
                    }
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> StudentPersonal(int id)
        {
            try
            {
                List<CountryCode> cl = new List<CountryCode>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCountry"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cl = JsonConvert.DeserializeObject<List<CountryCode>>(apiResponse);
                    }
                }
                cl.Insert(0, new CountryCode { CountryID = "", Description = "--Select Country Name--" });
                ViewBag.message = cl;

                List<Programs> Program = new List<Programs>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetPrograms"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Program = JsonConvert.DeserializeObject<List<Programs>>(apiResponse);
                    }
                }
                Program.Insert(0, new Programs { ProgramID = "", ProgramName = "--Select a Program--" });
                ViewBag.Programs = Program;

                List<Hardskills> Hskills = new List<Hardskills>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetHardSkills"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Hskills = JsonConvert.DeserializeObject<List<Hardskills>>(apiResponse);
                    }
                }
                Hskills.Insert(0, new Hardskills { HskillID = 0, HardSkills = "--Select a Hard Skill--" });
                ViewBag.Hskills = Hskills;

                List<Softskills> Sskills = new List<Softskills>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetSoftSkills"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Sskills = JsonConvert.DeserializeObject<List<Softskills>>(apiResponse);
                    }
                }
                Sskills.Insert(0, new Softskills { SskillID = 0, SoftSkills = "--Select a Soft Skill--" });
                ViewBag.Sskills = Sskills;

                StudentPersonalview NewStudent = new StudentPersonalview();
                StudentProfile Student = new StudentProfile();
                //string studentid = "1012";               
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                    }
                }
                NewStudent.StudentPersonal = Student.StudentPersonal;
                NewStudent.StudentAddress = Student.StudentAddress;

                return View(NewStudent);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentPersonal(int id, StudentPersonalview collection)
        {
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    //content = collection;
                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdatePersonal/" + id, collection))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            // return RedirectToAction("Profile", "Student", new { id = id });
                            return Json(new { isValid = true, message = "success" });
                        }
                    }
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                var list = new List<string>();
                foreach (var modelStateVal in ViewData.ModelState.Values)
                {
                    list.AddRange(modelStateVal.Errors.Select(error => error.ErrorMessage));
                }
                return Json(new { isValid = false, status = "error", errors = list });
            }
        }

        public async Task<IActionResult> EditStudentExperience(int id, string Typeof)
        {
            try
            {
                List<CompanyCategory> Ccategory = new List<CompanyCategory>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCompanyCategory"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Ccategory = JsonConvert.DeserializeObject<List<CompanyCategory>>(apiResponse);
                    }
                }
                Ccategory.Insert(0, new CompanyCategory { CategoryID = "", Category = "--Select a Category--" });
                ViewBag.Categories = Ccategory;

                StudentProfile Student = new StudentProfile();
                List<StudentExperienceModel> studExperience = new List<StudentExperienceModel>();
                if (Typeof == "Edit")
                {
                    //string studentid = "1012";
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    studExperience = Student.StudentExperience.ToList();
                }
                else
                {
                    studExperience.Add(new StudentExperienceModel { StudentID = id });
                }

                return View(studExperience);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudentExperience(int id, IList<StudentExperienceModel> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].Id == 0)
                {
                    Type = "Add";
                }
            }
            collection[0].CreatedDate = DateTime.Now;
            collection[0].updatedDate = DateTime.Now;
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateExperience/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddExperience/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> EditVolunteerExperience(int id, string Typeof)
        {
            try
            {
                StudentProfile Student = new StudentProfile();
                List<VolunteerExperience> studVExperience = new List<VolunteerExperience>();
                if (Typeof == "Edit")
                {
                    //string studentid = "1012";
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    studVExperience = Student.VolunteerExperience.ToList();
                }
                else
                {
                    studVExperience.Add(new VolunteerExperience { StudentID = id });
                }

                return View(studVExperience);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVolunteerExperience(int id, IList<VolunteerExperience> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].VexpId == 0)
                {
                    Type = "Add";
                    collection[0].Createdate = DateTime.Now;
                }
            }            
            collection[0].Updatedate = DateTime.Now;
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateVolunteerExperience/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {                               
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddVolunteerExperience/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {                               
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> EditPortfolio(int id, string Typeof)
        {
            try
            {
                StudentProfile Student = new StudentProfile();
                List<StudentPortfolio> studportfolio = new List<StudentPortfolio>();
                if (Typeof == "Edit")
                {
                    //string studentid = "1012";
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    studportfolio = Student.Portfolio.ToList();
                }
                else
                {
                    studportfolio.Add(new StudentPortfolio { StudentID = id });
                }

                return View(studportfolio);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPortfolio(int id, IList<StudentPortfolio> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].PortfolioID == 0)
                {
                    Type = "Add";
                    collection[0].Createddate = DateTime.Now;
                }
            }
            collection[0].Updatedate = DateTime.Now;
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdatePortfolio/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddPortfolio/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> EditProject(int id, string Typeof)
        {
            try
            {
                StudentProfile Student = new StudentProfile();
                IList<Projects> studProjects = new List<Projects>();
                if (Typeof == "Edit")
                {
                    //string studentid = "1012";
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    studProjects = Student.projects.ToList();
                }
                else
                {
                    studProjects.Add(new Projects { StudentID = id });
                }

                return View(studProjects);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int id, IList<Projects> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].PorjectID == 0)
                {
                    Type = "Add";
                }
            }
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateProjects/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddProjects/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> EditMembership(int id, string Typeof)
        {
            try
            {
                StudentProfile Student = new StudentProfile();
                IList<Memberships> studMemberships = new List<Memberships>();
                //string studentid = "1012";
                if (Typeof == "Edit")
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    studMemberships = Student.Memberships.ToList();
                }
                else
                {
                    studMemberships.Add(new Memberships { StudentID = id });
                }

                return View(studMemberships);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMembership(int id, IList<Memberships> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].MembershipID == 0)
                {
                    Type = "Add";
                }
            }
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateMemberships/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddMemberships/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> EditAwards(int id, string Typeof)
        {
            try
            {
                StudentProfile Student = new StudentProfile();
                IList<Awards> studAwards = new List<Awards>();
                //string studentid = "1012";
                if (Typeof == "Edit")
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    studAwards = Student.Awards.ToList();
                }
                else
                {
                    studAwards.Add(new Awards { StudentID = id });
                }

                return View(studAwards);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAwards(int id, IList<Awards> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].AwardID == 0)
                {
                    Type = "Add";
                }
            }
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateAwards/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddAwards/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                // return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                }
                // return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> EditLanguages(int id, string Typeof)
        {
            try
            {
                StudentProfile Student = new StudentProfile();
                IList<Languages> studLanguages = new List<Languages>();
                if (Typeof == "Edit")
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    studLanguages = Student.languages.ToList();
                    if(studLanguages.Count == 0)
                    {
                        studLanguages.Add(new Languages { StudentID = id });
                    }
                }
                else
                {
                    studLanguages.Add(new Languages { StudentID = id });
                }

                List<LanguageNames> langNames = new List<LanguageNames>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetLanguages"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        langNames = JsonConvert.DeserializeObject<List<LanguageNames>>(apiResponse);
                    }
                }
                langNames.Insert(0, new LanguageNames { LanguageID = 0, Language = "--Select a Language--" });
                ViewBag.langNames = langNames.OrderBy(a=>a.Language);

                List<LanguageLevels> langlevels = new List<LanguageLevels>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetLanguagelevels"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        langlevels = JsonConvert.DeserializeObject<List<LanguageLevels>>(apiResponse);
                    }
                }
                langlevels.Insert(0, new LanguageLevels { LevelID = 0, Levels = "--Select a Language level--" });
                ViewBag.langlevels = langlevels.OrderBy(a => a.Levels);

                return View(studLanguages);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLanguages(int id, IList<Languages> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].ID == 0)
                {
                    Type = "Add";
                }
            }
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateLanguages/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddLanguages/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> EditWorkAvailability(int id, string Typeof)
        {
            try
            {
                List<DaysPerWeekOptions> DaysOption = new List<DaysPerWeekOptions>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetDaysPerWeek"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        DaysOption = JsonConvert.DeserializeObject<List<DaysPerWeekOptions>>(apiResponse);
                    }
                }
                DaysOption.Insert(0, new DaysPerWeekOptions { DaysPerWeekID = "", Options = "--Select a Category--" });
                ViewBag.DaysOption = DaysOption;

                List<HoursPerWeekOptions> HoursOption = new List<HoursPerWeekOptions>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetHoursPerWeek"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        HoursOption = JsonConvert.DeserializeObject<List<HoursPerWeekOptions>>(apiResponse);
                    }
                }
                HoursOption.Insert(0, new HoursPerWeekOptions { HoursPerWeekID = "", Options = "--Select a Category--" });
                ViewBag.HoursOption = HoursOption;

                StudentProfile Student = new StudentProfile();
                IList<StudentWorkAvailabilityVM> StudentWorkAvailability = new List<StudentWorkAvailabilityVM>();
                if (Typeof == "Edit")
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }
                    StudentWorkAvailability = Student.StudentWorkAvailability.ToList();
                    if(StudentWorkAvailability.FirstOrDefault() == null)
                    {
                        StudentWorkAvailability.Add(new StudentWorkAvailabilityVM { StudentID = id });
                    }
                }
                else
                {
                    StudentWorkAvailability.Add(new StudentWorkAvailabilityVM { StudentID = id });
                }
                return View(StudentWorkAvailability);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWorkAvailability(int id, IList<StudentWorkAvailabilityVM> collection)
        {
            string Type = "Edit";
            if (collection.Count == 1)
            {
                if (collection[0].AvailabilityID == 0)
                {
                    Type = "Add";
                }
            }
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    if (Type == "Edit")
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateStudentWorkAvailabilty/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                    else if (Type == "Add")
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddStudentWorkAvailabilty/" + id, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                //return RedirectToAction("Profile", "Student", new { id = id });
                                return Json(new { isValid = true, message = "success" });
                            }
                        }
                    }
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> EditCompSkill(int id)
        {
            try
            {
                StudentProfile Student = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                    }
                }

                
                StudentComputerSkills stcmpskills = new StudentComputerSkills();

                if (Student.StudentComputerSkills.Any()) {
                    stcmpskills.StudentCsID = Student.StudentComputerSkills.FirstOrDefault().StudentCsID;
                    stcmpskills.ComputerSkill = Student.StudentComputerSkills.FirstOrDefault().ComputerSkill;
                    stcmpskills.StudentID = id;
                }
                else
                {
                    stcmpskills.StudentID = id;
                }

                return View(stcmpskills);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompSkill(int id, StudentComputerSkills collection)
        {
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                   
                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/UpdateStudentComputerSkills/" + id, collection))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();                        
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            ViewBag.Result = "Success";
                            //return RedirectToAction("Profile", "Student", new { id = id });
                            return Json(new { isValid = true, message = "success" });
                        }
                    }
                                      
                }
                //return View(collection);
                return Json(new { isValid = true, message = "success" });
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> AddSkills(int id, string Typeof)
        {
            try
            {
                ViewBag.IncomeType = Typeof;
                if (Typeof == "HardSkill")
                {
                    List<Hardskills> Hskills = new List<Hardskills>();
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetHardSkills"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Hskills = JsonConvert.DeserializeObject<List<Hardskills>>(apiResponse);
                        }
                    }
                    //Hskills.Insert(0, new Hardskills { HskillID = 0, HardSkills = "--Select a Hard Skill--" });
                    ViewBag.Hskills = Hskills;
                    //studentprofile.Hardskills = Hskills;
                }
                else if (Typeof == "SoftSkill")
                {
                    List<Softskills> Sskills = new List<Softskills>();
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetSoftSkills"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Sskills = JsonConvert.DeserializeObject<List<Softskills>>(apiResponse);
                        }
                    }
                    //Sskills.Insert(0, new Softskills { SskillID = 0, SoftSkills = "--Select a Soft Skill--" });
                    ViewBag.Sskills = Sskills;
                    //studentprofile.Softskills = Sskills;
                }
                else if (Typeof == "ComputerSkill")
                {
                    //List<Computerskills> Cskills = new List<Computerskills>();
                    //using (var httpClient = new HttpClient())
                    //{
                    //    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetComputerSkills"))
                    //    {
                    //        string apiResponse = await response.Content.ReadAsStringAsync();
                    //        Cskills = JsonConvert.DeserializeObject<List<Computerskills>>(apiResponse);
                    //    }
                    //}
                    //ViewBag.Cskills = Cskills;
                    StudentProfile Student = new StudentProfile();
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Student = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                        }
                    }

                }
                SkillSetVM Sillset = new SkillSetVM { 
                    StudentID = id
                };
               
                return View(Sillset);
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSkills(int id, SkillSetVM Skills)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(_endPoints.StudentPersonalEndpoints + "/AddStudentSkillSet/" + id, Skills))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {                           
                            return Json(new { isValid = true, message = "success" });
                        }
                    }
                }

                return Json(new { isValid = true, message = "success" });
            }
            catch (Exception Ex)
            {
                var er = $"{Ex.Message} - {Ex.InnerException}";
                return Json(new { isValid = false, message = "Failure!" });
            }
        }

        // GET: StudentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            StudentProfile StudentDetails = new StudentProfile();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    StudentDetails = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                }
            }
            return View(StudentDetails);
        }

        public async Task<IActionResult> AppliedJobsByStudent(int id)
        {
            List<JobApplyDetailsBystudent> joblist = new List<JobApplyDetailsBystudent>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/" + "JobbyStudent/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    joblist = JsonConvert.DeserializeObject<List<JobApplyDetailsBystudent>>(apiResponse);
                }
            }
            return View(joblist);
        }



        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentProfile collection)
        {
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    //var content = new MultipartFormDataContent();
                    //content = collection;
                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentEndpoints + "/UpdateStudent/" + id, collection))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        StudentDetails = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                    }
                }
                return View(StudentDetails);
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                StudentProfile StudentDetails = new StudentProfile();
                using (var httpClient = new HttpClient())
                {
                    //var content = new MultipartFormDataContent();
                    //content = collection;
                    using (var response = await httpClient.DeleteAsync(_endPoints.StudentEndpoints + "/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        //StudentDetails = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id, IFormCollection collection)
        public async Task<IActionResult> DeleteStd()
        {
            try
            {
                var id = HttpContext.Request.Form["id"];
                using (var httpClient = new HttpClient())
                {                    
                    using (var response = await httpClient.DeleteAsync(_endPoints.StudentEndpoints + "/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            return Json(new { isValid = true, message = "success" });
                        }
                    }
                }
                return Json(new { isValid = true, message = "success" });
            }
            catch(Exception ex)
            {
                return Json(new { isValid = true, status = "error", errors = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFilesAjax(IList<IFormFile> files)
        {
            var File = HttpContext.Request.Form.Files[0];
            var studentid = HttpContext.Request.Form["Stdid"];
            var Type = HttpContext.Request.Form["UploadType"];
            string rootpath = string.Empty;
            if (ModelState.IsValid)
            {
                if (File != null && File.Length > 0)
                {
                    try
                    {
                        if(Type == "Profilepic")
                         rootpath = _fileoperations.SaveFile(File, "StudentProfile");
                        else if (Type == "Resume")
                            rootpath = _fileoperations.SaveFile(File, "StudentResume");
                        if (rootpath != null)
                        {
                            StudentModel collection = new StudentModel { StudentID = Convert.ToInt16(studentid),profileImgpath = rootpath };

                            if (Type == "Profilepic")
                            {
                                using (var httpClient = new HttpClient())
                                {
                                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentEndpoints + "/UpdateProfileImg/" + studentid, collection))
                                    {
                                        string apiResponse = await response.Content.ReadAsStringAsync();                                        
                                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                                        {
                                            ViewBag.Result = "Success";
                                        }
                                    }
                                }
                            }
                            else if (Type == "Resume")
                            {
                                using (var httpClient = new HttpClient())
                                {
                                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.StudentEndpoints + "/UpdateStudentResume/" + studentid, collection))
                                    {
                                        string apiResponse = await response.Content.ReadAsStringAsync();
                                        //StudentDetails = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                                        {
                                            ViewBag.Result = "Success";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine($"{Ex.Message}-{Ex.InnerException}");
                        throw;
                    }
                }

                return Json(new { isValid = true, message = "success" });
            }
            else
            {
                var list = new List<string>();
                foreach (var modelStateVal in ViewData.ModelState.Values)
                {
                    list.AddRange(modelStateVal.Errors.Select(error => error.ErrorMessage));
                }
                return Json(new { isValid = false, status = "error", errors = list });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudentModules(int id,string Modules)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(_endPoints.StudentPersonalEndpoints + "/" + id + "/" + Modules))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            ViewBag.Result = "Success";
                        }
                    }
                }
                return Json(new { isValid = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { isValid = false, status = "error",error = ex.InnerException});
            }
        }

        //public ActionResult GetView()
        public ActionResult GetView(string id,string viewName)        
        {
            string _partialv = string.Empty;
            if(viewName == "MyProfilePersonal")
            {
                _partialv = "~/Views/Shared/Partials/_StudentPersonalPartial.cshtml";
            }
            else if (viewName == "MyprofileEducation")
            {
                _partialv = "~/Views/Shared/Partials/_StudentEducationPartial.cshtml";
            }
            else if (viewName == "MyprofileExperience")
            {
                _partialv = "~/Views/Shared/Partials/_StudentExperiencePartial.cshtml";
            }
            else if (viewName == "MyProfileMembership")
            {
                _partialv = "~/Views/Shared/Partials/_StudentMembershipPartial.cshtml";
            }
            else if (viewName == "MyprofileProjects")
            {
                _partialv = "~/Views/Shared/Partials/_StudentProjectsPartial.cshtml";
            }
            else if (viewName == "MyprofileAwards")
            {
                _partialv = "~/Views/Shared/Partials/_StudentAwardsPartial.cshtml";
            }
            else if (viewName == "Myprofilelanguages")
            {
                _partialv = "~/Views/Shared/Partials/_StudentLanguagesPartial.cshtml";
            }
            else if (viewName == "Myprofileinfo")
            {
                _partialv = "~/Views/Shared/Partials/_StudentProfilePartial.cshtml";
            }

            StudentProfile studentprofile = new StudentProfile();
            using (var httpClient = new HttpClient())
            {

                var response = httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var student =  responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    studentprofile = JsonConvert.DeserializeObject<StudentProfile>(student);
                }
            }
            if (studentprofile.StudentPersonal.UpdatedDate != null)
            {
                HttpContext.Session.SetString("Lastupdated", _CustFunction.Lastupdated(DateTime.Now, Convert.ToDateTime(studentprofile.StudentPersonal.UpdatedDate)));
                ViewBag.Lastupdated = _CustFunction.Lastupdated(DateTime.Now, Convert.ToDateTime(studentprofile.StudentPersonal.UpdatedDate));                
            }
            if (studentprofile.StudentPersonal.Resumepath != null)
            {
                HttpContext.Session.SetString("ResumeName", Path.GetFileName(studentprofile.StudentPersonal.Resumepath));
                ViewBag.ResumeName = Path.GetFileName(studentprofile.StudentPersonal.Resumepath);
            }
            if (studentprofile.StudentPersonal.profileImgpath != null && studentprofile.StudentPersonal.profileImgpath != string.Empty)
            {
                HttpContext.Session.SetString("ProfileImg", Path.GetFileName(studentprofile.StudentPersonal.profileImgpath));
                ViewBag.ProfileImg = Path.GetFileName(studentprofile.StudentPersonal.profileImgpath);
            }
            HttpContext.Session.SetString("usrFirstName", studentprofile.StudentPersonal.FirstName);
            HttpContext.Session.SetString("usrLastName", studentprofile.StudentPersonal.LastName);

            return PartialView(_partialv, studentprofile);
        }

        public ActionResult RegistrationSuccess()
        {
            return View();
        }
    }
}
