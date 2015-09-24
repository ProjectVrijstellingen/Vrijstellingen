namespace VTP2015.ViewModels.Admin
{
    public class UserViewModel:IViewModel
    {
        public string Email { get; set; }
        public bool IsBegeleider { get; set; }
        public bool IsAdmin { get; set; }
    }
}