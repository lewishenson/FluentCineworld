using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings.GetDates;
using FluentCineworld.Utilities;
using Moq;
using Xunit;

namespace FluentCineworld.UnitTests.Listings.GetDates
{
    [Trait("Category", "UnitTest")]
    public class GetDatesQueryTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GivenThereIsNoData_WhenExecuted_ThenAnEmptyCollectionIsReturned(string json)
        {
            var httpClient = Mock.Of<IHttpClient>();

            Mock.Get(httpClient)
                .Setup(client => client.GetContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(json));

            var query = new GetDatesQuery(httpClient);

            var films = await query.ExecuteAsync(Cinema.Northampton);

            films.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenThereIsData_WhenExecuted_ThenTheCorrectValuesAreReturned()
        {
            var httpClient = Mock.Of<IHttpClient>();

            var json = File.ReadAllText(@"Data/dates.json");

            Mock.Get(httpClient)
                .Setup(client => client.GetContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(json));

            var query = new GetDatesQuery(httpClient);

            var acutalDates = await query.ExecuteAsync(Cinema.Northampton);

            var expectedDates = new[] { new DateTime(2018, 4, 5), new DateTime(2018, 4, 6), new DateTime(2018, 4, 7) };

            acutalDates.Should().BeEquivalentTo(expectedDates);
        }
    }
}