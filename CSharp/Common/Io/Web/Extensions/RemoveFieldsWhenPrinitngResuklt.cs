using System.Runtime.CompilerServices;
using HtmlAgilityPack;
using Kevins.Examples.Common.Extensions;

namespace Kevins.Examples.Common.Io.Web.Extensions
{
    public static class RemoveFieldsWhenPrintingExtensions
    {
        public static readonly string FindHtmlTag = "//{0}";
        public static readonly string FindHtmlTagWithAttributeClause = FindHtmlTag + "[contains(@{1},'{2}')]";


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RemoveFieldsWhenPrintingResult RemoveSrisFieldsToHideWhenPrinting(this string htmlContent, bool removedFieldsToHideWhenPrinting = false)
        {
            var result = htmlContent.RemoveFields(
                string.Format(FindHtmlTagWithAttributeClause, Fields.Div.AsWords().ToLower(), "id", "sendACSControl"),
                removedFieldsToHideWhenPrinting
            );

            result = result.Html.RemoveFields(
                string.Format(FindHtmlTagWithAttributeClause, Fields.Div.AsWords().ToLower(), "id", "faxDiv"),
                result.RemovedFields
            );

            result = result.Html.RemoveFields(
                string.Format(FindHtmlTagWithAttributeClause, @"*", "class", "NoPrint"),
                result.RemovedFields
            );

            result = result.Html.ReplaceLogoProtType(
                string.Format(FindHtmlTagWithAttributeClause, @"img", "name", "LOGO"),
                result.RemovedFields
            );

            result = result.Html.RemoveFieldsMatchingTag(
                Fields.Select,
                result.RemovedFields
            );

            return result.Html.RemoveFieldsMatchingTag(
                Fields.Button,
                result.RemovedFields
            );
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RemoveFieldsWhenPrintingResult RemoveFieldsMatchingTag(this string htmlContent, Fields tag, bool removedFieldsToHideWhenPrinting = false)
        {
            return RemoveFieldsMatchingTag(htmlContent, tag.AsWords().ToLower(), removedFieldsToHideWhenPrinting);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RemoveFieldsWhenPrintingResult RemoveFieldsMatchingTag(this string htmlContent, string tagString, bool removedFieldsToHideWhenPrinting = false)
        {
            return RemoveFields(htmlContent, string.Format(FindHtmlTag, tagString), removedFieldsToHideWhenPrinting);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RemoveFieldsWhenPrintingResult RemoveFields(this string htmlContent, string fieldsSearchExpresion, bool removedFieldsToHideWhenPrinting = false)
        {
            var result = new RemoveFieldsWhenPrintingResult
            {
                Html = htmlContent,
                RemovedFields = removedFieldsToHideWhenPrinting
            };

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            var tags = htmlDocument.DocumentNode.SelectNodes(fieldsSearchExpresion);
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    result.RemovedFields = true;
                    tag.Remove();
                }
            }

            result.Html = htmlDocument.DocumentNode.OuterHtml;
            return result;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RemoveFieldsWhenPrintingResult ReplaceLogoProtType(this string htmlContent, string fieldsSearchExpresion, bool removedFieldsToHideWhenPrinting = false)
        {
            var result = new RemoveFieldsWhenPrintingResult
            {
                Html = htmlContent,
                RemovedFields = removedFieldsToHideWhenPrinting
            };

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            var tags = htmlDocument.DocumentNode.SelectNodes(fieldsSearchExpresion);
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    result.RemovedFields = true;
                    tag.SetAttributeValue("src", @"/interpimages/LOGO_009A27.jpg");
                }
            }

            result.Html = htmlDocument.DocumentNode.OuterHtml;
            return result;
        }
    }


    public class RemoveFieldsWhenPrintingResult
    {
        public virtual string Html { get; set; }

        public virtual bool RemovedFields { get; set; }
    }
}
