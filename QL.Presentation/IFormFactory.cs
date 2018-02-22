using System.Collections.Generic;
using System.Windows;

namespace QL.Presentation
{
    public interface IFormFactory
    {
        IList<UIElement> CreateControls(string question);
    }
}
