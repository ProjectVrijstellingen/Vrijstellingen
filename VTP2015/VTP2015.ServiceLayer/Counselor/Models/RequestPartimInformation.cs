﻿using System.Linq;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class RequestPartimInformation
    {
        public int RequestId { get; set; }
        public int FileId { get; set; }
        public Status Status { get; set; }
        public string Argumentation { get; set; }
        public string PartimName { get; set; }
        public string ModuleName { get; set; }
        public IQueryable<Evidence> Evidence { get; set; }
    }
}