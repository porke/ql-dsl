namespace QL.Core.Validation.Errors
{
    internal class QuestionAssignmentType : Error
    {
        private string _questionType;
        private string _assignmentType;

        public QuestionAssignmentType(string questionType, string assignmentType, int errorLine)
        {
            _questionType = questionType;
            _assignmentType = assignmentType;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: The question is of type \'{_questionType}\' "
                    + $"but the assignment evaluates to a(n) {_assignmentType}";
        }
    }
}
