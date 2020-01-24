using System;
using IronWebScraper;

namespace CityWebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new CityScraper();
            scraper.Start();
        }
    }

    class CityScraper : WebScraper
    {
        public override void Init()
        {
            License.LicenseKey = "LicenseKey";
            this.LoggingLevel = WebScraper.LogLevel.All;
            this.Request("https://www.cityofglasgowcollege.ac.uk/course-search?sort_by=field_template_reference_field_qualification&sort_order=ASC&f%5B0%5D=field_template_reference%253Afield_mode_of_study%3A139&page=", Parse);
        }
        public override void Parse(Response response)
        {
            foreach (var title_link in response.Css("a.panel-course-results__course"))
            {
                string strTitle = title_link.TextContentClean;
                Scrape(new ScrapedData() { { "Title", strTitle } });
            }
            if (response.CssExists("li.pager-next > a[href]"))
            {
                var next_page = response.Css("li.pager-next > a[href]")[0].Attributes["href"];
                this.Request(next_page, Parse);
            }
        }   
    }
}
