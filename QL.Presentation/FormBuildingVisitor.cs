using QL.Core.Ast;
using QL.Core.Ast.Visitors;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace QL.Presentation
{
    internal class FormBuildingVisitor : IVisitor
    {
        public IList<Control> Controls = new List<Control>();

        public void Visit(FormNode node)
        {
            Controls.Add(new Label { Content = node.Label, FontWeight = FontWeights.Bold });
        }

        public void Visit(QuestionNode node)
        {
            Controls.Add(new Label { Content = node.Description });
        }

        public void Visit(EmptyNode node)
        {
            // Do not add any controls for an empty node
        }
    }
}
