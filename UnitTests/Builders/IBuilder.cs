namespace FluentCineworld.UnitTests.Builders
{
    internal interface IBuilder<out T>
    {
        T Build();
    }
}