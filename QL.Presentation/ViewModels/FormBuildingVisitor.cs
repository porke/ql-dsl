using QL.Core.Ast;
using QL.Core.Types;

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

            if (node.Type == QLType.Boolean)
            {
                bool evaluatedValue = node.ChildNodes.Count > 0 ? bool.Parse(node.ChildNodes[0].Accept(this).ToString()) : false;
                Form.Questions.Add(new BooleanQuestionViewModel(node.Description, node.Label, false, evaluatedValue, parentForm: Form));
            }
            else
            {
                string evaluatedValue = node.ChildNodes.Count > 0 ? node.ChildNodes[0].Accept(this).ToString() : string.Empty;
                Form.Questions.Add(new TextQuestionViewModel(node.Description, node.Label, false, evaluatedValue, parentForm: Form));
            }

            return null;
        }

        public override object Visit(LiteralNode node)
        {
            return node.Value;
        }
    }
}
