using QL.Core.Ast;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace QL.Presentation
{
    internal class FormBuildingVisitor : BaseVisitor
    {
        public IList<UIElement> Controls = new List<UIElement>();

        public override void VisitEnter(FormNode node)
        {
            Controls.Add(new Label { Content = node.Label, FontWeight = FontWeights.Bold });
        }

        public override void VisitEnter(QuestionNode node)
        {
            var border = new Border { BorderBrush = Brushes.Black };
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            border.Child = stackPanel;
            switch (node.Type)
            {
                case "boolean":
                    stackPanel.Children.Add(new CheckBox { Content = node.Description });
                    break;
                default:
                    stackPanel.Children.Add(new Label { Content = node.Description });
                    stackPanel.Children.Add(new TextBox());
                    break;
            }            

            Controls.Add(border);
        }
    }
}
