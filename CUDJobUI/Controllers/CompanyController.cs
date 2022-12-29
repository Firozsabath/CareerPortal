using CudJobUI.Contracts;
using CudJobUI.Services;
using CudJobUI.ViewModels;
//using CudJobUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CudJobUI.Controllers
{
    //[SessionTimeout]
    public class CompanyController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStaticEndPoints _endPoints;
        private readonly IFileoperations _fileoperations;
        private readonly ICustomFunctions _customFunctions;

        public CompanyController(IConfiguration configuration, IStaticEndPoints endPoints, IFileoperations fileoperations, ICustomFunctions customFunctions
            //,IMapper Mapper
            )
        {
            _configuration = configuration;
            _endPoints = endPoints;
            //_Mapper = Mapper;
            _fileoperations = fileoperations;
            _customFunctions = customFunctions;
        }
        // GET: CompanyController
        public async Task<IActionResult> Index()
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

            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.CompanyEndpoints))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    companies = JsonConvert.DeserializeObject<List<CompanyViewModel>>(apiResponse);
                }
            }
            return View(companies);          
        }

        public async Task<IActionResult> CompanyListforstudents()
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.CompanyEndpoints))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    companies = JsonConvert.DeserializeObject<List<CompanyViewModel>>(apiResponse);
                }
            }
            return View(companies);
        }

        public async Task<IActionResult> CompanyforApproval()
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

            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.CompanyEndpoints + "/GetCompanyforApproval"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    companies = JsonConvert.DeserializeObject<List<CompanyViewModel>>(apiResponse);
                }
            }
         
            return View(companies);
            //return View();
        }

        public async Task<IActionResult> Approve(int id)
        {
            CompanyViewModel Company = new CompanyViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync(_endPoints.CompanyEndpoints + "/" + "Approve/" + id, Company))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //NewCompany = JsonConvert.DeserializeObject<CompanyViewModel>(apiResponse);
                }
            }
            return View();
        }

        public async Task<IActionResult> MassApprove(string id)
        {
            try
            {
                id = HttpContext.Request.Form["ids"];
                CompanyViewModel Company = new CompanyViewModel();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.CompanyEndpoints + "/" + "MassApprove/" + id, Company))
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
        public async Task<IActionResult> UpdateCompanyStatus()
        {
            var Status = HttpContext.Request.Form["status"];
            var Desc = HttpContext.Request.Form["Desc"];
            var CompanyID = HttpContext.Request.Form["CompanyID"];
            var NotesID = HttpContext.Request.Form["NotesID"];
            if (ModelState.IsValid)
            {
                try
                {
                    StatusesNotes Notes = new StatusesNotes { NotesID = Convert.ToInt32(NotesID), Notes = Desc, StatusID = Convert.ToInt32(Status) };

                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PutAsJsonAsync(_endPoints.CompanyEndpoints + "/UpdateStatus/" + CompanyID, Notes))
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
                return Json(new { isValid = false, status = "error", errors = list });
            }
        }
        // GET: CompanyController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            CompanyViewModel Company = new CompanyViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.CompanyEndpoints + "/GetCompany/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Company = JsonConvert.DeserializeObject<CompanyViewModel>(apiResponse);
                }
            }
            ViewBag.Lastupdated = _customFunctions.Lastupdated(DateTime.Now, Company.Companies.UpdatedDate);
            ViewBag.LicenseExpiresIn = _customFunctions.Lastupdated(Company.Companies.LicenseExpiryDate, DateTime.Now);
            if (Company.Companies.profileImgpath != null && Company.Companies.profileImgpath != string.Empty)
            {
                ViewBag.ProfileImg = Path.GetFileName(Company.Companies.profileImgpath);
            }
            if (Company.Companies.Certificatepath != null && Company.Companies.Certificatepath != string.Empty)
            {
                ViewBag.License = Path.GetFileName(Company.Companies.Certificatepath);
            }
            return View(Company);

        }

        public async Task<IActionResult> CompanyDetails(int id)
        {
            CompanyViewModel Company = new CompanyViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.CompanyEndpoints + "/GetCompany/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Company = JsonConvert.DeserializeObject<CompanyViewModel>(apiResponse);
                }
            }            
            ViewBag.Lastupdated = _customFunctions.Lastupdated(DateTime.Now,Company.Companies.UpdatedDate);
            if (Company.Companies.profileImgpath != null && Company.Companies.profileImgpath != string.Empty)
            {
                ViewBag.ProfileImg = Path.GetFileName(Company.Companies.profileImgpath);
            }
            if (Company.Companies.Certificatepath != null && Company.Companies.Certificatepath != string.Empty)
            {
                ViewBag.License = Path.GetFileName(Company.Companies.Certificatepath);
            }
            return View(Company);

        }

        // GET: CompanyController/Create
        public async Task<IActionResult> Create()
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
            ViewBag.EmailID = HttpContext.Session.GetString("EmailID");

            List<CompanyCategory> CompanyList = new List<CompanyCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var responseCcategory = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCompanyCategory"))
                {
                    string apiResponseCcategory = await responseCcategory.Content.ReadAsStringAsync();
                    CompanyList = JsonConvert.DeserializeObject<List<CompanyCategory>>(apiResponseCcategory);
                }
            }
            CompanyList.Insert(0, new CompanyCategory { CategoryID = "", Category = "--Select Category--" });
            ViewBag.CompanyCategory = CompanyList;

            List<CompanySectors> CompanySectors = new List<CompanySectors>();
            using (var httpClient = new HttpClient())
            {
                using (var responseSector = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCompanySectors"))
                {
                    string apiResponsesector = await responseSector.Content.ReadAsStringAsync();
                    CompanySectors = JsonConvert.DeserializeObject<List<CompanySectors>>(apiResponsesector);
                }
            }
            CompanySectors.Insert(0, new CompanySectors { SectorID = "", Category = "--Select Category--" });
            ViewBag.CompanySectors = CompanySectors;

            List<SelectListItem> CompanyStrength = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Less than 50", Value="Less than 50"},
                new SelectListItem() { Text="51-100", Value="51-100"},
                new SelectListItem() { Text="101-500", Value="101-500"},
                new SelectListItem() { Text="5001-1000", Value="5001-1000"},
                new SelectListItem() { Text="More Than 1000", Value="More Than 1000"},
                new SelectListItem() { Text="NA", Value="NA"}
            };
            ViewBag.CompanyStrength = CompanyStrength;

            List<SelectListItem> Options111 = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Yes", Value="Yes"},
                new SelectListItem() { Text="No", Value="No"},
            };
            ViewBag.Options111 = Options111;

            return View();
        }

        // POST: CompanyController/Create
        [HttpPost]
        //[SessionTimeout]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(CompanyViewModel collection)
        public async Task<IActionResult> Create(CompanyViewModel collection, List<IFormFile> file)
        {
            //collection.CompanyUserName.LoginType = "Company";
            if (ModelState.IsValid)
            {
                //collection.Companies.CategoryID = 1;
                var User = HttpContext.Session.GetString("EmailID");
                var Certificatepath = _fileoperations.SaveFile(file[0], "CompanyResume");
                if (User != null)
                {
                    collection.Companies.UserEmailID = User;
                }
                else
                {

                }
                collection.Companies.Certificatepath = Certificatepath;
                collection.Companies.CreatedDate = DateTime.Now;
                collection.Companies.UpdatedDate = DateTime.Now;
                try
                {
                    CompanyViewModel NewCompany = new CompanyViewModel();
                    Companydt Company = new Companydt();
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(collection), Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync(_endPoints.CompanyEndpoints, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Company = JsonConvert.DeserializeObject<Companydt>(apiResponse);

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                HttpContext.Session.SetInt32("ProfileID", Company.CompanyID);
                                return RedirectToAction("Details", new { id = Company.CompanyID });
                            }
                        }
                    }
                    return View(collection);
                }
                catch
                {
                    return View(collection);
                }
            }
            else
            {
                return View(collection);
            }
        }

        // GET: CompanyController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            CompanyViewModel Company = new CompanyViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.CompanyEndpoints + "/GetCompany/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Company = JsonConvert.DeserializeObject<CompanyViewModel>(apiResponse);
                }
            }

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
            //ViewBag.EmailID = HttpContext.Session.GetString("EmailID");

            List<CompanyCategory> CompanyList = new List<CompanyCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var responseCcategory = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCompanyCategory"))
                {
                    string apiResponseCcategory = await responseCcategory.Content.ReadAsStringAsync();
                    CompanyList = JsonConvert.DeserializeObject<List<CompanyCategory>>(apiResponseCcategory);
                }
            }
            CompanyList.Insert(0, new CompanyCategory { CategoryID = "", Category = "--Select Category--" });
            ViewBag.CompanyCategory = CompanyList;

            List<CompanySectors> CompanySectors = new List<CompanySectors>();
            using (var httpClient = new HttpClient())
            {
                using (var responseSector = await httpClient.GetAsync(_endPoints.DatatoformEndpoints + "/GetCompanySectors"))
                {
                    string apiResponsesector = await responseSector.Content.ReadAsStringAsync();
                    CompanySectors = JsonConvert.DeserializeObject<List<CompanySectors>>(apiResponsesector);
                }
            }
            CompanySectors.Insert(0, new CompanySectors { SectorID = "", Category = "--Select Category--" });
            ViewBag.CompanySectors = CompanySectors;

            List<SelectListItem> CompanyStrength = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Less than 50", Value="Less than 50"},
                new SelectListItem() { Text="51-100", Value="51-100"},
                new SelectListItem() { Text="101-500", Value="101-500"},
                new SelectListItem() { Text="5001-1000", Value="5001-1000"},
                new SelectListItem() { Text="More Than 1000", Value="More Than 1000"},
                new SelectListItem() { Text="NA", Value="NA"}
            };
            ViewBag.CompanyStrength = CompanyStrength;

            return View(Company);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyViewModel collection)
        {
            var CompanyID = HttpContext.Session.GetInt32("ProfileID");
            //if(CompanyID == 0)
            //{
            //    RedirectToAction("ErrorPage", "Account");
            //}
            collection.Companies.UpdatedDate = DateTime.Now;
            CompanyViewModel Company = new CompanyViewModel();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(content);
                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.CompanyEndpoints + "/UpdateCompany/" + id, collection))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        Company = JsonConvert.DeserializeObject<CompanyViewModel>(apiResponse);

                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {                           
                            return RedirectToAction("Details", new { id = CompanyID });
                        }
                    }
                }
                return View(Company);
                //return RedirectToAction("", "Company");
            }
            catch
            {
                return View();
            }
        }

        //// GET: CompanyController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: CompanyController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        StudentProfile StudentDetails = new StudentProfile();
        //        using (var httpClient = new HttpClient())
        //        {
        //            //var content = new MultipartFormDataContent();
        //            //content = collection;
        //            using (var response = await httpClient.DeleteAsync(_endPoints.CompanyEndpoints + "/" + id))
        //            {
        //                string apiResponse = await response.Content.ReadAsStringAsync();
        //                ViewBag.Result = "Success";
        //                //StudentDetails = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]        
        public async Task<IActionResult> DeleteCmp()
        {
            try
            {
                var id = HttpContext.Request.Form["id"];
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(_endPoints.CompanyEndpoints + "/" + id))
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
            catch (Exception ex)
            {
                return Json(new { isValid = true, status = "error", errors = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFilesAjax(IList<IFormFile> files)
         {
            var File = HttpContext.Request.Form.Files[0];
            var CompanyID = HttpContext.Request.Form["CompanyID"];
            var Type = HttpContext.Request.Form["UploadType"];            
            string rootpath = string.Empty;
            if (ModelState.IsValid)
            {
                if (File != null && File.Length > 0)
                {
                    try
                    {
                        if (Type == "Profilepic")
                            rootpath = _fileoperations.SaveFile(File, "CompanyProfile");
                        else if (Type == "Resume")
                            rootpath = _fileoperations.SaveFile(File, "CompanyResume");
                        //var rootpath = _fileoperations.SaveFile(File, "CompanyProfile");
                        if (rootpath != null)
                        {
                            // StudentModel collection = new StudentModel { StudentID = Convert.ToInt16(CompanyID), profileImgpath = rootpath };
                            CompanyDetails collection = new CompanyDetails { CompanyID = Convert.ToInt16(CompanyID), profileImgpath = rootpath };

                            if (Type == "Profilepic")
                            {
                                using (var httpClient = new HttpClient())
                                {
                                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.CompanyEndpoints + "/UpdateProfileImg/" + CompanyID, collection))
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
                            else if (Type == "Resume")
                            {
                                var expiryDate = Convert.ToDateTime(HttpContext.Request.Form["ExiryDate"]);
                                collection.LicenseExpiryDate = expiryDate;
                                using (var httpClient = new HttpClient())
                                {
                                    using (var response = await httpClient.PutAsJsonAsync(_endPoints.CompanyEndpoints + "/UpdateCompanyResume/" + CompanyID, collection))
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
                        return Json(new { status = "error", message = "error" });
                        //throw;
                    }
                }

                return Json(new { status = "success", message = "success" });
            }
            else
            {
                var list = new List<string>();
                foreach (var modelStateVal in ViewData.ModelState.Values)
                {
                    list.AddRange(modelStateVal.Errors.Select(error => error.ErrorMessage));
                }
                return Json(new { status = "error", errors = list });
            }
        }


    }
}
