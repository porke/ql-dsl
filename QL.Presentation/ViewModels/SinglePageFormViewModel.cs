using ReactiveUI;

namespace Presentation.ViewModels
{
    internal class SinglePageFormViewModel : FormViewModel
    {
        internal SinglePageFormViewModel(string formName) : base(formName)
        {
        }        

        public IReactiveList<QuestionViewModel> Questions { get; } = new ReactiveList<QuestionViewModel>();        
    }
}
