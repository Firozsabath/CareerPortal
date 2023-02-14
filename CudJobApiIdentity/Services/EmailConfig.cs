using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class EmailConfig : IEmailConfig
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILoggerService _logger;
        private readonly IConfiguration _configuration;

        public EmailConfig(IWebHostEnvironment hostingEnvironment, ILoggerService logger,IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _configuration = configuration;
        }
        public string SendEmail(EmailDTO email)
        {
            string status = string.Empty;
            string Emailcc = _configuration["Email:Emailcc"].ToString();
            MailMessage mm = new MailMessage("cudmailsender@cud.ac.ae", email.Recepient); // Message Body Initialisation.
            mm.Subject = email.Subject; //Adding Subject To the Mail/
                                        // mm.Body = body;       // Adding the Message Body.
            mm.Body = Createbody(email);       // Adding the Message Body.

            if(email.Type == "NewCompany" || email.Type == "NewJob")
            {
                //mm.Bcc.Add("studentcareers@cud.ac.a");
                mm.Bcc.Add(Emailcc);
            }           
            // mm.Attachments.Add(new Attachment(path)); //The Code is used to Attach the pdf to the mail.
            mm.IsBodyHtml = true;
            mm.BodyEncoding = Encoding.UTF8;
            SmtpClient smtp = new SmtpClient("smtp.office365.com", 25); // Initialising the SMTP
                                                                         //smtp.Host = "CUDSMTP01.cud.ae";            
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = _configuration["Email:EmailUserName"].ToString();
            NetworkCred.Password = _configuration["Email:EmailPassword"].ToString();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;            
            try
            {
                smtp.Send(mm); // Forwarding the Created mail to the recepient.
                status = "Success";
            }
            catch (Exception ex)
            {               
               _logger.LogError($"{ex.Message} - {ex.InnerException}");
            }

            return status;
            //return status;  
           
          
        }

        public string SendEmail_jobapprove(Jobs jobmodel)
        {
            EmailDTO email = new EmailDTO { Recepient = jobmodel.Companies.UserEmailID, Subject = "Job Approval", CompanyName = jobmodel.Title, Type = "JobApproval", Name = jobmodel.Companies.CompanyName };
            var emailsend = SendEmail(email);
            //EmailDTO email1 = new EmailDTO { Recepient = "studentcareers@cud.ac.ae", Subject = "Job Approval", CompanyName = jobmodel.Title, Type = "JobApproval", Name = jobmodel.Companies.CompanyName };
            //var emailsend1 = SendEmail(email1);

            return emailsend;
        }

        public string SendEmail_Companyapprove(Companies company)
        {
            EmailDTO email = new EmailDTO { Recepient = company.UserEmailID, Subject = "Company Approval", CompanyName = company.CompanyName, Type = "CompanyApproval", Name = company.CompanyContacts.FirstOrDefault().FirstName };
            var emailsend = SendEmail(email);
            //EmailDTO email1 = new EmailDTO { Recepient = "studentcareers@cud.ac.ae", Subject = "Company Approval", CompanyName = company.CompanyName, Type = "CompanyApproval", Name = company.CompanyContacts.FirstOrDefault().FirstName };
            //var emailsend1 = SendEmail(email1);

            return emailsend;
        }

        private string Createbody(EmailDTO email)
        {

            string body = "";

            if (email.Type == "NewCompany")
            {
                using (StreamReader reader = new StreamReader(_hostingEnvironment.ContentRootPath + "/Email_Templates/NewCompanyRegistration.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", email.CompanyName);
            }
            else if (email.Type == "NewStudent")
            {
                using (StreamReader reader = new StreamReader(_hostingEnvironment.ContentRootPath + "/Email_Templates/NewStudentRegistration.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", email.CompanyName);
            }
            else if (email.Type == "CompanyApproval")
            {
                using (StreamReader reader = new StreamReader(_hostingEnvironment.ContentRootPath + "/Email_Templates/NewCompanyApproval.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", email.CompanyName);
            }
            else if (email.Type == "NewJob")
            {
                using (StreamReader reader = new StreamReader(_hostingEnvironment.ContentRootPath + "/Email_Templates/NewJobPosting.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", email.Name);
                body = body.Replace("{JobTittle}", email.CompanyName);
            }
            else if (email.Type == "JobApproval")
            {
                using (StreamReader reader = new StreamReader(_hostingEnvironment.ContentRootPath + "/Email_Templates/NewJobApproval.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", email.Name);
                body = body.Replace("{JobTittle}", email.CompanyName);
            }
            else if (email.Type == "Applicationconfirmation")
            {
                using (StreamReader reader = new StreamReader(_hostingEnvironment.ContentRootPath + "/Email_Templates/JobApplicationConfirmation.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", email.Name);
                body = body.Replace("{Position}", email.PostionName);
                body = body.Replace("{company}", email.CompanyName);
            }
            else if (email.Type == "LicenseExpiry")
            {
                using (StreamReader reader = new StreamReader(_hostingEnvironment.ContentRootPath + "/Email_Templates/LicenseExpiry.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Name}", email.Name);         
            }
            return body;            
        }

        public string SendEmail_JobApplication(StudentSeekers student, Jobs jobmodel)
        {
            EmailDTO email = new EmailDTO { Recepient = student.EmailID, Subject = "Application Confirmation", CompanyName = jobmodel.Companies.CompanyName,PostionName = jobmodel.Title, Type = "Applicationconfirmation", Name = student.FirstName+' '+student.LastName };
            var emailsend = SendEmail(email);
            return emailsend;
        }
    }
}
