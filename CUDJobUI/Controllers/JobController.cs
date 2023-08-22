using CudJobUI.Contracts;
using CudJobUI.Services;
using CudJobUI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using X.PagedList;

namespace CudJobUI.Controllers
{
    //[SessionTimeout]
    public class JobController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStaticEndPoints _endPoints;
        private readonly IFileoperations _fileoperations;
        private readonly ILogger _logger;

        public JobController(IConfiguration configuration, IStaticEndPoints endPoints, IFileoperations fileoperations)
        {
            _configuration = configuration;
            _endPoints = endPoints;
            _fileoperations = fileoperations;
        }
        //[DefaultBreadcrumb("Home")]
        // GET: JobController
        public async Task<IActionResult> Index(int? page)
        {
            List<JobViewModel> joblist = new List<JobViewModel>();
            try
            {
                List<JobExperiences> Exp = new List<JobExperiences>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobExperience"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Exp = JsonConvert.DeserializeObject<List<JobExperiences>>(apiResponse);
                    }
                }
                Exp.Insert(0, new JobExperiences { ExperienceID = "", ExperienceType = "All" });
                ViewBag.Experiences = Exp;
                
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/GetAllJobsforStudent"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        joblist = JsonConvert.DeserializeObject<List<JobViewModel>>(apiResponse);
                    }
                }

                if (page.HasValue && page < 1)
                    return null;

                // page the list
                const int pageSize = 8;
                var listPaged = joblist.ToPagedList(page ?? 1, pageSize);

                ViewBag.PageList = listPaged;

                // return a 404 if user browses to pages beyond last page. special case first page if no items exist
                if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                    return null;
            }
            catch (Exception ex)
            {
               // _logger.LogInformation(ex.Message);
                throw;
            }

            return View(joblist);
        }

        public async Task<IActionResult> SearchJob(IFormCollection fm, string Searchstring, string Experience, int? page)
        {
            List<JobExperiences> Exp = new List<JobExperiences>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobExperience"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Exp = JsonConvert.DeserializeObject<List<JobExperiences>>(apiResponse);
                }
            }
            Exp.Insert(0, new JobExperiences { ExperienceID = "", ExperienceType = "All" });
            ViewBag.Experiences = Exp;
            string Exp1 = fm["Experience"];
            Exp1 = Experience;
            List<JobViewModel> joblist = new List<JobViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/JobSearch/" + Searchstring + "/" + Exp1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    joblist = JsonConvert.DeserializeObject<List<JobViewModel>>(apiResponse);
                }
            }

            if (page.HasValue && page < 1)
                return null;

            // page the list
            const int pageSize = 8;
            var listPaged = joblist.ToPagedList(page ?? 1, pageSize);

            ViewBag.PageList = listPaged;

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return View("index", joblist);
        }      

        public async Task<IActionResult> GetLatestJobs()
        {
            List<JobViewModel> joblist = new List<JobViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints+ "/GetLatestJobs"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    joblist = JsonConvert.DeserializeObject<List<JobViewModel>>(apiResponse);
                }
            }

            StringBuilder HtmlBody = new StringBuilder();
            foreach (var job in joblist)
            {
                var shordHeadings = job.JobDetail.Shortheading;
                shordHeadings = Regex.Replace(shordHeadings, "<.*?>", String.Empty);
                if (shordHeadings.Length > 150)
                    shordHeadings = shordHeadings.Substring(0, 150);

                HtmlBody.Append($"<tr style='line-height: 35px;'><td style='padding-bottom:20px;'><a href='https://studentcareers.cud.ac.ae/' style='text-decoration:none;'><table width='100%' cellpadding='0' cellspacing='0'><tbody>" +
               $"<tr> <td style='padding: 0 0 4px'><a href='https://studentcareers.cud.ac.ae/' style='display:block;text-decoration:underline;font-size:16px;line-height:20px; font-weight:bold;color:#2d2d2d' target='_blank'> {job.JobDetail.Title} </a></td></tr>" +
               $"<tr><td style='font: 14px/20px; font-weight:400; color:#2d2d2d;padding:0 0 4px'><span style='font-size:14px;line-height:20px;font-weight:700;color:#2d2d2d'>{job.Company.CompanyName}</span><span style='color:#767676;font-size:14px'>- {job.CompanyAddress.City}</span></td></tr>" +
               $"<tr><td style='font-size:14px;line-height:20px;color:#595959;padding:0 0 4px;'><p style='margin:0 2px 3px 0;'>{shordHeadings}</p><p><span class='text-muted'><small><em>read more...</em></small></span></p></td></tr>" +
               $"<tr><td style='color:#767676;font-size:14px;line-height:20px;padding:0 4px 0 0'>1 day ago</td></tr>" +
               $"</tbody></table></a><hr></td></tr>");
            }
            ViewBag.Joblists = HtmlBody;
            return View(joblist);
        }

        public async Task<IActionResult> JobList()
        {
            List<JobViewModel> joblist = new List<JobViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    joblist = JsonConvert.DeserializeObject<List<JobViewModel>>(apiResponse);
                }
            }
            return View(joblist);
        }

        public async Task<IActionResult> JobsforApproval()
        {

            List<Statuses> cl = new List<Statuses>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetStatuses"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cl = JsonConvert.DeserializeObject<List<Statuses>>(apiResponse);
                }
            }
            cl.Insert(0, new Statuses { StatusIDs = 0, MyProperty = "--Select a Status--" });
            ViewBag.Statuses = cl;

            List<JobViewModel> joblist = new List<JobViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/GetJobsforApproval"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    joblist = JsonConvert.DeserializeObject<List<JobViewModel>>(apiResponse);
                }
            }
            return View(joblist);
        }

        public async Task<IActionResult> Approve(int id)
        {
            JobDetails JobDetails = new JobDetails { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync(_endPoints.JobEndpoints + "/" + "Approve/" + id, JobDetails))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //NewCompany = JsonConvert.DeserializeObject<CompanyViewModel>(apiResponse);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("JobsforApproval", "Job");
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> MassApprove(string id)
        {
            try
            {
                JobDetails JobDetails = new JobDetails { };
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.JobEndpoints + "/" + "MassApprove/" + id, JobDetails))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //NewCompany = JsonConvert.DeserializeObject<CompanyViewModel>(apiResponse);
                    }
                }
                return Json(new { isValid = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { isValid = false, status = "error", error = ex.InnerException });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateJobStatus()
        {
            var Status = HttpContext.Request.Form["status"];
            var Desc = HttpContext.Request.Form["Desc"];
            var JobID = HttpContext.Request.Form["JobID"];
            var NotesID = HttpContext.Request.Form["NotesID"];
            if (ModelState.IsValid)
            {
                try
                {
                    StatusesNotes Notes = new StatusesNotes { NotesID = Convert.ToInt32(NotesID), Notes = Desc, StatusID = Convert.ToInt32(Status) };

                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.JobEndpoints + "/UpdateStatus/" + JobID, Notes))
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
                catch (Exception Ex)
                {
                    Console.WriteLine($"{Ex.Message}-{Ex.InnerException}");
                    throw;
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
                return Json(new { isValid = true, status = "error", errors = list });
            }
        }

        public async Task<IActionResult> JobsByCompany(int id)
        {
            List<JobViewModel> joblist = new List<JobViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/" + "JobforCompany/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    joblist = JsonConvert.DeserializeObject<List<JobViewModel>>(apiResponse);
                }
            }

            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/" + "JobbyCompany/" + id))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        joblist = JsonConvert.DeserializeObject<List<JobViewModel>>(apiResponse);
            //    }
            //}



            return View(joblist);
        }

        public async Task<IActionResult> JobsforStudentsByCompany(int id, int? page)
        {
            List<JobViewModel> joblist = new List<JobViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/" + "JobbyCompany/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    joblist = JsonConvert.DeserializeObject<List<JobViewModel>>(apiResponse);
                }
            }

            if (page.HasValue && page < 1)
                return null;

            // page the list
            const int pageSize = 5;
            var listPaged = joblist.ToPagedList(page ?? 1, pageSize);

            ViewBag.PageList = listPaged;

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;
            return View(joblist);
        }
        //[DefaultBreadcrumb("Home")]
        public async Task<IActionResult> JobsByStudent(int id)
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

        //[Breadcrumb("Job List", FromAction = "Index")]
        // GET: JobController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            JobViewModel JobDetail = new JobViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/" + "GetJob/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JobDetail = JsonConvert.DeserializeObject<JobViewModel>(apiResponse);
                }
            }
            if (JobDetail.Company.profileImgpath != null && JobDetail.Company.profileImgpath != string.Empty)
            {
                ViewBag.ProfileImg = Path.GetFileName(JobDetail.Company.profileImgpath);
            }
            if (JobDetail.JobDetail.JobdocsPath != null && JobDetail.JobDetail.JobdocsPath != string.Empty)
            {
                ViewBag.jobDocs = Path.GetFileName(JobDetail.JobDetail.JobdocsPath);
            }
            var Studentid = HttpContext.Session.GetInt32("ProfileID");

            if (Studentid != null || Studentid != 0)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.jobApplicationEndpoints + "/" + "Getjobstatus/" + id + '/' + Studentid))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.AppliedStatus = apiResponse;
                    }
                }
            }

            return View(JobDetail);
        }

        public async Task<IActionResult> jobDetailforstudent(int id)
        {
            JobViewModel JobDetail = new JobViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/" + "GetJob/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JobDetail = JsonConvert.DeserializeObject<JobViewModel>(apiResponse);
                }
            }
            if (JobDetail.Company.profileImgpath != null && JobDetail.Company.profileImgpath != string.Empty)
            {
                ViewBag.ProfileImg = Path.GetFileName(JobDetail.Company.profileImgpath);
            }
            if (JobDetail.JobDetail.JobdocsPath != null && JobDetail.JobDetail.JobdocsPath != string.Empty)
            {
                ViewBag.jobDocs = Path.GetFileName(JobDetail.JobDetail.JobdocsPath);
            }
            var Studentid = HttpContext.Session.GetInt32("ProfileID");

            if (Studentid != null || Studentid != 0)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.jobApplicationEndpoints + "/" + "Getjobstatus/" + id + '/' + Studentid))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.AppliedStatus = apiResponse;
                    }
                }
            }
            return PartialView("~/Views/Shared/Partials/_JDPartial.cshtml", JobDetail);
        }

        // GET: JobController/Details/5
        public async Task<IActionResult> Detailsforcompany(int id)
        {
            JobViewModel JobDetail = new JobViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/" + "GetJob/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JobDetail = JsonConvert.DeserializeObject<JobViewModel>(apiResponse);
                }
            }
            if (JobDetail.Company.profileImgpath != null && JobDetail.Company.profileImgpath != string.Empty)
            {
                ViewBag.ProfileImg = Path.GetFileName(JobDetail.Company.profileImgpath);
            }
            if (JobDetail.JobDetail.JobdocsPath != null && JobDetail.JobDetail.JobdocsPath != string.Empty)
            {
                ViewBag.jobDocs = Path.GetFileName(JobDetail.JobDetail.JobdocsPath);
            }
            var Studentid = HttpContext.Session.GetInt32("ProfileID");

            if (Studentid != null || Studentid != 0)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_endPoints.jobApplicationEndpoints + "/" + "Getjobstatus/" + id + '/' + Studentid))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.AppliedStatus = apiResponse;
                    }
                }
            }
            return View(JobDetail);
        }

        public async Task<IActionResult> StudentListByJob(int id)
        {

            List<JobStatuses> cl = new List<JobStatuses>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobStatuses"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cl = JsonConvert.DeserializeObject<List<JobStatuses>>(apiResponse);
                }
            }
            cl.Insert(0, new JobStatuses { StatusID = 0, Status = "--Select a Status--" });
            ViewBag.Statuses = cl;


            List<JobApplyDetails> Studentlist = new List<JobApplyDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.jobApplicationEndpoints + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Studentlist = JsonConvert.DeserializeObject<List<JobApplyDetails>>(apiResponse);
                }
            }
            return View(Studentlist);
            //return View();
        }

        public async Task<IActionResult> StudentListByJobforcompany(int id)
        {

            List<JobStatuses> cl = new List<JobStatuses>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobStatuses"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cl = JsonConvert.DeserializeObject<List<JobStatuses>>(apiResponse);
                }
            }
            cl.Insert(0, new JobStatuses { StatusID = 0, Status = "--Select a Status--" });
            ViewBag.Statuses = cl;


            List<JobApplyDetails> Studentlist = new List<JobApplyDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.jobApplicationEndpoints + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Studentlist = JsonConvert.DeserializeObject<List<JobApplyDetails>>(apiResponse);
                }
            }
            return View(Studentlist);
            //return View();
        }

       
        public async Task<IActionResult> ApplyJob(int id)
        {
            try
            {

                var Studentid = HttpContext.Session.GetInt32("ProfileID");
                //int? Studentid = 1007;
                if (Studentid == null || Studentid == 0)
                {
                    return RedirectToAction("Login", "Account");
                }
                AppliedJobs AppliedJobs = new AppliedJobs { jobID = id, StudentID = Convert.ToInt32(Studentid), StatusID = 8 };
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(_endPoints.jobApplicationEndpoints, AppliedJobs))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        AppliedJobs = JsonConvert.DeserializeObject<AppliedJobs>(apiResponse);
                        // await Task.Delay(100);                        
                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            ViewBag.PostType = "JobApply";
                            return View("Jobsuccess");
                            //return Json(new { isValid = true, message = "success" });
                        }
                    }
                }
                return View(AppliedJobs);
            }
            catch (Exception Ex)
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<JsonResult> Applyjobasync(string id)
        {
            try
            {

                var Studentid = HttpContext.Session.GetInt32("ProfileID");               
                if (Studentid == null || Studentid == 0)
                {
                    return Json(new { isValid = false, message = "You session is Invalid. Please login again." });
                }
                AppliedJobs AppliedJobs = new AppliedJobs { jobID = Convert.ToInt32(id), StudentID = Convert.ToInt32(Studentid), StatusID = 8 };
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(_endPoints.jobApplicationEndpoints, AppliedJobs))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        AppliedJobs = JsonConvert.DeserializeObject<AppliedJobs>(apiResponse);
                        // await Task.Delay(100);                        
                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            ViewBag.PostType = "JobApply";
                            return Json(new { isValid = true, message = "success" });
                        }
                    }
                }
                return Json(new { isValid = true, message = "success" });
            }
            catch (Exception Ex)
            {
                return Json(new { isValid = false, message = Ex.Message });
            }
        }

        // GET: JobController/Create
        public async Task<IActionResult> Create()
        {
            List<JobCategory> cl = new List<JobCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cl = JsonConvert.DeserializeObject<List<JobCategory>>(apiResponse);
                }
            }
            cl.Insert(0, new JobCategory { JobCategoryId = "", JobCategoryName = "--Select a Category--" });
            ViewBag.Categories = cl;

            List<JobExperiences> Exp = new List<JobExperiences>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobExperience"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Exp = JsonConvert.DeserializeObject<List<JobExperiences>>(apiResponse);
                }
            }
            Exp.Insert(0, new JobExperiences { ExperienceID = "", ExperienceType = "--Select an Experience--" });
            ViewBag.Experiences = Exp;

            List<DaysPerWeekOptions> DaysOption = new List<DaysPerWeekOptions>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetDaysPerWeek"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DaysOption = JsonConvert.DeserializeObject<List<DaysPerWeekOptions>>(apiResponse);
                }
            }
            DaysOption.Insert(0, new DaysPerWeekOptions { DaysPerWeekID = "", Options = "--Select an option--" });
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
            HoursOption.Insert(0, new HoursPerWeekOptions { HoursPerWeekID = "", Options = "--Select an option--" });
            ViewBag.HoursOption = HoursOption;

            List<Jobtypes> Jobtypes = new List<Jobtypes>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobType"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Jobtypes = JsonConvert.DeserializeObject<List<Jobtypes>>(apiResponse);
                }
            }
            Jobtypes.Insert(0, new Jobtypes { JobTypeID = "", Description = "--Select a Type--" });
            ViewBag.Jobtypes = Jobtypes;

            List<Job_durations> Job_durations = new List<Job_durations>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobDurations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Job_durations = JsonConvert.DeserializeObject<List<Job_durations>>(apiResponse);
                }
            }
            Job_durations.Insert(0, new Job_durations { durationID = 0, Description = "--Select a Duration--" });
            ViewBag.Job_durations = Job_durations;

            JobDetails dt = new JobDetails();
            return View(dt);
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobDetails collection, List<IFormFile> file)
        {
            if (ModelState.IsValid)
            {
                if (collection.Type == "Employer")
                {
                    var CompanyID = HttpContext.Session.GetInt32("ProfileID");

                    if (CompanyID == null || CompanyID == 0)
                    {
                        return RedirectToAction("ErrorPage", "Account");
                    }
                    collection.CompanyID = Convert.ToInt32(CompanyID);
                }
                try
                {
                    JobDetails NewJob = new JobDetails();
                    if (file.Count > 0)
                    {
                        var Certificatepath = _fileoperations.SaveFile(file[0], "JobDocs");
                        collection.JobdocsPath = Certificatepath;
                    }                                  
                    collection.durationID = 5;
                    ViewBag.PostypeBy = collection.Type;
                    using (var httpClient = new HttpClient())
                    {

                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.JobEndpoints, collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            NewJob = JsonConvert.DeserializeObject<JobDetails>(apiResponse);
                            // await Task.Delay(100);                        
                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                ViewBag.PostType = "JobPosting";
                                return View("Jobsuccess");
                            }
                        }
                    }
                    return View(NewJob);
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }
            return View();
        }

        // GET: JobController/Create
        public async Task<IActionResult> JobCreateByAdmin()
        {
            List<JobCategory> cl = new List<JobCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cl = JsonConvert.DeserializeObject<List<JobCategory>>(apiResponse);
                }
            }
            cl.Insert(0, new JobCategory { JobCategoryId = "", JobCategoryName = "--Select a Category--" });
            ViewBag.Categories = cl;

            List<JobExperiences> Exp = new List<JobExperiences>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobExperience"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Exp = JsonConvert.DeserializeObject<List<JobExperiences>>(apiResponse);
                }
            }
            Exp.Insert(0, new JobExperiences { ExperienceID = "", ExperienceType = "--Select an Experience--" });
            ViewBag.Experiences = Exp;

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

            List<Jobtypes> Jobtypes = new List<Jobtypes>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobType"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Jobtypes = JsonConvert.DeserializeObject<List<Jobtypes>>(apiResponse);
                }
            }
            Jobtypes.Insert(0, new Jobtypes { JobTypeID = "", Description = "--Select a Type--" });
            ViewBag.Jobtypes = Jobtypes;

            List<Job_durations> Job_durations = new List<Job_durations>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobDurations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Job_durations = JsonConvert.DeserializeObject<List<Job_durations>>(apiResponse);
                }
            }
            Job_durations.Insert(0, new Job_durations { durationID = 0, Description = "--Select a Duration--" });
            ViewBag.Job_durations = Job_durations;

            List<CompanyDetails> companyList = new List<CompanyDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCompanyList"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    companyList = JsonConvert.DeserializeObject<List<CompanyDetails>>(apiResponse);
                }
            }
            companyList.Insert(0, new CompanyDetails { CompanyID = 0, CompanyName = "--Select a Company--" });
            ViewBag.companyList = companyList;

            JobDetails dt = new JobDetails();
            return View(dt);
        }

        // GET: JobController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            List<JobCategory> cl = new List<JobCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cl = JsonConvert.DeserializeObject<List<JobCategory>>(apiResponse);
                }
            }
            cl.Insert(0, new JobCategory { JobCategoryId = "", JobCategoryName = "--Select a Category--" });
            ViewBag.Categories = cl;

            List<JobExperiences> Exp = new List<JobExperiences>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobExperience"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Exp = JsonConvert.DeserializeObject<List<JobExperiences>>(apiResponse);
                }
            }
            Exp.Insert(0, new JobExperiences { ExperienceID = "", ExperienceType = "--Select an Experience--" });
            ViewBag.Experiences = Exp;

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

            List<Jobtypes> Jobtypes = new List<Jobtypes>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobType"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Jobtypes = JsonConvert.DeserializeObject<List<Jobtypes>>(apiResponse);
                }
            }
            Jobtypes.Insert(0, new Jobtypes { JobTypeID = "", Description = "--Select a Type--" });
            ViewBag.Jobtypes = Jobtypes;

            List<Job_durations> Job_durations = new List<Job_durations>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobDurations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Job_durations = JsonConvert.DeserializeObject<List<Job_durations>>(apiResponse);
                }
            }
            Job_durations.Insert(0, new Job_durations { durationID = 0, Description = "--Select a Duration--" });
            ViewBag.Job_durations = Job_durations;

            List<CompanyDetails> companyList = new List<CompanyDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCompanyList"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    companyList = JsonConvert.DeserializeObject<List<CompanyDetails>>(apiResponse);
                }
            }
            companyList.Insert(0, new CompanyDetails { CompanyID = 0, CompanyName = "--Select a Company--" });
            ViewBag.companyList = companyList;

            JobDetails jb = new JobDetails();
            JobViewModel JobDetail = new JobViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.JobEndpoints + "/" + "GetJob/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JobDetail = JsonConvert.DeserializeObject<JobViewModel>(apiResponse);
                }
            }

            jb = JobDetail.JobDetail;
            jb.JobsWorkAvail = JobDetail.JobsWorkAvail;
            jb.JobOption = JobDetail.JobOption;
            jb.StatusesNotes = JobDetail.jobstatusNotes;

            return View(jb);
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobDetails collection)
        {
            try
            {
                collection.UpdatedDate = DateTime.Now;                
                JobDetails jb = new JobDetails();
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(content);
                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.JobEndpoints + "/UpdateJob/" + id, collection))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        jb = JsonConvert.DeserializeObject<JobDetails>(apiResponse);

                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            return RedirectToAction("Detailsforcompany", new { id = id });
                        }
                    }
                }
                return View(jb);
            }
            catch
            {
                return View();
            }
        }

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Deletejob()
        {
            try
            {
                var id = HttpContext.Request.Form["id"];
                using (var httpClient = new HttpClient())
                {
                    //var content = new MultipartFormDataContent();
                    //content = collection;
                    using (var response = await httpClient.DeleteAsync(_endPoints.JobEndpoints + "/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            return Json(new { isValid = true, message = "success" });
                        }
                    }
                }
                return Json(new { isValid = true, message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { isValid = true, status = "error", errors = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppliedJobStatus()
        {
            var Status = HttpContext.Request.Form["status"];
            var Desc = HttpContext.Request.Form["Desc"];
            var JobID = HttpContext.Request.Form["JobID"];
            var AppliedID = HttpContext.Request.Form["AppliedID"];
            if (ModelState.IsValid)
            {
                try
                {
                    AppliedJobs AppliedJobs = new AppliedJobs { ID = Convert.ToInt32(AppliedID), jobID = Convert.ToInt32(JobID), Description = Desc, StatusID = Convert.ToInt32(Status) };

                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.jobApplicationEndpoints + "/UpdateJobStatus/" + AppliedID, AppliedJobs))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();                            
                            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {
                                ViewBag.Result = "Success";
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine($"{Ex.Message}-{Ex.InnerException}");
                    throw;
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
                return Json(new { isValid = true, status = "error", errors = list });
            }
        }

        public ActionResult GetView(int ID)
        {
            List<JobStatuses> cl = new List<JobStatuses>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetJobStatuses").GetAwaiter().GetResult())
                {
                    string apiResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    cl = JsonConvert.DeserializeObject<List<JobStatuses>>(apiResponse);
                }
            }
            cl.Insert(0, new JobStatuses { StatusID = 0, Status = "--Select a Status--" });
            ViewBag.Statuses = cl;

            List<JobApplyDetails> Studentlist = new List<JobApplyDetails>();
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(_endPoints.jobApplicationEndpoints + "/" + ID).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var student = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    Studentlist = JsonConvert.DeserializeObject<List<JobApplyDetails>>(student);
                }
            }
            return PartialView("~/Views/Shared/Partials/_StudentListByJobPartial.cshtml", Studentlist);
        }
    }
}
