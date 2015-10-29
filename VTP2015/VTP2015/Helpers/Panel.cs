using System;
using System.Web.Mvc;

namespace VTP2015.Helpers
{
    public class Panel: IDisposable
    {
        private readonly HtmlHelper _html;

        public Panel(HtmlHelper html)
        {
            _html = html;
        }
        public void Dispose()
        {
            _html.EndPanel();
        }
    }
}