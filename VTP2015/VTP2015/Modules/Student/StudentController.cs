using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using VTP2015.Config;
using VTP2015.Modules.Student.ViewModels;
using VTP2015.ServiceLayer.Student;
using VTP2015.ServiceLayer.Student.Models;
using File = VTP2015.ServiceLayer.Student.Models.File;

namespace VTP2015.Modules.Student
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
                errors.Add("FileName does already exist!");
                return Json(errors.ToArray());
            }
            viewModel.File.SaveAs(path);

            var dbBewijs = new Evidence
            {
                StudentMail = User.Identity.Name,
                Path = pic,
                Description = viewModel.Description
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

        [Route("FileWidget")]
        [HttpGet]
        public PartialViewResult FileWidget()
        {
            var models = _studentFacade.GetFilesByStudentEmail(User.Identity.Name)
                .Project().To<FileListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("EvidenceListWidget")]
        [HttpGet]
        public PartialViewResult EvidenceListWidget()
        {
            var models = _studentFacade.GetEvidenceByStudentEmail(User.Identity.Name)
                .Project().To<EvidenceListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("AddFileWidget")]
        [HttpGet]
        public PartialViewResult AddFileWidget()
        {
            return PartialView();
        }

        #endregion

        #region dossier
        [Route("FileName/{dossierId}")]
        [HttpGet]
        public ActionResult File(int dossierId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, dossierId))
                return RedirectToAction("Index");

            return View();
        }

        [Route("RequestedPartimsWidget")]
        [HttpGet]
        public ActionResult RequestedPartimsWidget(int dossierId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, dossierId))
                return RedirectToAction("Index");

            var models = _studentFacade.GetPartims(User.Identity.Name, dossierId, PartimMode.Requested)
                .Project().To<PartimViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("AvailablePartimsWidget")]
        [HttpGet]
        public ActionResult AvailablePartimsWidget(int dossierId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, dossierId))
                return RedirectToAction("Index");

            var models = _studentFacade.GetPartims(User.Identity.Name, dossierId, PartimMode.Available)
                .Project().To<PartimViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("SelectEvidenceWidget")]
        [HttpGet]
        public PartialViewResult SelectEvidenceWidget()
        {
            var models = _studentFacade.GetEvidenceByStudentEmail(User.Identity.Name)
                .Project().To<EvidenceListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("RequestDetailWidget")]
        [HttpGet]
        public PartialViewResult RequestDetailWidget(int dossierId)
        {
            var models = _studentFacade.GetRequestByFileId(dossierId)
                .Project().To<RequestDetailViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("NewFile")]
        [HttpGet]
        public ActionResult NewFile()
        {
            var configFile = new ConfigFile();
            var academieJaar = configFile.AcademieJaar();

            if(!_studentFacade.SyncStudentPartims(User.Identity.Name,academieJaar))
                return RedirectToAction("Index");

            var education = _studentFacade.GetEducation(User.Identity.Name);
            var dossier = new File
            {
                StudentMail = User.Identity.Name,
                DateCreated = DateTime.Now,
                Education = education.Name,
                Editable = true,
                AcademicYear = academieJaar
            };

            var newId = _studentFacade.InsertFile(dossier);

            return this.RedirectToAction(c => c.File(newId));
        }

        [Route("SaveAanvraag")]
        [HttpPost]
        public ActionResult SaveAanvraag(RequestViewModel viewModel)
        {
            if(viewModel.Evidence == null) return Content("Geen bewijzen!");
            //viewModel.Evidence = viewModel.Evidence.Distinct().ToArray();
            //viewModel.Evidence.Select(bewijsId => _studentFacade.GetEvidenceById(bewijsId)).ToList();

            var request = new Request
            {
                FileId = viewModel.FileId,
                Argumentation = viewModel.Argumentation,
                LastChanged = DateTime.Now,
                Evidence = viewModel.Evidence.Select(evidenceId => new Evidence
                {
                    Id = evidenceId
                }).AsQueryable()
            };

            return Content(!_studentFacade.SyncRequestInFile(request) ? "Don't cheat!" : "Saved!");

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
        public ActionResult DeleteAanvraag(int dossierId, int aanvraagId)
        {
            return _studentFacade.IsRequestFromStudent(dossierId, aanvraagId, User.Identity.Name)
                ? Content("Don't cheat!")
                : Content(!_studentFacade.DeleteRequest(dossierId, aanvraagId)
                    ? "RequestPartimInformation bestaat niet!"
                    : "Voltooid!");
        }

        #endregion
    }
}