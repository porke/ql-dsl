using QL.Core.Ast;
using QL.Core.Types;
using System.Collections.Generic;

namespace QL.Core.Interpreting
{
    internal class InterpreterVisitor
    {       
        private Dictionary<string, Value> _globalMemory = new Dictionary<string, Value>();

        internal InterpreterVisitor()
        {

        }

        public Node EvaluateForm(FormNode form)
        {
            var ret = new FormNode(form.Token, form.Label);
            foreach (var block in form.ChildNodes)
            {

            }
            return ret;
        }

        internal void Assign(string symbol, object value)
        {
            _globalMemory[symbol] = new Value(value);
        }
    }
}
