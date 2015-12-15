using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using VTP2015.Modules.Lecturer.ViewModels;
using VTP2015.ServiceLayer.Lecturer;

namespace VTP2015.Modules.Lecturer
{
    [Authorize(Roles = "Lecturer")]
    [RoutePrefix("Lecturer")]
    public class LecturerController : Controller
    {
        private readonly ILecturerFacade _lecturerFacade;

        public LecturerController(ILecturerFacade lecturerFacade)
        {
            _lecturerFacade = lecturerFacade;
        }

        //
        // GET: /Lecturer/
        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {  
            return View();
        }

        [HttpGet]
        [Route("OpenRequests")]
        public ViewResult OpenRequests()
        {
            ViewBag.DocentHeeftAanvragen = _lecturerFacade.hasAny(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Untreated);

            return View();
        }

        [HttpGet]
        [Route("Partim")]
        public ViewResult Partim()
        {
            return View();
        }

        [HttpGet]
        [Route("StudentListWidget")]
        public PartialViewResult StudentListWidget()
        {
            var viewModel = _lecturerFacade.GetUntreadedStudent(User.Identity.Name)
                .ProjectTo<StudentListViewModel>();

            return PartialView(viewModel);
        }

        [HttpGet]
        [Route("ModuleListWidget")]
        public PartialViewResult ModuleListWidget()
        {
            var viewModel = _lecturerFacade.GetPartims(User.Identity.Name)
                .ProjectTo<PartimListViewModel>();

            return PartialView(viewModel);
        }


        [HttpGet]
        [Route("RequestListWidget")]
        public PartialViewResult RequestListWidget()
        {
            var viewModel = _lecturerFacade.GetRequests(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Untreated)
                .ProjectTo<RequestListViewModel>().OrderBy(p => p.SuperCode);

            return PartialView(viewModel);
        }

        [HttpGet]
        [Route("Archive")]
        public ViewResult Archive()
        {
            ViewBag.DocentHeeftApprovedAanvragen = _lecturerFacade.hasAny
                (User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Approved);

            ViewBag.DocentHeeftRejectedAanvragen = _lecturerFacade.hasAny
                (User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Rejected);
            
            return View();
        }

        [HttpGet]
        [Route("ApprovedRequestListWidget")]
        public PartialViewResult ApprovedRequestListWidget()
        {

            var viewModel = _lecturerFacade.GetRequests(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Approved)
                .ProjectTo<RequestListViewModel>();

            return PartialView(viewModel);
        }

        [HttpGet]
        [Route("RejectedRequestListWidget")]
        public PartialViewResult RejectedRequestListWidget()
        {
            var viewModel = _lecturerFacade.GetRequests(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Rejected)
                .ProjectTo<RequestListViewModel>();

            return PartialView(viewModel);
        }

        [Route("ApproveAanvraag")]
        [HttpPost]
        public ActionResult ApproveAanvraag(int aanvraagId, int motivationId)
        {
            return Content(_lecturerFacade.Approve(aanvraagId, true, User.Identity.Name, motivationId) 
                ? "Voltooid!" 
                : "De aanvraag mag niet beoordeeld worden door u!");
        }

        [Route("DissapproveAanvraag")]
        [HttpPost]
        public ActionResult DissapproveAanvraag(int aanvraagId, int motivationId)
        {
            return Content(_lecturerFacade.Approve(aanvraagId, false, User.Identity.Name, motivationId) 
                ? "Voltooid!" 
                : "De aanvraag mag niet beoordeeld worden door u!");
        }

        [Route("RemovePartimLecturer")]
        [HttpPost]
        public ActionResult RemovePartimLecturer(string supercodeId)
        {
            return Content(_lecturerFacade.RemovePartimLecturer(supercodeId)?"Voltooid!":"Error");
        }

        [HttpGet]
        [Route("MotivationListWidget")]
        public PartialViewResult MotivationListWidget()
        {

            var viewModel = _lecturerFacade.GetMotivations()
                .ProjectTo<MotivationListViewModel>();

            return PartialView(viewModel);
        }
    }
}