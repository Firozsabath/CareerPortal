using CudJobUI.Contracts;
using CudJobUI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CudJobUI.Controllers
{
    public class AccountController : Controller
    {
        //public ILoggerSer _Logger { get; }
        public IStaticEndPoints _endPoints { get; }
        public ICustomFunctions _CusFunctions { get; }

        public AccountController(
            //ILogger logger, 
            IStaticEndPoints endPoints,
            ICustomFunctions CusFunctions

            )
        {
            //_Logger = logger;
            _endPoints = endPoints;
            _CusFunctions = CusFunctions;
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            Accesstoken token = new Accesstoken();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(_endPoints.AccountEndpoints + "/Login", user))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        token = JsonConvert.DeserializeObject<Accesstoken>(apiResponse);
                        if (token.token != null)
                        {
                            Response.Cookies.Append("X-Access-Token", token.token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                            if (user.LoginType == "Student")
                            {
                                if (token.ID != 0)
                                {
                                    HttpContext.Session.SetInt32("ProfileID", token.ID);
                                    HttpContext.Session.SetString("EmailID", user.EmailID);
                                    HttpContext.Session.SetString("LoginType", "Student");
                                    return RedirectToAction("Index", "Job");
                                }
                                else
                                {
                                    using (var cams_response = await httpClient.GetAsync(_endPoints.Cams_Integration + "/GetByEmail/" + user.EmailID))
                                    {
                                        //Student_Cams CudStdDetails = new Student_Cams();
                                        StudentDetails_Cams CudStdDetails = new StudentDetails_Cams();
                                        string cams_apiResponse = await cams_response.Content.ReadAsStringAsync();
                                        CudStdDetails = JsonConvert.DeserializeObject<StudentDetails_Cams>(cams_apiResponse);

                                        if (CudStdDetails != null)
                                        {
                                            HttpContext.Session.SetString("EmailID", user.EmailID);
                                            HttpContext.Session.SetString("LoginType", "Student");
                                            TempData["student"] = JsonConvert.SerializeObject(CudStdDetails);
                                            return RedirectToAction("Refeerence", "Student");
                                        }
                                    }

                                }
                            }
                            else if (user.LoginType == "Company")
                            {

                                if (token.ID != 0)
                                {
                                    HttpContext.Session.SetInt32("ProfileID", token.ID);
                                    HttpContext.Session.SetString("EmailID", user.EmailID);
                                    HttpContext.Session.SetString("LoginType", "Company");
                                    return RedirectToAction("Details", "Company", new { id = token.ID });

                                }
                                else
                                {
                                    HttpContext.Session.SetString("EmailID", user.EmailID);
                                    HttpContext.Session.SetString("LoginType", "Company");
                                    return RedirectToAction("Create", "Company");
                                }

                            }
                            else if (user.LoginType == "Admin")
                            {

                                if (token.ID == 1)
                                {
                                    HttpContext.Session.SetInt32("ProfileID", token.ID);
                                    HttpContext.Session.SetString("EmailID", user.EmailID);
                                    HttpContext.Session.SetString("LoginType", "Admin");
                                    return RedirectToAction("Index", "Admin");

                                }

                            }
                        }
                        else
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                ViewBag.Unauthorized = "Unauthorized";
                            }
                        }
                    }
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> sample()
        {
            return View();
        }


        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegister collection)
        {
            Accesstoken token = new Accesstoken();
            try
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.AccountEndpoints + "/Register", collection))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            //token = JsonConvert.DeserializeObject<Accesstoken>(apiResponse);                            
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                //Response.Cookies.Append("X-Access-Token", token.token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                                HttpContext.Session.SetString("EmailID", collection.EmailID);
                                HttpContext.Session.SetString("LoginType", "Company");
                                return RedirectToAction("Create", "Company");

                            }
                            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                List<ErroList> ErrorList = JsonConvert.DeserializeObject<List<ErroList>>(apiResponse);
                                foreach (var errors in ErrorList)
                                {
                                    ModelState.AddModelError("", errors.Description);
                                }
                            }
                            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                            {
                                ModelState.AddModelError("", "An e-mail with this emailID already exists!");
                            }

                        }
                    }
                }

                return View();
            }
            catch (Exception Ex)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            foreach (var cookieKey in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookieKey);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult ErrorPage()
        {

            return View();
        }

        [HttpGet]
        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordReset(ForgotPassword modeldata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.AccountEndpoints + "/GeneratepasswordToken", modeldata))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                return RedirectToAction("PasswordConfirmation", "Account", new { email = modeldata.Email, token = apiResponse });
                            }
                            else
                            {
                                ModelState.AddModelError("", "Invalid Email ID.");
                                return View();
                            }

                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PasswordConfirmation(string email, string token)
        {
            //ViewBag.resetSuccess = "success";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordConfirmation(Passwordconfirmation data)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.PostAsJsonAsync(_endPoints.AccountEndpoints + "/ResetPassword", data))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            //var passwordLink = Url.Action("PasswordConfirmation", "Account", new { email = modeldata.Email, token = apiResponse }, Request.Scheme);
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                ViewBag.resetSuccess = "success";
                                return View();
                            }
                            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                List<ErroList> ErrorList = JsonConvert.DeserializeObject<List<ErroList>>(apiResponse);
                                foreach (var errors in ErrorList)
                                {
                                    ModelState.AddModelError("", errors.Description);
                                }
                                return View();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
