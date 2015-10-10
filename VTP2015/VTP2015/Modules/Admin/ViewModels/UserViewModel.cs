using VTP2015.Modules.Shared;

namespace VTP2015.Modules.Admin.ViewModels
{
    public class UserViewModel:IViewModel
    {
        public string Email { get; set; }
        public bool IsCounselor { get; set; }
        public bool IsAdmin { get; set; }
    }
}