using System;
using ReactiveUI;
using QLS.Api.Entities;
using QL.Api.Entities;
using Presentation.Properties;

namespace Presentation.ViewModels
{
    internal class QuestionViewModel : ReactiveObject
    {
        private object _value;

        internal QuestionViewModel(string description, string id, bool isEvaluated, object value, QLType type, FormViewModel parentForm)
        {
            _value = value;

            Id = id;
            Description = description;
            IsEvaluated = isEvaluated;            
            QLType = type;
            Widget = type != QLType.Boolean ? WidgetType.Textbox : WidgetType.Radio;

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

        public StyleViewModel Style { get; set; }

        public WidgetType Widget { get; set; }

        public string YesOption { get; set; } = Resources.Yes;

        public string NoOption { get; set; } = Resources.No;

        public QLType QLType { get; }

        public bool IsEvaluated { get; }
    }
}
