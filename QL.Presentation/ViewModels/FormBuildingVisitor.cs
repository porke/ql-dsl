using QL.Core.Ast;

namespace QL.Presentation.ViewModels
{
    internal class FormBuildingVisitor : BaseVisitor
    {
        public FormViewModel Form { get; set; }

        public override void VisitEnter(FormNode node)
        {
            Form = new FormViewModel(node.Label);
        }

        public override void VisitEnter(QuestionNode node)
        {
            if (Form == null)
            {
                return;
            }

            if (node.Type == Core.Types.QLType.Boolean)
            {
                Form.Questions.Add(new BooleanQuestionViewModel(node.Description, node.Label, false, value: false, parentForm: Form));
            }
            else
            {
                Form.Questions.Add(new TextQuestionViewModel(node.Description, node.Label, false, string.Empty, Form));
            }
        }
    }
}
