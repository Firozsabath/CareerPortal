using CudJobUI.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CudJobUI.Services
{
    public class CustomFunctions : ICustomFunctions
    {
        public CustomFunctions(IStaticEndPoints endPoints)
        {
            _endPoints = endPoints;
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

        public double TotalPercent(int Outof, int Total)
        {
            double x = Convert.ToDouble((float)Outof/(float)Total);
            return Math.Round(x * 100);
        }
    }
}
