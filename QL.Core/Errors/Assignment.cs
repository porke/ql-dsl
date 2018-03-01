namespace QL.Core.Errors
{
    public class Assignment : Error
    {
        public string QuestionType;
        public string AssignmentType;
        public int ErrorLine;

        public Assignment(string questionType, string assignmentType, int errorLine)
        {
            QuestionType = questionType;
            AssignmentType = assignmentType;
            ErrorLine = errorLine;
        }

        public override string ToString()
        {
            return $"Type error in line {ErrorLine}: The question is of type \'{QuestionType}\'"
                    + $"but the assignment evaluates to a(n) {AssignmentType}";
        }
    }
}
