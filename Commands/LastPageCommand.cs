using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MVVMListViewPagination.ViewModels;

namespace MVVMListViewPagination.Commands
{
    class LastPageCommand : ICommand
    {
        private MainViewModel viewModel;

        public LastPageCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.CurrentPage != viewModel.TotalPages;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            viewModel.ShowLastPage();
        }
    }
}
