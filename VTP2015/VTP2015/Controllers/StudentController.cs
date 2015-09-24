using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using VTP2015.Config;
using VTP2015.Entities;
using VTP2015.Helpers;
using VTP2015.Repositories.Interfaces;
using VTP2015.ViewModels.Student;

namespace VTP2015.Controllers
{
    [Authorize(Roles = "Student")]
    [RoutePrefix("Student")]
    public class StudentController : Controller
    {
        private readonly IAanvraagRepository _aanvraagRepository;
        private readonly IBewijsRepository _bewijsRepository;
        private readonly IDossierRepository _dossierRepository;
        private readonly IPartimInformatieRepository _partimInformatieRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IOpleidingRepository _opleidingRepository;
        private readonly IDocentRepository _docentRepository;
        private readonly MailHelper _mailHelper;
        private readonly ConfigFile _configFile;



        public StudentController(IAanvraagRepository aanvraagRepository, IBewijsRepository bewijsRepository, IDossierRepository dossierRepository,
            IStudentRepository studentRepository, IPartimInformatieRepository partimInformatieRepository,IOpleidingRepository opleidingRepository,
            IDocentRepository docentRepository)
        {
            _aanvraagRepository = aanvraagRepository;
            _bewijsRepository = bewijsRepository;
            _dossierRepository = dossierRepository;
            _studentRepository = studentRepository;
            _partimInformatieRepository = partimInformatieRepository;
            _opleidingRepository = opleidingRepository;
            _docentRepository = docentRepository;
            _configFile = new ConfigFile();
            _mailHelper = new MailHelper();
        }

        #region index
        [Route("")]
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [Route("")]
        [HttpPost]
        public ActionResult AddBewijs(IndexViewModel viewModel)
        {

            ModelState.Clear();
            var validation = TryValidateModel(viewModel);
            var errors = (from modelstate in ModelState.Values from error in modelstate.Errors select error.ErrorMessage).ToList();

            if (!validation) return Json(errors.ToArray());

            var pic = Path.GetFileName(viewModel.File.FileName);
            var name = User.Identity.Name.Split('@')[0];
            var path = Path.Combine(Server.MapPath("/bewijzen/" + name), pic);

            if (System.IO.File.Exists(path))
            {
                errors.Add("File does already exist!");
                return Json(errors.ToArray());
            }
            viewModel.File.SaveAs(path);

            var studentId = _studentRepository.GetStudentIdByEmail(User.Identity.Name);

            var dbBewijs = new Bewijs
            {
                StudentId = studentId,
                Path = pic,
                Omschrijving = viewModel.Omschrijving
            };

            _bewijsRepository.Insert(dbBewijs);
            errors.Add("Finish");
            return Json(errors.ToArray());
        }

        [Route("DeleteBewijs")]
        [HttpPost]
        public ActionResult DeleteBewijs(int bewijsId)
        {
            if (!_bewijsRepository.IsBewijsFromStudent(User.Identity.Name))
                return Content("bewijs bestaat niet voor gebruiker!");

            var name = User.Identity.Name.Split('@')[0];
            var path = Path.Combine(Request.MapPath("/bewijzen/" + name), _bewijsRepository.Delete(bewijsId));

            if (!System.IO.File.Exists(path)) return Content("gegeven bestand kon niet verwijdert worden!");

            System.IO.File.Delete(path);
            return Content("Voltooid!");
        }

        [Route("DossierWidget")]
        [HttpGet]
        public PartialViewResult DossierWidget()
        {
            var models = _dossierRepository.GetByStudent(User.Identity.Name)
                .Project().To<DossierListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("BewijsListWidget")]
        [HttpGet]
        public PartialViewResult BewijsListWidget()
        {
            var models = _bewijsRepository.GetByStudent(User.Identity.Name)
                .Project().To<BewijsListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("BewijsToevoegenWidget")]
        [HttpGet]
        public PartialViewResult BewijsToevoegenWidget()
        {
            return PartialView();
        }

        #endregion

        #region dossier
        [Route("Dossier/{dossierId}")]
        [HttpGet]
        public ActionResult Dossier(int dossierId)
        {
            if (!_dossierRepository.IsDossierFromStudent(User.Identity.Name, dossierId)) return RedirectToAction("Index");
            return View();
        }

        [Route("AangevraagdePartimsWidget")]
        [HttpGet]
        public ActionResult AangevraagdePartimsWidget(int dossierId)
        {
            if (!_dossierRepository.IsDossierFromStudent(User.Identity.Name, dossierId)) return RedirectToAction("Index");
            var models = _partimInformatieRepository.GetAangevraagdePartims(User.Identity.Name, dossierId)
                .Project().To<PartimViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("BeschikbarePartimsWidget")]
        [HttpGet]
        public ActionResult BeschikbarePartimsWidget(int dossierId)
        {
            if (!_dossierRepository.IsDossierFromStudent(User.Identity.Name, dossierId)) return RedirectToAction("Index");
            var models = _partimInformatieRepository.GetBeschikbarePartims(User.Identity.Name, dossierId)
                .Project().To<PartimViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("BewijsSelecterenWidget")]
        [HttpGet]
        public PartialViewResult BewijsSelecterenWidget()
        {
            var models = _bewijsRepository.GetByStudent(User.Identity.Name)
                .Project().To<BewijsListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("AanvraagDetailWidget")]
        [HttpGet]
        public PartialViewResult AanvraagDetailWidget(int dossierId)
        {
            var models = _aanvraagRepository.GetByDossierId(dossierId)
                .Project().To<AanvraagDetailViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("NieuwDossier")]
        [HttpGet]
        public ActionResult NieuwDossier()
        {
            var configFile = new ConfigFile();
            var academieJaar = configFile.AcademieJaar();
            if(!_partimInformatieRepository.SyncStudentPartims(User.Identity.Name,academieJaar)) return RedirectToAction("Index");
            var studentId = _studentRepository.GetStudentIdByEmail(User.Identity.Name);
            var dossier = new Dossier
            {
                StudentId = studentId,
                AanmaakDatum = DateTime.Now,
                Opleiding = _studentRepository.GetByEmail(User.Identity.Name).Opleiding,
                KeuzeTraject = null,
                Editable = true,
                AcademieJaar = academieJaar
            };

            _dossierRepository.Insert(dossier);

            return this.RedirectToAction(c => c.Dossier(dossier.DossierId));
        }

        [Route("SaveAanvraag")]
        [HttpPost]
        public ActionResult SaveAanvraag(AanvraagViewModel viewModel)
        {
            if(viewModel.Bewijzen == null) return Content("Geen bewijzen!");
            viewModel.Bewijzen = viewModel.Bewijzen.Distinct().ToArray();

            var bewijzen = viewModel.Bewijzen.Select(bewijs => _bewijsRepository.GetById(bewijs)).ToList();

            var aanvraag = new Aanvraag
            {
                DossierId = viewModel.DossierId,
                PartimInformatie = _partimInformatieRepository.GetBySuperCode(viewModel.SuperCode),
                Argumentatie = viewModel.Argumentatie,
                LastChanged = DateTime.Now,
                Bewijzen = bewijzen
            };

            return Content(!_aanvraagRepository.SyncAanvraagInDossier(aanvraag) ? "Fout!" : "Opgeslaan!");

            //if (!_mailRepository.EmailExists(aanvraag.PartimInformatie.Docent.Email))
            //{
            //    _mailRepository.AddDocent(aanvraag.PartimInformatie.Docent.Email);
            //}
            //System.TimeSpan passedTimeSinceLastEmail = System.DateTime.Now.Subtract(_mailRepository.GetByEmail(aanvraag.PartimInformatie.Docent.Email).WarningMail);
            //if (_configFile.WarningMailTimeIsAllowed(passedTimeSinceLastEmail))
            //{
            //    string bodyText = "Geachte \r \r Een nieuwe aanvraag betreffende " +
            //        aanvraag.PartimInformatie.Docent.Email + " vereist uw goedkeuring. Verder heeft u nog steeds " +
            //        _aanvraagRepository.GetOnbehandeldeAanvragen(aanvraag.PartimInformatie.Docent.Email).Count() + " openstaande aanvragen." +
            //        " U kunt deze aanvragen keuren op het online webplatform" +
            //        "\r \r (Deze mail werd verstuurd vanop het webplatform op vraag van de betreffende trajectbegeleider, antwoorden op dit emailadres worden niet gelezen.)";

            //    _mailHelper.sendEmail(User.Identity.Name, aanvraag.PartimInformatie.Docent.Email, bodyText);
            //}
        }

        [Route("Delete")]
        [HttpPost]
        public ActionResult DeleteAanvraag(int dossierId, string supercode)
        {
            return !_studentRepository.IsAanvraagFromStudent(dossierId,supercode, User.Identity.Name) ? Content("Don't cheat!") : Content(!_aanvraagRepository.Delete(dossierId, supercode) ? "Aanvraag bestaat niet!" : "Voltooid!");
        }

        #endregion
    }
}