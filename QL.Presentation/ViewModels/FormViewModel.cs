using ReactiveUI;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    internal abstract class FormViewModel : ReactiveObject
    {
        protected FormViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public ICommand QuestionValueAssignedCommand { get; set; }
    }
}
