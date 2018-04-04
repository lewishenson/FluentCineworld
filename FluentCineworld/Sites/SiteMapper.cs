using System;
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

            var response = JsonConvert.DeserializeObject<ResponseDto>(json);

            foreach (var siteDto in response.Body.Cinemas)
            {
                yield return Map(siteDto);
            }
        }

        private static SiteDetails Map(SiteDto siteDto)
        {
            return new SiteDetails
                {
                    Address = siteDto.Address,
                    DisplayName = siteDto.DisplayName,
                    Id = Convert.ToInt32(siteDto.Id),
                    Link = siteDto.Link
                };
        }
    }
}