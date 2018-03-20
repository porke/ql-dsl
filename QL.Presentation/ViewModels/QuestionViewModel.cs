using System;
using ReactiveUI;
using QLS.Api.Entities;
using QL.Api.Entities;

namespace Presentation.ViewModels
{
    internal class QuestionViewModel : ReactiveObject
    {
        private object _value;
        private FormViewModel _parentForm;

        internal QuestionViewModel(string description, string id, bool isEvaluated, object value, QLType type, FormViewModel parentForm)
        {
            _parentForm = parentForm;
            _value = value;

            Id = id;
            Description = description;
            IsEvaluated = isEvaluated;            
            QLType = type;

            this.ObservableForProperty(x => x.Value)
                .Subscribe(x => parentForm.QuestionValueAssignedCommand.Execute(this));
        }

        public string Description { get; }

        public string Id { get; }

        public object Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        public WidgetType WidgetType { get; set; } = WidgetType.Textbox;

        public QLType QLType { get; }

        public bool IsEvaluated { get; }
    }
}
