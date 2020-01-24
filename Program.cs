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
            
        }
    }
}
