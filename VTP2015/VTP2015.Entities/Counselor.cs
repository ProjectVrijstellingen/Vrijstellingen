namespace VTP2015.Entities
{
    public class Counselor : BaseEntity
    {
        public string Email { get; set; }
        public int? EducationId { get; set; }

        public virtual Education Education { get; set; }
    }
}
