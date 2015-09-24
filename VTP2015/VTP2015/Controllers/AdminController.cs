using System.Linq;
using System.Web.Mvc;
using VTP2015.Config;
using VTP2015.Identity;
using VTP2015.Repositories.Interfaces;
using VTP2015.ViewModels.Admin;

namespace VTP2015.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IdentityManager _im = new IdentityManager();
        private readonly ConfigFile _configFile;

        public AdminController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            _configFile = new ConfigFile();
        }

        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("UserTableWidget")]
        public ActionResult UserTableWidget()
        {
            var viewModel = (from user in _im.GetUsers().ToList()
                where user.Email.EndsWith("@howest.be")
                select
                    new UserViewModel
                    {
                        Email = user.Email,
                        IsAdmin = _im.HasRole(user.UserName, "Admin"),
                        IsBegeleider = _im.HasRole(user.UserName, "Counselor")
                    }).ToList();
            return PartialView(viewModel);
        }

        [HttpPost]
        [Route("AddUserToRole")]
        public ActionResult AddUserToRole(string email, string role)
        {
            if(email == User.Identity.Name) return Json("False");
            if(role=="Counselor") _loginRepository.AddBegeleider(email);
            return Json(_im.AddUserToRole(_im.GetUsers().Where(x => x.Email.EndsWith("@howest.be")).First(x => x.Email == email).Id, role));
        }

        [HttpPost]
        [Route("RemoveUserFromRole")]
        public ActionResult RemoveUserFromRole(string email, string role)
        {
            if (email == User.Identity.Name) return Json("False");
            if (role == "Counselor") _loginRepository.RemoveBegeleider(email);
            return Json(_im.DeleteUserFromRole(_im.GetUsers().Where(x => x.Email.EndsWith("@howest.be")).First(x => x.Email == email).Id, role));
        }

        [HttpPost]
        [Route("SubmitConfig")]
        public ActionResult SubmitConfig(ConfigViewModel viewModel)
        {
            ModelState.Clear();
            var validation = TryValidateModel(viewModel);
            var errors = (from modelstate in ModelState.Values from error in modelstate.Errors select error.ErrorMessage).ToList();

            if (!validation) return Json(errors.ToArray());

            var jsonobj = new DefaultConfig
            {
                EindeVrijstellingDayMonth = viewModel.EindeVrijstellingDayMonth,
                InfoMailFrequency = viewModel.InfoMailFrequency,
                WarningMailFrequency = viewModel.WarningMailFrequency,
                StartVrijstellingDayMonth = viewModel.StartVrijstellingDayMonth
            };
            _configFile.SaveToConfig(jsonobj);
            
            errors.Add("Finish");
            return Json(errors.ToArray());
        }

        [HttpGet]
        [Route("SettingsWidget")]
        public ActionResult SettingsWidget()
        {
            var jsonobj = _configFile.GetConfig();
            var viewModel = new ConfigViewModel
            {
                EindeVrijstellingDayMonth = jsonobj.EindeVrijstellingDayMonth,
                InfoMailFrequency = jsonobj.InfoMailFrequency,
                WarningMailFrequency = jsonobj.WarningMailFrequency,
                StartVrijstellingDayMonth = jsonobj.StartVrijstellingDayMonth
            };
            return PartialView(viewModel);
        }
    }
}