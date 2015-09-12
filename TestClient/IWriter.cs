namespace FluentCineworld.TestClient
{
    public interface IWriter<in T>
    {
        void Output(T item);
    }
}