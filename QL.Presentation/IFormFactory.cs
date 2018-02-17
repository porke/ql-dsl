using System.Collections.Generic;
using System.Windows.Controls;

namespace QL.Presentation
{
    public interface IFormFactory
    {
        IList<Control> CreateControls(string question);
    }
}
