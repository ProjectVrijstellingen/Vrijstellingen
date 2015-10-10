using System.Web.Mvc;
using VTP2015.lib;
using VTP2015.Modules.Student.ViewModels;

namespace VTP2015.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString ShowPartimList(this HtmlHelper html, PartimViewModel[] viewModels, bool deletable)
        {
            var htmlString = "";
            var moduleFactory = new ModuleFactory(viewModels);
            var modules = moduleFactory.Modules;

            foreach (var module in modules)
            {
                var tag = new TagBuilder("div");
                tag.Attributes.Add("data-moduleid",module.ModuleId.ToString());

                var moduleNameTag = new TagBuilder("h4");
                moduleNameTag.SetInnerText(module.Naam);
                tag.InnerHtml += moduleNameTag;

                var moduleTag = new TagBuilder("ul");
                moduleTag.AddCssClass("list-group");
                foreach (var partim in module.Partims)
                {
                    var partimTag = new TagBuilder("li");
                    partimTag.Attributes.Add("data-SuperCode", partim.SuperCode);
                    partimTag.AddCssClass("list-group-item partim");
                    var partimNameTag = new TagBuilder("span");
                    partimNameTag.AddCssClass("name");
                    partimNameTag.SetInnerText(TextLimiter(partim.Naam, 30));
                    partimTag.InnerHtml += partimNameTag;
                    partimTag.InnerHtml += ShowGlyphicon(html, "remove","btn badge" + (deletable ? "" : " hide"));
                    moduleTag.InnerHtml += partimTag;
                }
                tag.InnerHtml += moduleTag;
                htmlString += tag.ToString();
            }
            return new MvcHtmlString(htmlString);
        }

        public static MvcHtmlString ShowBewijzenList(this HtmlHelper html, EvidenceListViewModel[] bewijzen, bool draggable)
        {
            var tag = new TagBuilder("ul");
            tag.AddCssClass("list-group");
            tag.Attributes.Add("id", draggable ? "draggableList" : "bewijzenList");
            foreach (var bewijs in bewijzen)
            {
                tag.InnerHtml += ShowBewijsLi(html, bewijs,draggable);
            }
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString ShowBewijsLi(this HtmlHelper html, EvidenceListViewModel evidence, bool draggable)
        {
            var itemTag = new TagBuilder("li");
            itemTag.AddCssClass("list-group-item");
            itemTag.Attributes.Add("data-bewijsid", evidence.Id.ToString());
            if (draggable)
            {
                itemTag.Attributes.Add("id", "evidence-" + evidence.Id);
                itemTag.Attributes.Add("draggable", "true");
                itemTag.Attributes.Add("ondragstart", "drag(event)");
            }
            itemTag.InnerHtml += ShowGlyphicon(html, "file");
            var descriptionTag = new TagBuilder("span");
            descriptionTag.AddCssClass("glyphicon-class");
            descriptionTag.SetInnerText(TextLimiter(evidence.Path,20) + " - " + evidence.Omschrijving);
            itemTag.InnerHtml += descriptionTag;
            itemTag.InnerHtml += ShowGlyphicon(html, "minus","btn badge" + (draggable? " hide":""));
            if (draggable) itemTag.InnerHtml += ShowGlyphicon(html, "plus","btn badge");
            return new MvcHtmlString(itemTag.ToString());
        }

        public static MvcHtmlString ShowAanvraagDetails(this HtmlHelper html, RequestDetailViewModel[] aanvragen)
        {
            var htmlString = "";
            foreach (var aanvraag in aanvragen)
            {
                var articleTag = new TagBuilder("article");
                articleTag.Attributes.Add("id",aanvraag.SuperCode);
                articleTag.AddCssClass("hide");
                var moduleTag = new TagBuilder("h3");
                moduleTag.SetInnerText(aanvraag.ModuleName);
                articleTag.InnerHtml += moduleTag;
                var partimTag = new TagBuilder("h4");
                partimTag.SetInnerText(aanvraag.PartimName);
                articleTag.InnerHtml += partimTag;
                var argumentatieLabelTag = new TagBuilder("label");
                argumentatieLabelTag.Attributes.Add("for","argumentatie");
                argumentatieLabelTag.AddCssClass("control-label");
                argumentatieLabelTag.SetInnerText("Argumentation:");
                articleTag.InnerHtml += argumentatieLabelTag;
                var argumentatieTag = new TagBuilder("textarea");
                argumentatieTag.Attributes.Add("id","argumentatie");
                argumentatieTag.AddCssClass("form-control");
                argumentatieTag.SetInnerText(aanvraag.Argumentation);
                articleTag.InnerHtml += argumentatieTag;
                var bewijzenLabelTag = new TagBuilder("label");
                bewijzenLabelTag.AddCssClass("control-label");
                bewijzenLabelTag.SetInnerText("Evidence:");
                articleTag.InnerHtml += bewijzenLabelTag;
                var bewijzenTag = new TagBuilder("ul");
                bewijzenTag.AddCssClass("list-group");
                foreach (var bewijs in aanvraag.Evidence)
                {
                    bewijzenTag.InnerHtml += ShowBewijsLi(html, bewijs, false);
                }
                articleTag.InnerHtml += bewijzenTag;
                var buttonTag = new TagBuilder("button");
                buttonTag.AddCssClass("btn btn-success");
                buttonTag.Attributes.Add("onclick", "Return()");
                buttonTag.Attributes.Add("type","button");
                buttonTag.SetInnerText("Terug");
                articleTag.InnerHtml += buttonTag;
                var labelTag = new TagBuilder("label");
                labelTag.Attributes.Add("id", "status");
                labelTag.SetInnerText("Saved!");
                articleTag.InnerHtml += labelTag;
                htmlString += articleTag.ToString();
            }
            return new MvcHtmlString(htmlString);
        }

        public static MvcHtmlString ShowGlyphiconWithRole(this HtmlHelper html, string name, string role)
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("glyphicon glyphicon-" + name);
            tag.Attributes.Add("aria-hidden", "true");
            tag.Attributes.Add("data-role", role);
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString ShowGlyphicon(this HtmlHelper html, string name, string classes)
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("glyphicon glyphicon-" + name + " " + classes);
            tag.SetInnerText(" ");
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString ShowGlyphicon(this HtmlHelper html, string name)
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("glyphicon glyphicon-" + name);
            tag.Attributes.Add("aria-hidden", "true");
            return new MvcHtmlString(tag.ToString());
        }

        public static string TextLimiter(string text, int length)
        {
            
            if (text.Length <= length) return text;
            return text.Substring(0, length - 1) + "...";
        }
    }
}