using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using RealEstateTourNotebook.Group;
using RealEstateTourNotebook.Model.DataServices;
using RealEstateTourNotebook.Model.Tables;
using RealEstateTourNotebook.Utils;
using RealEstateTourNotebook.ViewModel.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace RealEstateTourNotebook.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ListTourViewModel : ViewModelBase
    {
        #region private properties
        private ICommand _addEditTourCommand;
        private ICommand _filterCommand;
        private ICommand _deleteTourCommand;
        private ICommand _checkListTourCommand;
        private ICommand _compareCommand;
        private INavigationService navigationService;
        private FilterBy _currentFilter;
        #endregion private properties

        #region Contructor
        /// <summary>
        /// Initializes a new instance of the ListTourViewModel class.
        /// </summary>
        public ListTourViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            TourList = new List<Tour>();
            //On filtre la liste
            Messenger.Default.Register<FilterBy>(this, filter =>
            {
                SetTourList();
            });
        }
        #endregion Constructor

        #region public properties

        public List<KeyedList<string, Tour>> GroupedTours
        {
            get
            {
                var groupedNotes =
                    from tour in TourList
                    orderby tour.Date
                    group tour by tour.Date.ToString("m") into notesByDay
                    select new KeyedList<string, Tour>(notesByDay);

                return new List<KeyedList<string, Tour>>(groupedNotes);
            }
        }

        public ICommand AddEditTourCommand
        {
            get
            {
                if (_addEditTourCommand == null)
                {
                    _addEditTourCommand = new RelayCommand<Tour>((tour) => AddEditTour(tour));
                }
                return _addEditTourCommand;
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new RelayCommand(() => navigationService.NavigateTo("Filter"));
                }
                return _filterCommand;
            }
        }

        public ICommand DeleteTourCommand
        {
            get
            {
                if (_deleteTourCommand == null)
                {
                    _deleteTourCommand = new RelayCommand<object>((tour) => DeleteTour(tour));
                }
                return _deleteTourCommand;
            }
        }

        public ICommand CheckListTourCommand
        {
            get
            {
                if (_checkListTourCommand == null)
                {
                    _checkListTourCommand = new RelayCommand<Tour>((tour) => CheckList(tour));
                }
                return _checkListTourCommand;
            }
        }


        public ICommand CompareCommand
        {
            get
            {
                if (_compareCommand == null)
                {
                    _compareCommand = new RelayCommand<List<Tour>>((tours) => CompareTours(tours));
                }
                return _compareCommand;
            }
        }

        public List<Tour> TourList
        {
            get;
            set;
        }

        private Tour _selectedItem;
        public Tour SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                Messenger.Default.Send<Tour, TourViewModel>(_selectedItem);
                navigationService.NavigateTo("Tour");
            }
        }

        public string Currency
        {
            get
            {
                return Utils.Utility.Currency;
            }
        }

        #endregion public properties

        #region DB Methods

        public void AddEditTour(Tour tour)
        {
            Messenger.Default.Send<Tour, AddEditTourViewModel>(tour);
            navigationService.NavigateTo("NewTour");
        }

        public void CompareTours(List<Tour> tours)
        {
            Messenger.Default.Send<List<Tour>, CompareViewModel>(tours);
            navigationService.NavigateTo("Compare");
        }

        public void CheckList(Tour tour)
        {
            Messenger.Default.Send<Tour, CheckListViewModel>(tour);
            navigationService.NavigateTo("CheckList");
        }
        public void SetTourList()
        {
            DataServiceTour dsTour = new DataServiceTour();
            this.TourList = dsTour.LoadTours();

            if (FilterBy.CurrentTourType != TourType.AllTourType)
            {
                this.TourList = this.TourList.Where(x => x.Type == FilterBy.CurrentTourType).ToList();
            }

            if (FilterBy.CurrentEstate != EstateType.AllEstateType)
            {
                this.TourList = this.TourList.Where(x => x.EstateType == FilterBy.CurrentEstate).ToList();
            }
        }

        private void DeleteTour(object tour)
        {
            DataServiceTour dsTour = new DataServiceTour();
            if (tour is Tour)
            {
                this.TourList.Remove(tour as Tour);
                dsTour.DeleteTour(tour as Tour);
            }
            else if (tour is List<Tour>)
            {
                foreach (Tour tour_Loop in tour as List<Tour>)
                {
                    this.TourList.Remove(tour_Loop as Tour);
                    dsTour.DeleteTour(tour_Loop);
                }
            }

        }
        #endregion DB Methods
    }
}