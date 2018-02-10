using QL.Presentation.Properties;
using ReactiveUI;
using System.Windows.Input;

namespace QL.Presentation
{
    internal class MainViewModel : ReactiveObject
    {
        private string _questionnaireContent;
        private string _questionnaireOutput;
        private string _questionnaireInput;
        private string _questionnaireValidation;

        public string AppTitle => Resources.MainTitle;

        public string QuestionnaireContent
        {
            get { return _questionnaireContent; }
            set { this.RaiseAndSetIfChanged(ref _questionnaireContent, value); }
        }

        public string QuestionnaireOutput
        {
            get { return _questionnaireOutput; }
            set { this.RaiseAndSetIfChanged(ref _questionnaireOutput, value); }
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
