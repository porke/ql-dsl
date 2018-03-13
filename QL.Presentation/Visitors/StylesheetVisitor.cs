using Presentation.ViewModels;
using QLS.Api.Ast;
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
            foreach (var section in page.ChildNodes)
            {
                pageViewModel.Sections.Sections.Add(section.Accept(this) as SectionViewModel);
            }
            
            return null;
        }

        public override object Visit(SectionNode section)
        {            
            return new SectionViewModel(section.Label);
        }
    }
}
