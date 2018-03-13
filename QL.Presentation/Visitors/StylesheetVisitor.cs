using Presentation.ViewModels;
using QLS.Api.Ast;
using System.Linq;
using System.Collections.Generic;

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
            }

            return sectionViewModel;
        }

        public override object Visit(QuestionNode question)
        {
            return _questions.FirstOrDefault(q => q.Id.Equals(question.Label));
        }
    }
}
