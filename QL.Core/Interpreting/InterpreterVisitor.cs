using QL.Core.Ast;
using QL.Core.Types;
using System.Collections.Generic;

namespace QL.Core.Interpreting
{
    internal class InterpreterVisitor : BaseVisitor<Node>
    {       
        private Dictionary<string, Value> _globalMemory = new Dictionary<string, Value>();

        internal Node EvaluateAst(Node ast)
        {
            return ast.Accept(this);
        }

        public override Node Visit(FormNode form)
        {
            var newForm = new FormNode(form.Token, form.Label);
            foreach (var block in form.ChildNodes)
            {
                newForm.AddChild(block.Accept(this));
            }
            return newForm;
        }

        public override Node Visit(BlockNode block)
        {
            return new NullNode();
        }

        internal void Assign(string symbol, object value)
        {
            _globalMemory[symbol] = new Value(value);
        }
    }
}
