namespace QLS.Core.Validation.Errors
{
    class WidgetNotCompatible : Error
    {
        private string _questionName;
        private string _questionType;
        private string _widgetType;

        public WidgetNotCompatible(string questionName, string questionType, string widgetType, int errorLine)
        {
            _questionName = questionName;
            _questionType = questionType;
            _widgetType = widgetType;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: The type of {_questionName} ({_questionType}) is not compatible with the type of the widget ({_widgetType}).";
        }
    }
}
