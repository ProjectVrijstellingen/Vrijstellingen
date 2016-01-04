using System.Web.Mvc;

namespace VTP2015.Helpers
{
    public static class PanelHelpers
    {
        public static Panel BeginPanel(this HtmlHelper html, string title)
        {
            var htmlText = "<div class=\"panel panel-default clearfix\">";
            htmlText += "<div class=\"panel-heading\">";
            htmlText += "<h3 class=\"panel-title\">" + title + "</h3></div>";
            htmlText += "<div class=\"panel-body\">";

            html.ViewContext.Writer.Write(htmlText);
            return new Panel(html);
        }

        public static void EndPanel(this HtmlHelper html)
        {
            html.ViewContext.Writer.Write("</div></div>");
        }
    }
}