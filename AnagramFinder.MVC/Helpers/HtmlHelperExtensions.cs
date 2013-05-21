using System.Web.Mvc;
using System.Web.Routing;

namespace AnagramFinder.MVC.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, string value, object htmlAttributes = null)
        {
            var builder = new TagBuilder("input");
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.MergeAttribute("type", "submit");
            builder.MergeAttribute("value", value);
            var html = builder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString File(this HtmlHelper helper, string name, object htmlAttributes = null)
        {
            var builder = new TagBuilder("input");
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.MergeAttribute("type", "file");
            builder.MergeAttribute("name", name);
            var html = builder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(html);
        }
    }
}