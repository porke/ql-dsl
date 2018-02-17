using System.Windows.Controls;

namespace QL.Presentation
{
    public interface IFormConstructor
    {
        Control MakeControlFromQuestion(string question);
    }
}
