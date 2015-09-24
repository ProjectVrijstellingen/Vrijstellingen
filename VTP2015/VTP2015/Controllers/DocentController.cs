using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Ajax.Utilities;
using Ninject.Infrastructure.Language;
using VTP2015.Repositories;
using VTP2015.Repositories.Interfaces;
using VTP2015.ViewModels.Docent;

namespace VTP2015.Controllers
{
    [Authorize(Roles = "Docent")]
    [RoutePrefix("Docent")]
    public class DocentController : Controller
    {
        private readonly IAanvraagRepository _aanvraagRepository;
        public DocentController(IAanvraagRepository aanvraagRepository)
        {
            _aanvraagRepository = aanvraagRepository;
        }

        //
        // GET: /Docent/
        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {
            ViewBag.DocentHeeftAanvragen = _aanvraagRepository.GetOnbehandeldeAanvragen(User.Identity.Name).Any();
            return View();
        }

        [HttpGet]
        [Route("StudentListWidget")]
        public PartialViewResult StudentListWidget()
        {
            var viewModel = _aanvraagRepository.GetOnbehandeldeAanvragenDistinct(User.Identity.Name)
                .Project().To<StudentListViewModel>();

            return PartialView(viewModel);
        }

        [HttpGet]
        [Route("AanvraagListWidget")]
        public PartialViewResult AanvraagListWidget()
        {
            var viewModel = _aanvraagRepository.GetOnbehandeldeAanvragen(User.Identity.Name)
                .Project().To<AanvraagListViewModel>();

            return PartialView(viewModel);
        }

        [Route("ApproveAanvraag")]
        [HttpPost]
        public ActionResult ApproveAanvraag(int aanvraagId)
        {
            return Content(_aanvraagRepository.Beoordeel(aanvraagId, true, User.Identity.Name) 
                ? "Voltooid!" 
                : "De aanvraag mag niet beoordeeld worden door u!");
        }

        [Route("DissapproveAanvraag")]
        [HttpPost]
        public ActionResult DissapproveAanvraag(int aanvraagId)
        {
            return Content(_aanvraagRepository.Beoordeel(aanvraagId, false, User.Identity.Name) 
                ? "Voltooid!" 
                : "De aanvraag mag niet beoordeeld worden door u!");
        }
    }
}