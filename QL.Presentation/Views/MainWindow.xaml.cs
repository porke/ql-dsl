﻿using QL.Core.Api;
using QL.Presentation.Controllers;
using QL.Presentation.ViewModels;
using System.Windows;

namespace QL.Presentation
{
    public partial class MainWindow : Window
    {
        private MainController _controller;

        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainViewModel();
            DataContext = viewModel;
            _controller = new MainController(viewModel, ServiceRegistry.ParsingService, ServiceRegistry.InterpretingService);
        }
    }
}
