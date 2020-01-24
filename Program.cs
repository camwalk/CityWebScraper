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
            this.LoggingLevel = WebScraper.LogLevel.All;
            this.Request("https://www.cityofglasgowcollege.ac.uk/course-search", Parse);
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
