﻿using System.Collections.Generic;

namespace VTP2015.Modules.Student.ViewModels
{
    public class AddRequestViewModel
    {
        public int FileId { get; set; }
        public string Argumentation { get; set; }
        public IEnumerable<int> Evidence { get; set; }
    }
}   