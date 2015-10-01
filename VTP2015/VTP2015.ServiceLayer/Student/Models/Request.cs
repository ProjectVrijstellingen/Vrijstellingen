using System;
using System.Collections.Generic;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Student.Models
{
    public class Request
    {
        public string Argumentation { get; set; }
        public DateTime LastChanged { get; set; }
        public Status Status { get; set; }
        public int PartimInformationId { get; set; }
        public int FileId { get; set; }
        public int DossierId { get; set; }
        public string PartimInformationSuperCode { get; set; }
        public IEnumerable<int> Bewijzen { get; set; }
    }
}
