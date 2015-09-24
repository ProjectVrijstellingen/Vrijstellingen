using System.ComponentModel.DataAnnotations;
using VTP2015.Config;
using VTP2015.Validation;

namespace VTP2015.ViewModels.Admin
{
    public class ConfigViewModel:IDefaultConfig
    {   
        [Display(Name="Info email frequency (dd:hh:mm)")]
        [Required]
        [RegularExpression(@"[0-9]{2}:([0-1][0-9]|2[0-4]):[0-5][0-9]", ErrorMessage = "Info email frequency moet in het formaat dd:hh:mm staan!")]
        public string InfoMailFrequency { get; set; }

        [Display(Name = "Warning email frequency (dd:hh:mm)")]
        [Required]
        [RegularExpression(@"[0-9]{2}:([0-1][0-9]|2[0-4]):[0-5][0-9]", ErrorMessage = "warning email frequency moet in het formaat dd:hh:mm staan!")]
        public string WarningMailFrequency { get; set; }

        [Display(Name = "Start vrijstellingen")]
        [DayMonthValidation]
        public string StartVrijstellingDayMonth { get; set; }

        [Display(Name = "Einde vrijstellingen")]
        [DayMonthValidation]
        public string EindeVrijstellingDayMonth { get; set; }
    }
}