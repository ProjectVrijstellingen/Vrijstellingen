﻿namespace VTP2015.ServiceLayer.Lecturer.Models
{
    public class Request
    {
        public int Id { get; set; }
        public Module Module { get; set; }
        public Partim Partim { get; set; }
        public string Argumentation { get; set; }
        public File File { get; set; }
    }
}
