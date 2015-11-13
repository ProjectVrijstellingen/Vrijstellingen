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
            ViewBag.DocentHeeftAanvragen = _lecturerFacade.hasAny(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Untreated);
               // .GetRequests(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Untreated).Any().ToString();

            return View();
        }

        [HttpGet]
        [Route("StudentListWidget")]
        public PartialViewResult StudentListWidget()
        {
            var viewModel = _lecturerFacade.GetUntreadedRequestsDistinct(User.Identity.Name)
                .ProjectTo<StudentListViewModel>();

            return PartialView(viewModel);
        }

        [HttpGet]
        [Route("RequestListWidget")]
        public PartialViewResult RequestListWidget()
        {
            var viewModel = _lecturerFacade.GetRequests(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Untreated)
                .ProjectTo<RequestListViewModel>();

            return PartialView(viewModel);
        }

        [HttpGet]
        [Route("Archive")]
        public ViewResult Archive()
        {
            ViewBag.DocentHeeftApprovedAanvragen = _lecturerFacade
                .GetRequests(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Approved).Any();

            ViewBag.DocentHeeftRejectedAanvragen = _lecturerFacade
                .GetRequests(User.Identity.Name, ServiceLayer.Lecturer.Models.Status.Rejected).Any();



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
        public ActionResult ApproveAanvraag(int aanvraagId)
        {
            return Content(_lecturerFacade.Approve(aanvraagId, true, User.Identity.Name) 
                ? "Voltooid!" 
                : "De aanvraag mag niet beoordeeld worden door u!");
        }

        [Route("DissapproveAanvraag")]
        [HttpPost]
        public ActionResult DissapproveAanvraag(int aanvraagId)
        {
            return Content(_lecturerFacade.Approve(aanvraagId, false, User.Identity.Name) 
                ? "Voltooid!" 
                : "De aanvraag mag niet beoordeeld worden door u!");
        }
    }
}