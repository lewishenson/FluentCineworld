using FluentCineworld.Listings;
using FluentCineworld.Utilities;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FluentCineworld.Details
{
    [Obsolete]
    public class SyndicationDetailsScraper : IScraper<CinemaDetails>
    {
        private readonly IWebClient _webClient;

        public SyndicationDetailsScraper(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public CinemaDetails Scrape(Cinema cinema)
        {
            var uri = UriGenerator.CinemaNames();
            var content = _webClient.GetContent(uri);
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            var details = Parse(content, cinema.Value);

            return details;
        }

        private CinemaDetails Parse(string content, int cinemaId)
        {
            var relatedDataElement = XElement.Parse(content);
            var cinemaPath = string.Format("/row[@key={0}]", cinemaId);
            var cinemaElement = relatedDataElement.XPathSelectElement(cinemaPath);

            if (cinemaElement == null)
            {
                return null;
            }

            return new CinemaDetails
                       {
                           Address = cinemaElement.Elements("column").Single(e => e.Attribute("name").Value == "address").Value,
                           Id = Convert.ToInt32(cinemaElement.Elements("column").Single(e => e.Attribute("name").Value == "CinemaID").Value),
                           Name = cinemaElement.Elements("column").Single(e => e.Attribute("name").Value == "CinemaName").Value,
                           Phone = cinemaElement.Elements("column").Single(e => e.Attribute("name").Value == "phone").Value,
                           PostCode = cinemaElement.Elements("column").Single(e => e.Attribute("name").Value == "postcode").Value,
                           Url = cinemaElement.Elements("column").Single(e => e.Attribute("name").Value == "root").Value
                                 + cinemaElement.Elements("column").Single(e => e.Attribute("name").Value == "url").Value
                       };
        }
    }
}