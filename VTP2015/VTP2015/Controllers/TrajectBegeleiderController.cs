using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RazorPDF;
using VTP2015.Config;
using VTP2015.Helpers;
using VTP2015.ServiceLayer;
using VTP2015.ViewModels.TrajectBegeleider;

namespace VTP2015.Controllers
{
    [Authorize(Roles = "Counselor")]
    [RoutePrefix("Counselor")]
    public class TrajectBegeleiderController : Controller
    {
        private readonly ICounselorFacade _counselorFacade;
        private readonly ConfigFile _configFile;
        private readonly MailHelper _mailHelper;

        public TrajectBegeleiderController(ICounselorFacade counselorFacade)
        {
            _counselorFacade = counselorFacade;
            _configFile = new ConfigFile();
            _mailHelper = new MailHelper();
        }

        //
        // GET: /Counselor/
        [Route("")]
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [Route("OpleidingSelectWidget")]
        [HttpGet]
        public PartialViewResult OpleidingSelectWidget()
        {
            var viewModel = new OpleidingSelectViewModel
            {
                SelectedOpleiding = _counselorFacade.GetEducationNameByStudentEmail(User.Identity.Name),
                Opleidingen = _counselorFacade.GetEducations()
                    .Project().To<OpleidingViewModel>().ToList()
            };

            return PartialView(viewModel);
        }

        [Route("ChangeOpleiding")]
        [HttpPost]
        public ActionResult ChangeOpleiding(string opleiding)
        {
            //TODO: 1 repository call
            _counselorFacade.ChangeEducation(User.Identity.Name, opleiding);
            return Json("Changed!");
        }

        [Route("DossierOverviewWidget")]
        [HttpGet]
        public PartialViewResult DossierOverviewWidget()
        {
            var models = _counselorFacade.GetFileByCounselorEmail(User.Identity.Name, _configFile.AcademieJaar())
                .Project().To<DossierOverviewViewModel>();

            return PartialView(models);

        }

        [Route("AanvraagDetailsWidget")]
        [HttpGet]
        public PartialViewResult AanvraagDetailsWidget()
        {
            var models = _counselorFacade.GetRequests()
                .Project().To<AanvraagDetailsViewModel>();

            return PartialView(models);
        }

        [Route("SendReminder")]
        [HttpPost]
        public ActionResult SendReminder(int aanvraagId)
        {
            _counselorFacade.SendReminder(aanvraagId);
            return Content("");
            //string email = _aanvraagRepository.GetEmailByAanvraagId(aanvraagId);
            //TimeSpan passedTimeSinceLastEmail = DateTime.Now.Subtract(_docentRepository.GetByEmail(email).WarningMail);
            //if (_configFile.WarningMailTimeIsAllowed(passedTimeSinceLastEmail))
            //{
            //    string bodyText = "Geachte \r \r ";
            //    string begeleider = User.Identity.Name;
            //    int aantalAanvragenWachtend = _aanvraagRepository.GetOnbehandeldeAanvragen(email).Count();
            //    string dringendeAanvraagPartimNaam = _aanvraagRepository.GetAanvraagById(aanvraagId).PartimInformation.Partim.Name;
            //    string dringendeAanvraagAanvragerNaam = _aanvraagRepository.GetAanvraagById(aanvraagId).File.Student.Email;

            //    bodyText += begeleider + " Wenst u er van op de hoogte te brengen dat de aanvraag betreffende " +
            //                    dringendeAanvraagPartimNaam + " aangevraagd door " +
            //                    dringendeAanvraagAanvragerNaam + " op dringende keuring wacht! Verder wachten er nog " +
            //                    aantalAanvragenWachtend + " aanvragen op uw keuring. U kunt deze aanvragen bekijken en keuren op het vrijstellingen web platform."
            //                    + "\r \r (Deze mail werd verstuurd vanop het webplatform op vraag van de betreffende trajectbegeleider, antwoorden op dit emailadres worden niet gelezen.)";
            //    _mailHelper.sendEmail(begeleider, email, bodyText);
            //    _docentRepository.ChangeWarningTime(email, DateTime.Now);

            //    return Content(email + " ontvangt een email met een herinnering.");
            //}
            //return Content(email + " heeft reeds minder dan " + _configFile.GetConfig().WarningMailFrequency + " (Dagen/Uren/Minuten) geleden een herinnering ontvangen. " + 
            //    "De administrator van dit platform verhindert dat u momenteel een email kunt sturen om spam tegen te gaan");
        }

        [Route("PrintDossier")]
        [HttpGet]
        public ActionResult PrintDossier()
        {
            var viewModel = new PdfViewModel
            {
                Naam = "Bockland",
                Voornaam = "Joachim",
                Email = User.Identity.Name,
                Tel = "null"
            };
            return new PdfResult(viewModel,"PrintDossier");
        }
    }
}