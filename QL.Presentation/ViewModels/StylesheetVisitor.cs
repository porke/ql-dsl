using QLS.Api.Ast;
using System.Collections.Generic;

namespace Presentation.ViewModels
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
            return null;
        }

        public override object Visit(SectionNode section)
        {
            return null;
        }
    }
}
