using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using VTP2015.Config;
using VTP2015.Entities;
using VTP2015.ServiceLayer;
using VTP2015.ServiceLayer.Interfaces;
using VTP2015.ViewModels.Student;
using File = VTP2015.Entities.File;

namespace VTP2015.Controllers
{
    [Authorize(Roles = "Student")]
    [RoutePrefix("Student")]
    public class StudentController : Controller
    {
        private readonly IStudentFacade _studentFacade;

        public StudentController(IStudentFacade studentFacade)
        {
            _studentFacade = studentFacade;
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
        public ActionResult AddFile(IndexViewModel viewModel)
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

            var studentId = _studentFacade.GetStudentCodeByEmail(User.Identity.Name);

            var dbBewijs = new Evidence
            {
                StudentId = studentId,
                Path = pic,
                Description = viewModel.Omschrijving
            };

            _studentFacade.InsertEvidence(dbBewijs);
            errors.Add("Finish");
            return Json(errors.ToArray());
        }

        [Route("DeleteEvidence")]
        [HttpPost]
        public ActionResult DeleteEvidence(int bewijsId)
        {
            if (!_studentFacade.IsEvidenceFromStudent(User.Identity.Name))
                return Content("bewijs bestaat niet voor gebruiker!");

            var mapPath = Request.MapPath("/bewijzen/" + User.Identity.Name.Split('@')[0]);

            return Content(!_studentFacade.DeleteEvidence(bewijsId, mapPath)
                ? "gegeven bestand kon niet verwijdert worden!"
                : "Voltooid!");
        }

        [Route("DossierWidget")]
        [HttpGet]
        public PartialViewResult DossierWidget()
        {
            var models = _studentFacade.GetFilesByStudentEmail(User.Identity.Name)
                .Project().To<DossierListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("BewijsListWidget")]
        [HttpGet]
        public PartialViewResult BewijsListWidget()
        {
            var models = _studentFacade.GetEvidenceByStudentEmail(User.Identity.Name)
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
        [Route("File/{dossierId}")]
        [HttpGet]
        public ActionResult Dossier(int dossierId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, dossierId)) return RedirectToAction("Index");
            return View();
        }

        [Route("AangevraagdePartimsWidget")]
        [HttpGet]
        public ActionResult AangevraagdePartimsWidget(int dossierId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, dossierId)) return RedirectToAction("Index");
            var models = _studentFacade.GetRequestedPartims(User.Identity.Name, dossierId)
                .Project().To<PartimViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("BeschikbarePartimsWidget")]
        [HttpGet]
        public ActionResult BeschikbarePartimsWidget(int dossierId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, dossierId)) return RedirectToAction("Index");
            var models = _studentFacade.GetAvailablePartims(User.Identity.Name, dossierId)
                .Project().To<PartimViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("BewijsSelecterenWidget")]
        [HttpGet]
        public PartialViewResult BewijsSelecterenWidget()
        {
            var models = _studentFacade.GetEvidenceByStudentEmail(User.Identity.Name)
                .Project().To<BewijsListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("AanvraagDetailWidget")]
        [HttpGet]
        public PartialViewResult AanvraagDetailWidget(int dossierId)
        {
            var models = _studentFacade.GetRequestsByFileId(dossierId)
                .Project().To<AanvraagDetailViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("NieuwDossier")]
        [HttpGet]
        public ActionResult NieuwDossier()
        {
            var configFile = new ConfigFile();
            var academieJaar = configFile.AcademieJaar();
            if(!_studentFacade.SyncStudentPartims(User.Identity.Name,academieJaar)) return RedirectToAction("Index");
            var studentId = _studentFacade.GetStudentCodeByEmail(User.Identity.Name);
            var dossier = new File
            {
                StudentId = studentId,
                DateCreated = DateTime.Now,
                Specialization = "",
                Editable = true,
                AcademicYear = academieJaar
            };

            _studentFacade.InsertFile(dossier);

            return this.RedirectToAction(c => c.Dossier(dossier.Id));
        }

        [Route("SaveAanvraag")]
        [HttpPost]
        public ActionResult SaveAanvraag(AanvraagViewModel viewModel)
        {
            if(viewModel.Bewijzen == null) return Content("Geen bewijzen!");
            viewModel.Bewijzen = viewModel.Bewijzen.Distinct().ToArray();

            var bewijzen = viewModel.Bewijzen.Select(bewijsId => _studentFacade.GetEvidenceById(bewijsId)).ToList();

            var aanvraag = new Request
            {
                FileId = viewModel.DossierId,
                PartimInformation = _studentFacade.GetPartimInformationBySuperCode(viewModel.SuperCode),
                Argumentation = viewModel.Argumentatie,
                LastChanged = DateTime.Now,
                Evidence = bewijzen
            };

            return Content(!_studentFacade.SyncRequestInFile(aanvraag) ? "Don't cheat!" : "Saved!");

            //if (!_mailRepository.EmailExists(aanvraag.PartimInformation.Lecturer.Email))
            //{
            //    _mailRepository.AddDocent(aanvraag.PartimInformation.Lecturer.Email);
            //}
            //System.TimeSpan passedTimeSinceLastEmail = System.DateTime.Now.Subtract(_mailRepository.GetByEmail(aanvraag.PartimInformation.Lecturer.Email).WarningMail);
            //if (_configFile.WarningMailTimeIsAllowed(passedTimeSinceLastEmail))
            //{
            //    string bodyText = "Geachte \r \r Een nieuwe aanvraag betreffende " +
            //        aanvraag.PartimInformation.Lecturer.Email + " vereist uw goedkeuring. Verder heeft u nog steeds " +
            //        _studentFacade.GetUntreadedRequests(aanvraag.PartimInformation.Lecturer.Email).Count() + " openstaande aanvragen." +
            //        " U kunt deze aanvragen keuren op het online webplatform" +
            //        "\r \r (Deze mail werd verstuurd vanop het webplatform op vraag van de betreffende trajectbegeleider, antwoorden op dit emailadres worden niet gelezen.)";

            //    _mailHelper.sendEmail(User.Identity.Name, aanvraag.PartimInformation.Lecturer.Email, bodyText);
            //}
        }

        [Route("Delete")]
        [HttpPost]
        public ActionResult DeleteAanvraag(int dossierId, string supercode)
        {
            return _studentFacade.IsRequestFromStudent(dossierId, supercode, User.Identity.Name)
                ? Content("Don't cheat!")
                : Content(!_studentFacade.DeleteRequest(dossierId, supercode)
                    ? "Request bestaat niet!"
                    : "Voltooid!");
        }

        #endregion
    }
}