using QL.Core.Ast;

namespace QL.Presentation.ViewModels
{
    internal class FormBuildingVisitor : BaseVisitor<object>
    {
        public FormViewModel Form { get; set; }

        public override object Visit(FormNode node)
        {
            Form = new FormViewModel(node.Label);
            return Form;
        }

        public override object Visit(QuestionNode node)
        {
            if (Form == null)
            {
                return null;
            }

            if (node.Type == Core.Types.QLType.Boolean)
            {
                Form.Questions.Add(new BooleanQuestionViewModel(node.Description, node.Label, false, value: false, parentForm: Form));
            }
            else
            {
                string evaluatedValue = node.ChildNodes[0].Accept(this).ToString();
                Form.Questions.Add(new TextQuestionViewModel(node.Description, node.Label, false, evaluatedValue, Form));
            }

            return null;
        }

        public override object Visit(LiteralNode node)
        {
            return node.Value;
        }
    }
}
