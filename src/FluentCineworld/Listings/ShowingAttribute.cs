namespace FluentCineworld.Listings
{
    public class ShowingAttribute(string id, string text, ShowingAttributeType attributeType)
    {
        public string Id { get; } = id;

        public string Text { get; } = text;

        public ShowingAttributeType AttributeType { get; } = attributeType;
    }
}
