using Presentation.Properties;
using ReactiveUI;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    internal class MainViewModel : ReactiveObject
    {
        private string _questionnaireInput;
        private string _stylesheetInput;
        private string _questionnaireValidation;
        private FormViewModel _formViewModel = new FormViewModel("Default");

        public string AppTitle => Resources.MainTitle;

        public FormViewModel Form
        {
            get { return _formViewModel; }
            set { this.RaiseAndSetIfChanged(ref _formViewModel, value); }
        }
        
        public string QuestionnaireInput
        {
            get { return _questionnaireInput; }
            set { this.RaiseAndSetIfChanged(ref _questionnaireInput, value); }
        }

        public string StylesheetInput
        {
            get { return _stylesheetInput; }
            set { this.RaiseAndSetIfChanged(ref _stylesheetInput, value); }
        }

        public string QuestionnaireValidation
        {
            get { return _questionnaireValidation; }
            set { this.RaiseAndSetIfChanged(ref _questionnaireValidation, value); }
        }

        public ICommand RebuildQuestionnaireCommand { get; set; }
    }
}
