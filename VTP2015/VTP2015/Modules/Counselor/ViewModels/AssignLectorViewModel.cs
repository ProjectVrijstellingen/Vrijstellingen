using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class AssignLectorViewModel
    {
        [Display(Name = "supercode")]
        [Required]
        public string SuperCode { get; set; }

        [Display(Name = "email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}