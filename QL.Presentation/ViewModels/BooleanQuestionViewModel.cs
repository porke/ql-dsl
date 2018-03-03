using ReactiveUI;
using System;

namespace QL.Presentation.ViewModels
{
    internal class BooleanQuestionViewModel : QuestionViewModel
    {
        private bool _value;

        public BooleanQuestionViewModel(string description, string id, bool isEvaluated, bool value, FormViewModel parentForm) 
            : base(description, id, isEvaluated, parentForm)
        {
            _value = value;

            this.ObservableForProperty(x => x.Value)
                .Subscribe(x => parentForm.QuestionValueAssignedCommand.Execute(this));
        }

        public bool Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }
    }
}
