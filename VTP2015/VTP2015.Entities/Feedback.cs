namespace VTP2015.Entities
{
    public class Feedback : BaseEntity
    {
        public string StudentId { get; set; }
        public string Text { get; set; }

        public Student Student { get; set; }
    }
}
