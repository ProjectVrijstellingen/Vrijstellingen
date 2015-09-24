using System.Web.Mvc;
using VTP2015.Entities;
using VTP2015.ServiceLayer;
using VTP2015.ViewModels.Feedback;

namespace VTP2015.Controllers
{
    [RoutePrefix("Feedback")]
    public class FeedbackController : Controller
    {

        private readonly IFeedbackFacade _feedbackFacade;

        public FeedbackController(IFeedbackFacade feedbackFacade)
        {
            _feedbackFacade = feedbackFacade;
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
        public ActionResult AddFeedback(AddFeedbackViewModel viewModel)
        {

            var feedback = new Feedback
            {
                Student = _feedbackFacade.GetStudentByEmail(User.Identity.Name),
                Text = viewModel.Text
            };

            _feedbackFacade.InsertFeedback(feedback);

            return Content("Uw feedback werd goed ontvangen");
        }
    }
}
