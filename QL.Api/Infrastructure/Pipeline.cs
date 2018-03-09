using System.Collections.Generic;

namespace QL.Api.Infrastructure
{
    public sealed class Pipeline<T>
    {
        private IList<IPipelineElement<T>> _steps = new List<IPipelineElement<T>>();

        public void ConnectElement(IPipelineElement<T> element)
        {
            _steps.Add(element);
        }

        public T Process(T input)
        {
            T intermediaryProduct = input;
            foreach (var step in _steps)
            {
                intermediaryProduct = step.Process(intermediaryProduct);
                if (!step.CanContinue)
                {
                    return intermediaryProduct;
                }
            }

            return intermediaryProduct;
        }
    }
}
