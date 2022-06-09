using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AcademyFWeek8.MVC.TagHelpers
{
    public class WebTagHelper : TagHelper
    {
        public string TextButton { get; set; }
        public string Site { get; set; } = "https://www.avanade.com/";
        public string LanguageSite { get; set; } = "it-it";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //output.TagName = "a";
            //output.Content.SetContent(TextButton);
            //output.Attributes.SetAttribute("class", "btn btn-danger");
            //output.Attributes.SetAttribute("rel", "alternate");
            //output.Attributes.SetAttribute("href", Site);
            //output.Attributes.SetAttribute("hreflang", LanguageSite);
            output.TagName = "a";
            output.Content.SetContent($"{ TextButton}");
            output.Attributes.SetAttribute("class", "btn btn-warning");
            output.Attributes.SetAttribute("href", $"{Site}{LanguageSite}");
        }
    }
}
