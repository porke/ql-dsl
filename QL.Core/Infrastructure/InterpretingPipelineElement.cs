using Infrastructure;
using QL.Api.Infrastructure;
using QL.Core.Interpreting;
using static QL.Core.Interpreting.Value;

namespace QL.Core.Infrastructure
{
    internal sealed class InterpretingPipelineElement : IPipelineElement<InterpretingTask>
    {
        private bool _hasSucceeded = true;

        public bool CanContinue => _hasSucceeded;

        public InterpretingTask Process(InterpretingTask input)
        {
            var visitor = new InterpreterVisitor(new ValueFactory());

            input.InterpretedAst = visitor.EvaluateAst(input.InitialAst, input.Memory, input.Symbols);
            return input;
        }
    }
}
