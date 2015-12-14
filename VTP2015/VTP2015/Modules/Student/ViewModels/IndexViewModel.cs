using System.ComponentModel.DataAnnotations;
using System.Web;
using VTP2015.Modules.Shared;
using VTP2015.Security;

namespace VTP2015.Modules.Student.ViewModels
{
    public class IndexViewModel : IViewModel
    {
        [FileValidation]
        [Display(Name = "Upload bewijs")]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Argumentatie")]
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Geef een omschrijving van min. 5 en max 30 tekens!")]
        public string Description { get; set; }
    }
}