namespace VTP2015.Entities
{
    public class TrajectBegeleider
    {
        public int TrajectBegeleiderId { get; set; }
        public string Email { get; set; }

        public virtual Opleiding Opleiding { get; set; }
    }
}
