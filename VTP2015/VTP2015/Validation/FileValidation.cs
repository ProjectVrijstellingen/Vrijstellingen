using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTP2015.Validation
{
    public class FileValidation : RequiredAttribute
    {
        private HttpPostedFileBase _file;
        private const int MaxFileSize = 1024*1024;
        private readonly List<string> _extensions;

        public FileValidation()
        {
            _extensions = new List<string>{ ".gif", ".png", ".jpeg", ".jpg", ".pdf" };
        }
        public override bool IsValid(object value)
        {
            var isValid = false;
            _file = value as HttpPostedFileBase;

            if (_file == null || _file.ContentLength > MaxFileSize)
            {
                return isValid;
            }

            if (IsFileTypeValid(_file))
            {
                isValid = true;
            }

            return isValid;
        }

        private bool IsFileTypeValid(HttpPostedFileBase file)
        {
            var fileName = file.FileName.ToLower();
            var isValidExtension = _extensions.Any(y => fileName.EndsWith(y));
            return isValidExtension;
        }

        public override string FormatErrorMessage(string name)
        {
            if (_file == null)
            {
                return "Gelieve een file te selecteren!";
            }
            if (_file.ContentLength > MaxFileSize)
            {
                return "De file mag niet groter zijn dan 1MB!";
            }
            if (!IsFileTypeValid(_file))
            {
                return "File type behoort niet tot: " + GetExtensions();
            }
            return !IsFilenameValid(_file) ? "Filename can not be longer then 255 char." : "ERROR!";
        }

        private static bool IsFilenameValid(HttpPostedFileBase file)
        {
 	        return file.FileName.Length <= 255;
        }

        private string GetExtensions()
        {
            return _extensions.Aggregate("", (current, ext) => current + (ext + "|"));
        }
    }
}