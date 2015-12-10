using System;
using System.IO;
using System.Security.Policy;
using System.Web;
using System.Web.Script.Serialization;

namespace VTP2015.Config
{
    public class ConfigFile
    {
        private static string _file;

        public ConfigFile()
        {
            _file = Path.Combine(HttpContext.Current.Server.MapPath("/"), "bewijzen/Config.json");
        }

        public bool InfoMailTimeIsAllowed(TimeSpan time)
        {
            return TimeSpanOver(time, GetConfig().InfoMailFrequency);
        }

        public bool WarningMailTimeIsAllowed(TimeSpan time)
        {
            return TimeSpanOver(time,GetConfig().WarningMailFrequency);
        }

        private static bool TimeSpanOver(TimeSpan time, string frequency)
        {
            var _frequency = frequency.Split(':');
            var days = int.Parse(_frequency[0]);
            if (time.Days < days) return false;
            var hours = int.Parse(_frequency[1]);
            if(time.Days == days && time.Hours<hours) return false;
            var minutes = int.Parse(_frequency[2]);
            return time.Days != days || time.Hours != hours || time.Minutes >= minutes;
        }

        public string AcademieJaar()
        {
            var today = DateTime.Now;
            var dayMonth = GetConfig().StartVrijstellingDayMonth.Split('/');
            if(today.Month > int.Parse(dayMonth[1]) || (today.Month == int.Parse(dayMonth[1]) && today.Day > int.Parse(dayMonth[0]))) return CalcAcademieJaar(today.Year);
            return CalcAcademieJaar(today.Year - 1);
        }

        public string CalcAcademieJaar(int year)
        {
            return year + "-" + (year+1)%100;
        }

        public void CreateDefaultConfig()
        {
            var jsonobj = new DefaultConfig
            {
                EindeVrijstellingDayMonth = "01/11",
                InfoMailFrequency = "07:00:00",
                WarningMailFrequency = "01:00:00",
                StartVrijstellingDayMonth = "01/08"
            };
            SaveToConfig(jsonobj);
        }

        public void SaveToConfig(DefaultConfig jsonobj)
        {
            var json = new JavaScriptSerializer().Serialize(jsonobj);
            System.IO.File.WriteAllText(_file, json);
        }

        public DefaultConfig GetConfig()
        {
            if (!System.IO.File.Exists(_file)) CreateDefaultConfig();
            var json = System.IO.File.ReadAllText(_file);
            return new JavaScriptSerializer().Deserialize<DefaultConfig>(json);
        }
    }
}