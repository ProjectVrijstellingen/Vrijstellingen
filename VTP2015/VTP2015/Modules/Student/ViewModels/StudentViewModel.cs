using System.ComponentModel.DataAnnotations;

namespace VTP2015.Modules.Student.ViewModels
{
    public class StudentViewModel
    {
        public string Name { get; set; }
        
        public string FirstName { get; set; }
        
        public string Email { get; set; }
        
        public string Education { get; set; }

        public string Counselor { get; set; }
    }
}