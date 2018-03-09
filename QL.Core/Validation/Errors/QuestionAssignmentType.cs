namespace QL.Core.Validation.Errors
{
    internal class QuestionAssignmentType : Error
    {
        public string QuestionType;
        public string AssignmentType;
        public int ErrorLine;

        public QuestionAssignmentType(string questionType, string assignmentType, int errorLine)
        {
            QuestionType = questionType;
            AssignmentType = assignmentType;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: The question is of type \'{QuestionType}\' "
                    + $"but the assignment evaluates to a(n) {AssignmentType}";
        }
    }
}
