using System.Web.Mvc;
using VTP2015.DataAccess.ServiceRepositories;
using VTP2015.Modules.Feedback.ViewModels;
using VTP2015.ServiceLayer.Feedback;

namespace VTP2015.Modules.Feedback
{
    [RoutePrefix("Feedback")]
    public class FeedbackController : Controller
    {

        private readonly IFeedbackFacade _feedbackFacade;
        private readonly IIdentityRepository _identityRepository;

        public FeedbackController(IFeedbackFacade feedbackFacade, IIdentityRepository identityRepository)
        {
            _feedbackFacade = feedbackFacade;
            _identityRepository = identityRepository;
        }
        // GET: /Feedback
        [Route("")]
        public PartialViewResult Index()
        {
            var model = new IndexViewModel
            {
                User = _identityRepository.GetUserByEmail("joachim.bockland")
            };
            return PartialView(model);
        }


        //
        // GET: /Feedback/Create
        [Route("AddFeedbackWidget")]
        public PartialViewResult AddFeedbackWidget()
        {
            return PartialView();
        }

        //
        // POST: /Feedback/Create
        [Route("AddFeedback")]
        [HttpPost]
        public ContentResult AddFeedback(AddFeedbackViewModel viewModel)
        {
            var feedback = new ServiceLayer.Feedback.Models.Feedback
            {
                StudentEmail = User.Identity.Name,
                Text = viewModel.Text
            };

            _feedbackFacade.InsertFeedback(feedback);

            return Content("Uw feedback werd goed ontvangen");
        }
    }
}
