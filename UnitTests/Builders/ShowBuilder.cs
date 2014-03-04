using FluentCineworld.Listings;

namespace FluentCineworld.UnitTests.Builders
{
    internal class ShowBuilder : IBuilder<Show>
    {
        public ShowBuilder()
        {
        }

        public Show Build()
        {
            var show = new Show();

            return show;
        }
    }
}