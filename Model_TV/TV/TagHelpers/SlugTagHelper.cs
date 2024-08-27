using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.RegularExpressions;

namespace TV.TagHelpers
{
    [HtmlTargetElement("url_slug")]
    public class SlugTagHelper : AnchorTagHelper
    {
        public SlugTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        [HtmlAttributeName("for-TV-Id")]
        public Guid Id { get; set; }

        [HtmlAttributeName("for-TV-Title")]
        public string Title { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var slug = Regex.Replace(Title, "[^a-zA-Z0-9]+", "-").Trim('-').ToLower();

            RouteValues["slug"] = slug;
            RouteValues["slug_Id"] = Id.ToString();

            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;

            base.Process(context, output);
        }
    }
}
