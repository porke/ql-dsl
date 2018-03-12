namespace QLS.Api.Infrastructure
{
    public interface IPipelineElement<T>
    {
        T Process(T input);

        bool CanContinue { get; }
    }
}
