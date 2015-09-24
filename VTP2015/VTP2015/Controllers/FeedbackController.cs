using System.Web.Mvc;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;
using VTP2015.ViewModels.Feedback;

namespace VTP2015.Controllers
{
    [RoutePrefix("Feedback")]
    public class FeedbackController : Controller
    {

        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IStudentRepository _studentRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository, IStudentRepository studentRepository)
        {
            _feedbackRepository = feedbackRepository;
            _studentRepository = studentRepository;
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
                Student = _studentRepository.GetByEmail(User.Identity.Name),
                Text = viewModel.Text
            };

            _feedbackRepository.AddFeedback(feedback);

            return Content("Uw feedback werd goed ontvangen");
        }
    }
}
