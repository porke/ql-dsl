using Presentation.ViewModels;
using QL.Api.Ast;

namespace Presentation.Visitors
{
    internal class QuestionnaireVisitor : BaseVisitor<object>
    {
        public SinglePageFormViewModel Form { get; private set; }

        public override object Visit(FormNode node)
        {
            Form = new SinglePageFormViewModel(node.Label);
            VisitChildren(node);

            return Form;
        }

        public override object Visit(QuestionNode node)
        {
            if (Form == null)
            {
                return null;
            }

            string evaluatedValue = node.ChildNodes.Count > 0 ? node.ChildNodes[0].Accept(this).ToString() : string.Empty;
            Form.Questions.Add(new QuestionViewModel(node.Description, node.Label, node.IsEvaluated, evaluatedValue, node.Type, parentForm: Form));

            return null;
        }

        public override object Visit(LiteralNode node)
        {
            return node.Value;
        }
    }
}
