using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Module : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int Semester { get; set; }

    public ICollection<PartimInformation> PartimInformation { get; set; }
}
}
