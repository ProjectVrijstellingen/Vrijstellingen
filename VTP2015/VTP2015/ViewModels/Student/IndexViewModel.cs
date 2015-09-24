using System.ComponentModel.DataAnnotations;
using System.Web;
using VTP2015.Validation;

namespace VTP2015.ViewModels.Student
{
    public class IndexViewModel : IViewModel
    {
        [FileValidation]
        [Display(Name = "Upload Image")]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Description")]
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Geef een omschrijving van min. 5 en max 30 tekens!")]
        public string Omschrijving { get; set; }
    }
}