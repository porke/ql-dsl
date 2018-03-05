namespace QL.Presentation.ViewModels
{
    internal class TextQuestionViewModel : QuestionViewModel
    {
        internal TextQuestionViewModel(string description, string id, bool isEvaluated, string value, FormViewModel parentForm)
            : base(description, id, isEvaluated, value, parentForm)
        {
        }
    }
}
