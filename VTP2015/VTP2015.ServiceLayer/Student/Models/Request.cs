﻿using System;
using System.Linq;

namespace VTP2015.ServiceLayer.Student.Models
{
    public class Request
    {
        public int FileId { get; set; }
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string PartimName { get; set; }
        public string Code { get; set; }
        public string Argumentation { get; set; }
        public bool Submitted { get; set; }
        public IQueryable<Evidence> Evidence { get; set; }
    }
}
