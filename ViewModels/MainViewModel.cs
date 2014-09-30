using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMListViewPagination.Commands;
using MVVMListViewPagination.Models;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace MVVMListViewPagination.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        #region Commands
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }
        #endregion

        #region Fields And Properties
        private int itemPerPage = 15;
        private int itemcount;
        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set 
            { 
                _currentPageIndex = value; 
                OnPropertyChanged("CurrentPage"); 
            }
        }
        public int CurrentPage
        {
            get { return _currentPageIndex + 1; }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            private set 
            { 
                _totalPages = value; 
                OnPropertyChanged("TotalPage"); 
            }
        }

        public CollectionViewSource ViewList { get; set; }
        private ObservableCollection<Person> peopleList = new ObservableCollection<Person>();
        public ReadOnlyObservableCollection<Person> PeopleList
        {
            get { return new ReadOnlyObservableCollection<Person>(peopleList); }
        }
        #endregion

        #region Pagination Methods
        public void ShowNextPage()
        {
            CurrentPageIndex++;
            ViewList.View.Refresh();
        }

        public void ShowPreviousPage()
        {
            CurrentPageIndex--;
            ViewList.View.Refresh();
        }

        public void ShowFirstPage()
        {
            CurrentPageIndex = 0;
            ViewList.View.Refresh();
        }

        public void ShowLastPage()
        {
            CurrentPageIndex = TotalPages - 1;
            ViewList.View.Refresh();
        }

        void view_Filter(object sender, FilterEventArgs e)
        {
            int index = ((Person)e.Item).Id - 1;
            if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void CalculateTotalPages()
        {
            if (itemcount % itemPerPage == 0)
            {
                TotalPages = (itemcount / itemPerPage);
            }
            else
            {
                TotalPages = (itemcount / itemPerPage) + 1;
            }
        }
        #endregion

        public MainViewModel()
        {
            populateList();

            ViewList = new CollectionViewSource();
            ViewList.Source = PeopleList;
            ViewList.Filter += new FilterEventHandler(view_Filter);

            CurrentPageIndex = 0;
            itemcount = peopleList.Count;
            CalculateTotalPages();

            NextCommand = new NextPageCommand(this);
            PreviousCommand = new PreviousPageCommand(this);
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);
        }

        private void populateList()
        {
            for (int i = 0; i < 100; i++)
            {
                peopleList.Add(new Person(i, "Jack", "Black"));
            }
        }
    }
}
