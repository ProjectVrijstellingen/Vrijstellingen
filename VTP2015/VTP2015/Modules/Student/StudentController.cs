using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Web.Mvc;
using VTP2015.Config;
using VTP2015.Modules.Student.ViewModels;
using VTP2015.ServiceLayer.Student;
using VTP2015.ServiceLayer.Student.Models;

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
                Path = pic,
                Description = viewModel.Description
            };

            _studentFacade.InsertEvidence(dbBewijs, User.Identity.Name);
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
                .ProjectTo<FileListViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("EvidenceListWidget")]
        [HttpGet]
        public PartialViewResult EvidenceListWidget()
        {
            var models = _studentFacade.GetEvidenceByStudentEmail(User.Identity.Name)
                .ProjectTo<EvidenceListViewModel>();

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
        [Route("File/{fileId}")]
        [HttpGet]
        public ActionResult File(int fileId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, fileId))
                return RedirectToAction("Index");

            return View();
        }

        [Route("RequestedPartimsWidget")]
        [HttpGet]
        public ActionResult RequestedPartimsWidget(int fileId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, fileId))
                return RedirectToAction("Index");

            var models = _studentFacade.GetPartims(fileId, PartimMode.Requested)
                .ProjectTo<PartimViewModel>();
            
            return PartialView(models.ToArray());
        }

        [Route("AvailablePartimsWidget")]
        [HttpGet]
        public ActionResult AvailablePartimsWidget(int fileId)
        {
            if (!_studentFacade.IsFileFromStudent(User.Identity.Name, fileId))
                return RedirectToAction("Index");

            var models = _studentFacade.GetPartims(fileId, PartimMode.Available)
                .ProjectTo<PartimViewModel>();

            return PartialView(models.ToArray());
        }

        [Route("SelectEvidenceWidget")]
        [HttpGet]
        public PartialViewResult SelectEvidenceWidget(int fileId)
        {
            var models = _studentFacade.GetEvidenceByStudentEmail(User.Identity.Name)
                .ProjectTo<EvidenceListViewModel>();

            ViewBag.FileStatus = _studentFacade.GetFileStatus(fileId);

            return PartialView(models.ToArray());
        }

        [Route("RequestDetailWidget")]
        [HttpGet]
        public PartialViewResult RequestDetailWidget(int fileId)
        {
            var models = _studentFacade.GetRequestByFileId(fileId)
                .ProjectTo<RequestDetailViewModel>();

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
            var dossier = new ServiceLayer.Student.Models.File
            {
                StudentMail = User.Identity.Name,
                DateCreated = DateTime.Now,
                Education = education.Name,
                AcademicYear = academieJaar
            };

            var newId = _studentFacade.InsertFile(dossier);

            return this.RedirectToAction(c => c.File(newId));
        }

        [Route("AddAanvraag")]
        [HttpPost]
        public ActionResult AddAanvraag(AddRequestViewModel viewModel)
        {
            if (_studentFacade.IsFileFromStudent(User.Identity.Name, viewModel.FileId)) Content("Don't cheat!");
            return Content(_studentFacade.AddRequestInFile(viewModel.FileId,viewModel.Code));
        }

        [Route("SaveAanvraag")]
        [HttpPost]
        public ActionResult SaveAanvraag(RequestViewModel viewModel)
        {
            var requestId = int.Parse(viewModel.RequestId);
            if (_studentFacade.IsRequestFromStudent(viewModel.FileId, requestId, User.Identity.Name)) Content("Don't cheat!");

            var request = new Request
            {
                Id = requestId,
                FileId = viewModel.FileId,
                Argumentation = viewModel.Argumentation
            }; ;
            if (viewModel.Evidence != null)
            {
                viewModel.Evidence = viewModel.Evidence.Distinct().ToArray();
                request.Evidence = viewModel.Evidence.Select(evidenceId => new Evidence
                {
                    Id = evidenceId
                    }).AsQueryable();
            }


            return Content(!_studentFacade.SyncRequestInFile(request) ? "Don't cheat!" : "Saved!");
        }

        [Route("Delete")]
        [HttpPost]
        public ActionResult DeleteAanvraag(int fileId, string requestId)
        {
            var aanvraagId = int.Parse(requestId);
            return !_studentFacade.IsRequestFromStudent(fileId, aanvraagId, User.Identity.Name)
                ? Content("Don't cheat!")
                : Content(!_studentFacade.DeleteRequest(fileId, aanvraagId)
                    ? "RequestPartimInformation bestaat niet!"
                    : "Voltooid!");
        }

        [Route("Submit")]
        [HttpPost]
        public ActionResult SubmitFile(int fileId)
        {
            _studentFacade.IsFileFromStudent(User.Identity.Name, fileId);
            _studentFacade.SumbitFile(fileId);
            return Content("Submitted!");
        }
        #endregion
    }
}