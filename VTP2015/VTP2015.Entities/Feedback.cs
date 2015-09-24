namespace VTP2015.Entities
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string StudentId { get; set; }
        public string Text { get; set; }

        public Student Student { get; set; }
    }
}
