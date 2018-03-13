using QL.Api.Ast;
using QL.Api.Types;

namespace Presentation.ViewModels
{
    internal class FormViewModelBuildingVisitor : BaseVisitor<object>
    {
        public FormViewModel Form { get; private set; }

        public override object Visit(FormNode node)
        {
            Form = new FormViewModel(node.Label);
            VisitChildren(node);

            return Form;
        }

        public override object Visit(QuestionNode node)
        {
            if (Form == null)
            {
                return null;
            }

            // TODO: Check if it is an evaluated value
            if (node.Type == QLType.Boolean)
            {                
                bool evaluatedValue = node.ChildNodes.Count > 0 ? bool.Parse(node.ChildNodes[0].Accept(this).ToString()) : false;
                Form.Questions.Add(new BooleanQuestionViewModel(node.Description, node.Label, node.IsEvaluated, evaluatedValue, parentForm: Form));
            }
            else
            {
                string evaluatedValue = node.ChildNodes.Count > 0 ? node.ChildNodes[0].Accept(this).ToString() : string.Empty;
                Form.Questions.Add(new TextQuestionViewModel(node.Description, node.Label, node.IsEvaluated, evaluatedValue, parentForm: Form));
            }

            return null;
        }

        public override object Visit(LiteralNode node)
        {
            return node.Value;
        }
    }
}
