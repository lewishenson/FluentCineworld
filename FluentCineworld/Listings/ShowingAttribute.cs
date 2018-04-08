namespace FluentCineworld.Listings
{
    public class ShowingAttribute
    {
        public ShowingAttribute(string id, string text, ShowingAttributeType attributeType)
        {
            Id = id;
            Text = text;
            AttributeType = attributeType;
        }

        public string Id { get; }

        public string Text { get; }

        public ShowingAttributeType AttributeType { get; }
    }
}