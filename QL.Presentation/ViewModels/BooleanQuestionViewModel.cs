namespace Presentation.ViewModels
{
    internal class BooleanQuestionViewModel : QuestionViewModel
    {        
        public BooleanQuestionViewModel(string description, string id, bool isEvaluated, bool value, FormViewModel parentForm) 
            : base(description, id, isEvaluated, value, parentForm)
        {
        }
    }
}
