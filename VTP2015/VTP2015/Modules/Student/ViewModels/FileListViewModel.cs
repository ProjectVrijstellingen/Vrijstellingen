using System;
using VTP2015.lib;

namespace VTP2015.Modules.Student.ViewModels
{
    public class FileListViewModel
    {
        public int Id { get; set; }
        public string Description => Education + FileName + DateCreated;
        public bool Editable { get; set; }
        public string Education { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}