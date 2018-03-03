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
                Form.Questions.Add(new TextQuestionViewModel(node.Description, node.Label, false, string.Empty, Form));
            }

            return null;
        }
    }
}
