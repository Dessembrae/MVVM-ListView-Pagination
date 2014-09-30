using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MVVMListViewPagination.ViewModels;

namespace MVVMListViewPagination.Commands
{
    class NextPageCommand : ICommand
    {
        private MainViewModel viewModel;

        public NextPageCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.TotalPages - 1 > viewModel.CurrentPageIndex;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            viewModel.ShowNextPage();
        }
    }
}
