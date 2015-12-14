
namespace VTP2015.ServiceLayer.Lecturer.Models
{
    public class Evidence
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
