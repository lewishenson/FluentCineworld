using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentCineworld.Sites
{
    public class SiteMapper : ISiteMapper
    {
        public IEnumerable<SiteDetails> Map(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                yield break;
            }

            var siteDtos = JsonConvert.DeserializeObject<List<SiteDto>>(json);

            foreach (var siteDto in siteDtos)
            {
                yield return Map(siteDto);
            }
        }

        private static SiteDetails Map(SiteDto siteDto)
        {
            return new SiteDetails
                {
                    Address = siteDto.Address,
                    Id = siteDto.Id,
                    Latitude = siteDto.Latitude,
                    Longitude = siteDto.Longitude,
                    Name = siteDto.Name,
                    PhoneNumber = siteDto.PhoneNumber,
                    Url = siteDto.Url
                };
        }
    }
}