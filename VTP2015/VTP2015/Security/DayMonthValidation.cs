using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VTP2015.Security
{
    public class DayMonthValidation:RequiredAttribute
    {
        private static readonly int[] daysInMonth = {31,28,31,30,31,30,31,31,30,31,30,31};
        private string _dayMonth;
        public override bool IsValid(object value)
        {
            _dayMonth = value as string;
            return _dayMonth != null && RegEx(_dayMonth) && CorrectMonth(_dayMonth) && CorrectDay(_dayMonth);
        }
        
        public override string FormatErrorMessage(string name)
        {
            if (_dayMonth == null) return "Vul een dag en maand in bij " + name + "!";
            if (!RegEx(_dayMonth)) return "De datum van " + name + " staat niet in het juiste formaat. bv: 05/06!";
            if (!CorrectMonth(_dayMonth)) return "hou rekening bij " + name + "dat er maar 12 maanden zijn!";
            var month = int.Parse(_dayMonth.Split('/')[1]);
            if (!CorrectDay(_dayMonth)) return "Bij " + name + " zijn er in die maand maar " + daysInMonth[month] + "dagen!";
            return "ERROR!";
        }

        private static bool RegEx(string dayMonth)
        {
            var regex = new Regex(@"^([0-3]?[1-9]+\/+[0-1]?[1-9])$");
	        var match = regex.Match(dayMonth);
            return match.Success;
        }

        private static bool CorrectMonth(string dayMonth)
        {
            var month = int.Parse(dayMonth.Split('/')[1]);
            return 0 < month && month < 13;
        }

        private static bool CorrectDay(string dayMonth)
        {
            var array = dayMonth.Split('/');
            var day = int.Parse(array[0]);
            var month = int.Parse(array[1]);
            return 0 < day && day <= daysInMonth[month - 1];
        }
    }
}