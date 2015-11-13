﻿using System.Linq;
using System.Web.Mvc;
using VTP2015.lib;
using VTP2015.Modules.Student.ViewModels;
using VTP2015.ServiceLayer.Student.Models;

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
                var count = module.Partims.Count;
                var submitted = module.Partims.All(x => x.Status != 0);
                var tag = new TagBuilder("div");
                tag.Attributes.Add("data-moduleid",module.Code);

                var moduleNameTag = new TagBuilder("span");
                moduleNameTag.AddCssClass("name h4" + (count == module.TotalCount && !submitted ? " module" : ""));
                moduleNameTag.SetInnerText(module.Name);
                tag.InnerHtml += moduleNameTag;
                if (!submitted) tag.InnerHtml += ShowGlyphicon(html, "remove", "btn badge" + (count == module.TotalCount && deletable ? "" : " hide"));

                var moduleTag = new TagBuilder("ul");
                moduleTag.AddCssClass("list-group" + (count == module.TotalCount && deletable ? " hide" : ""));
                foreach (var partim in module.Partims)
                {
                    var partimTag = new TagBuilder("li");
                    partimTag.Attributes.Add("data-SuperCode", partim.SuperCode);
                    partimTag.AddCssClass("list-group-item" +  (partim.Status == 0 ? " partim" : ""));
                    var partimNameTag = new TagBuilder("span");
                    if (TextLimiter(partim.Name, 30).EndsWith("..."))
                    {
                        partimNameTag.MergeAttribute("data-toggle", "tooltip");
                        partimNameTag.MergeAttribute("title", partim.Name);

                    }
                    partimNameTag.AddCssClass("name");
                    partimNameTag.SetInnerText(TextLimiter(partim.Name, 30));
                    partimTag.InnerHtml += partimNameTag;
                    if (partim.Status == 0) partimTag.InnerHtml += ShowGlyphicon(html, "remove","btn badge" + (deletable ? "" : " hide"));
                    moduleTag.InnerHtml += partimTag;
                }
                tag.InnerHtml += moduleTag;
                htmlString += tag.ToString();
            }
            return new MvcHtmlString(htmlString);
        }

        public static MvcHtmlString ShowBewijzenList(this HtmlHelper html, EvidenceListViewModel[] bewijzen, bool moveable)
        {
            var tag = new TagBuilder("ul");
            tag.AddCssClass("list-group");
            tag.Attributes.Add("id", moveable ? "draggableList" : "bewijzenList");
            foreach (var bewijs in bewijzen)
            {
                tag.InnerHtml += ShowBewijsLi(html, bewijs,moveable);
            }
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString ShowBewijsLi(this HtmlHelper html, EvidenceListViewModel evidence, bool movable)
        {
            return ShowBewijsLi(html,evidence,movable,false);
        }

        public static MvcHtmlString ShowBewijsLi(this HtmlHelper html, EvidenceListViewModel evidence, bool movable, bool submitted)
        {
            var itemTag = new TagBuilder("li");
            itemTag.AddCssClass("list-group-item");
            itemTag.Attributes.Add("data-bewijsid", evidence.Id.ToString());
            if (movable) itemTag.Attributes.Add("id", "evidence-" + evidence.Id);
            itemTag.InnerHtml += ShowGlyphicon(html, "file");
            var descriptionTag = new TagBuilder("span");
            descriptionTag.AddCssClass("glyphicon-class");
            if (TextLimiter(evidence.Path, 20).EndsWith("..."))
            {
                descriptionTag.MergeAttribute("data-toggle", "tooltip");
                descriptionTag.MergeAttribute("title", evidence.Path);
            }
            descriptionTag.SetInnerText(TextLimiter(evidence.Path,20) + " - " + evidence.Description);
            itemTag.InnerHtml += descriptionTag;
            if (!submitted) itemTag.InnerHtml += ShowGlyphicon(html, "minus","btn badge" + (movable? " hide":""));
            if (movable) itemTag.InnerHtml += ShowGlyphicon(html, "plus","btn badge");
            return new MvcHtmlString(itemTag.ToString());
        }

        public static MvcHtmlString ShowAanvraagDetails(this HtmlHelper html, RequestDetailViewModel[] aanvragen)
        {
            var htmlString = "";
            foreach (var aanvraag in aanvragen)
            {
                var articleTag = new TagBuilder("article");
                articleTag.Attributes.Add("data-code",aanvraag.Code);
                articleTag.Attributes.Add("data-requestId",aanvraag.Id.ToString());
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
                argumentatieLabelTag.SetInnerText("Argumentatie:");
                articleTag.InnerHtml += argumentatieLabelTag;
                var argumentatieTag = new TagBuilder("textarea");
                if(!aanvraag.Submitted) argumentatieTag.Attributes.Add("id","argumentatie");
                argumentatieTag.AddCssClass("form-control");
                argumentatieTag.SetInnerText(aanvraag.Argumentation);
                articleTag.InnerHtml += argumentatieTag;
                var bewijzenLabelTag = new TagBuilder("label");
                bewijzenLabelTag.AddCssClass("control-label");
                bewijzenLabelTag.SetInnerText("Bewijzen:");
                articleTag.InnerHtml += bewijzenLabelTag;
                var bewijzenTag = new TagBuilder("ul");
                if(!aanvraag.Submitted) bewijzenTag.Attributes.Add("id","bewijzen");
                bewijzenTag.AddCssClass("list-group");
                foreach (var bewijs in aanvraag.Evidence)
                {
                    bewijzenTag.InnerHtml += ShowBewijsLi(html, bewijs, false, aanvraag.Submitted);
                }
                articleTag.InnerHtml += bewijzenTag;
                var buttonTag = new TagBuilder("button");
                buttonTag.AddCssClass("btn btn-success");
                buttonTag.Attributes.Add("onclick", "Return()");
                buttonTag.Attributes.Add("type","button");
                buttonTag.SetInnerText("Terug");
                articleTag.InnerHtml += buttonTag;
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