namespace VTP2015.Entities
{
    public class Feedback : BaseEntity
    {
        public int? StudentId { get; set; }
        public string Text { get; set; }

        public Student Student { get; set; }
    }
}
