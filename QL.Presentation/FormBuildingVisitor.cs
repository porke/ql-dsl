using QL.Core.Ast;
using QL.Core.Ast.Visitors;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace QL.Presentation
{
    internal class FormBuildingVisitor : IVisitor
    {
        public IList<UIElement> Controls = new List<UIElement>();

        public void Visit(FormNode node)
        {
            Controls.Add(new Label { Content = node.Label, FontWeight = FontWeights.Bold });
        }

        public void Visit(QuestionNode node)
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

        public void Visit(ConditionalNode node)
        {
            // TODO: See if controls need to be added
        }

        public void Visit(ExpressionNode node)
        {
            // TODO: See if controls need to be added
        }

        public void Visit(LiteralNode node)
        {
            // TODO: See if controls need to be added
        }

        public void Visit(VariableNode node)
        {
            // TODO: See if controls need to be added
        }

        public void Visit(EmptyNode node)
        {
            // Do not add any controls for an empty node
        }
    }
}
