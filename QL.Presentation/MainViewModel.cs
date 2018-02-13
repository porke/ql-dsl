using QL.Presentation.Properties;
using ReactiveUI;
using System.Windows.Controls;
using System.Windows.Input;

namespace QL.Presentation
{
    internal class MainViewModel : ReactiveObject
    {
        private string _questionnaireContent;        
        private string _questionnaireInput;
        private string _questionnaireValidation;
        private StackPanel _questionnaireHost;

        public string AppTitle => Resources.MainTitle;

        internal MainViewModel(StackPanel questionnaireHost)
        {
            _questionnaireHost = questionnaireHost;
        }

        public string QuestionnaireContent
        {
            get { return _questionnaireContent; }
            set { this.RaiseAndSetIfChanged(ref _questionnaireContent, value); }
        }

        public StackPanel QuestionnaireHost
        {
            get { return _questionnaireHost; }            
        }

        public string QuestionnaireInput
        {
            get { return _questionnaireInput; }
            set { this.RaiseAndSetIfChanged(ref _questionnaireInput, value); }
        }

        public string QuestionnaireValidation
        {
            get { return _questionnaireValidation; }
            set { this.RaiseAndSetIfChanged(ref _questionnaireValidation, value); }
        }

        public ICommand RebuildQuestionnaireCommand { get; set; }
    }
}
