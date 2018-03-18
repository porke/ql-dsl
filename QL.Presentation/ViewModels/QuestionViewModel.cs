using System;
using ReactiveUI;
using QLS.Api.Entities;
using QL.Api.Entities;

namespace Presentation.ViewModels
{
    internal class QuestionViewModel : ReactiveObject
    {
        private string _description;
        private string _id;
        private bool _isEvaluated;
        private object _value;
        private QLType _type;
        private FormViewModel _parentForm;

        internal QuestionViewModel(string description, string id, bool isEvaluated, object value, QLType type, FormViewModel parentForm)
        {
            _id = id;
            _description = description;
            _isEvaluated = isEvaluated;
            _parentForm = parentForm;
            _value = value;
            _type = type;

            this.ObservableForProperty(x => x.Value)
                .Subscribe(x => parentForm.QuestionValueAssignedCommand.Execute(this));
        }       

        public string Description => _description;

        public string Id => _id;

        public object Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        public WidgetType WidgetType { get; set; } = WidgetType.Textbox;

        public QLType QLType => _type;

        public bool IsEvaluated => _isEvaluated;
    }
}
