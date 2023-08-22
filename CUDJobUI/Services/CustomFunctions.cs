using CudJobUI.Contracts;
using CudJobUI.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CudJobUI.Services
{
    public class CustomFunctions : ICustomFunctions
    {
        IHttpContextAccessor _contextAccessor;
        public CustomFunctions(IStaticEndPoints endPoints, IHttpContextAccessor contextAccessor)
        {
            _endPoints = endPoints;
            _contextAccessor = contextAccessor;
        }

        public IStaticEndPoints _endPoints { get; }

        public string Converttolastupdated(DateTime updateddate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - updateddate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        public async Task<bool> isStudentCreated(string EmailID)
        {
            bool isExists = false;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.StudentPersonalEndpoints + "/isCreated/" + EmailID))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    //Studentlist = JsonConvert.DeserializeObject<List<StudentProfile>>(apiResponse);
                    isExists = Convert.ToBoolean(apiResponse); 
                }
            }

            return isExists;
        }

        public string Lastupdated(DateTime startdate, DateTime enddate)
        {
            var tot = Math.Round((startdate - enddate).TotalHours);
            string Lastupdatedtime = string.Empty;            
            if (tot >= 24)
            {
                Lastupdatedtime = $"{Math.Round((startdate - enddate).TotalDays).ToString()} Days";
            }
            else if (tot > 1 && tot < 24)
            {
                Lastupdatedtime = $"{Math.Round((startdate - enddate).TotalHours).ToString()} Hours";
            }
            else if(tot < 1)
            {
                Lastupdatedtime = $"{Math.Round((startdate - enddate).TotalMinutes).ToString()} Minutes";
            }           

            return Lastupdatedtime;
            
        }

        public async Task<bool> setStudentState(int id)
        {
            StudentProfile studentprofile = new StudentProfile();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_endPoints.StudentEndpoints + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    studentprofile = JsonConvert.DeserializeObject<StudentProfile>(apiResponse);
                }
            }

            var context = _contextAccessor.HttpContext;

            if (studentprofile.StudentPersonal.UpdatedDate != null)
            {
                context.Session.SetString("Lastupdated", Lastupdated(DateTime.Now, Convert.ToDateTime(studentprofile.StudentPersonal.UpdatedDate)));
            }
            if (studentprofile.StudentPersonal.Resumepath != null)
            {
                context.Session.SetString("ResumeName", Path.GetFileName(studentprofile.StudentPersonal.Resumepath));
            }
            if (studentprofile.StudentPersonal.profileImgpath != null && studentprofile.StudentPersonal.profileImgpath != string.Empty)
            {
                context.Session.SetString("ProfileImg", Path.GetFileName(studentprofile.StudentPersonal.profileImgpath));
            }
            context.Session.SetString("usrFirstName", studentprofile.StudentPersonal.FirstName);
            context.Session.SetString("usrLastName", studentprofile.StudentPersonal.LastName);

            return true;
        }

        public double TotalPercent(int Outof, int Total)
        {
            double x = Convert.ToDouble((float)Outof/(float)Total);
            return Math.Round(x * 100);
        }       
    }
}
