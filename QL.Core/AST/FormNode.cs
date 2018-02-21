using Antlr4.Runtime;

namespace QL.Core.AST
{
    public class FormNode : Node
    {
        public FormNode(IToken token, string label) : base(token)
        {
            Label = label;
        }

        public string Label { get; }        
    }
}
