using Presentation.ViewModels;
using QLS.Api.Ast;
using System.Linq;
using System.Collections.Generic;
using QLS.Api.Entities;

namespace Presentation.Visitors
{
    internal class StylesheetVisitor : BaseVisitor<object>
    {
        private IReadOnlyList<QuestionViewModel> _questions;

        public PagesViewModel PagesViewModel { get; private set; } = new PagesViewModel();

        public StylesheetVisitor(IReadOnlyList<QuestionViewModel> questions)
        {
            _questions = questions;
        }

        public override object Visit(PageNode page)
        {
            var pageViewModel = new PageViewModel(page.Label);
            PagesViewModel.Pages.Add(pageViewModel);
            foreach (var sectionNode in page.ChildNodes)
            {
                pageViewModel.Sections.Sections.Add(sectionNode.Accept(this) as SectionViewModel);
            }
            
            return null;
        }

        public override object Visit(SectionNode section)
        {            
            var sectionViewModel = new SectionViewModel(section.Label);
            foreach (var questionNode in section.ChildNodes)
            {
                var questionViewModel = questionNode.Accept(this) as QuestionViewModel;
                if (questionViewModel != null)
                {
                    sectionViewModel.Questions.Add(questionViewModel);
                }

                // TODO: Include default styles
            }

            return sectionViewModel;
        }

        public override object Visit(QuestionNode question)
        {
            QuestionViewModel questionVm = _questions.FirstOrDefault(q => q.Id.Equals(question.Label));
            if (question.ChildNodes.Count == 1 && questionVm != null)
            {
                var widgetNode = question.ChildNodes[0];
                var widgetType = (WidgetType)widgetNode.Accept(this);
                questionVm.WidgetType = widgetType;

                // TODO: Include overriden styles
            }

            return questionVm;
        }

        public override object Visit(WidgetNode node)
        {
            // TODO: Extract widget options
            // return a triplet type, options (2)
            return node.WidgetType;
        }
    }
}
