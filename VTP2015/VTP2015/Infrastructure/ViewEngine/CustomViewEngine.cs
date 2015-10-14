using System.Web.Mvc;

namespace VTP2015.Infrastructure.ViewEngine
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            var viewLocations = new[]
            {
                "~/Modules/{1}/Views/{0}.cshtml",
                "~/Modules/Shared/Views/{0}.cshtml",
            };

            PartialViewLocationFormats = viewLocations;
            ViewLocationFormats = viewLocations;
        }
    }
}