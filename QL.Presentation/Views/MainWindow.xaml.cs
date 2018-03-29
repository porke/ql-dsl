using Presentation.Controllers;
using Presentation.ViewModels;

namespace Presentation.Views
{
    public partial class MainWindow
    {
        private MainController _controller;

        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainViewModel();
            DataContext = viewModel;
            _controller = new MainController(viewModel,
                                             QL.Core.Module.ParsingPipeline,
                                             QL.Core.Module.InterpretingPipeline,
                                             QLS.Core.Module.ParsingPipeline,
                                             QL.Core.Module.ValueFactory);
        }
    }
}
