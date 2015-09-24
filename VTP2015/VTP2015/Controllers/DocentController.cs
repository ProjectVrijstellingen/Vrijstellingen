using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using VTP2015.ServiceLayer;
using VTP2015.ViewModels.Docent;

namespace VTP2015.Controllers
{
    [Authorize(Roles = "Lecturer")]
    [RoutePrefix("Lecturer")]
    public class DocentController : Controller
    {
        private readonly ILecturerFacade _lecturerFacade;

        public DocentController(ILecturerFacade lecturerFacade)
        {
            _lecturerFacade = lecturerFacade;
        }

        //
        // GET: /Lecturer/
        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {
            ViewBag.DocentHeeftAanvragen = _lecturerFacade.GetUntreadedRequests(User.Identity.Name).Any();
            return View();
        }

        [HttpGet]
        [Route("StudentListWidget")]
        public PartialViewResult StudentListWidget()
        {
            var viewModel = _lecturerFacade.GetUntreadedRequestsDistinct(User.Identity.Name)
                .Project().To<StudentListViewModel>();

            return PartialView(viewModel);
        }

        [HttpGet]
        [Route("AanvraagListWidget")]
        public PartialViewResult AanvraagListWidget()
        {
            var viewModel = _lecturerFacade.GetUntreadedRequests(User.Identity.Name)
                .Project().To<AanvraagListViewModel>();

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