using System.ComponentModel.DataAnnotations;
using VTP2015.Config;
using VTP2015.Security;

namespace VTP2015.Modules.Admin.ViewModels
{
    public class ConfigViewModel:IDefaultConfig
    {   
        [Display(Name="Info email frequentie (dd:uu:mm)")]
        [Required]
        [RegularExpression(@"[0-9]{2}:([0-1][0-9]|2[0-4]):[0-5][0-9]", ErrorMessage = "Info email frequentie moet in het formaat dd:uu:mm staan!")]
        public string InfoMailFrequency { get; set; }

        [Display(Name = "Waarschuwing email frequentie (dd:uu:mm)")]
        [Required]
        [RegularExpression(@"[0-9]{2}:([0-1][0-9]|2[0-4]):[0-5][0-9]", ErrorMessage = "waarschuwing email frequentie moet in het formaat dd:uu:mm staan!")]
        public string WarningMailFrequency { get; set; }

        [Display(Name = "Start vrijstellingen")]
        [DayMonthValidation]
        public string StartVrijstellingDayMonth { get; set; }

        [Display(Name = "Einde vrijstellingen")]
        [DayMonthValidation]
        public string EindeVrijstellingDayMonth { get; set; }
    }
}