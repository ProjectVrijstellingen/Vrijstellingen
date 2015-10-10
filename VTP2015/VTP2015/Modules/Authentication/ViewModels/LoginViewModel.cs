using System.ComponentModel.DataAnnotations;

namespace VTP2015.Modules.Authentication.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage="Gelieve een correct e-mailadres in te geven")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Blijf ingelogd")]
        public bool RememberMe { get; set; }
    }
}