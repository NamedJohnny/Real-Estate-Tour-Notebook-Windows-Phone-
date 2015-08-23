using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using RealEstateTourNotebook.Utils;
using RealEstateTourNotebook.ViewModel.Enum;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RealEstateTourNotebook.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class FilterByViewModel : ViewModelBase
    {
        private ICommand _listFilterCommand;
        private INavigationService navigationService;
        /// <summary>
        /// Initializes a new instance of the FilterByViewModel class.
        /// </summary>
        public FilterByViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            TourTypeList = new ObservableCollection<TourType>();
            foreach (TourType item in System.Enum.GetValues(typeof(TourType)))
            {
                TourTypeList.Add(item);
            }

            EstateTypeList = new ObservableCollection<EstateType>();
            foreach (EstateType item in System.Enum.GetValues(typeof(EstateType)))
            {
                EstateTypeList.Add(item);
            }
        }

        public ObservableCollection<TourType> TourTypeList
        {
            get;
            set;
        }

        public TourType SelectedItem
        {
            get
            {
                return FilterBy.CurrentTourType;
            }
            set
            {
                FilterBy.CurrentTourType = value;
            }
        }

        public EstateType SelectedEstate
        {
            get
            {
                return FilterBy.CurrentEstate;
            }
            set
            {
                FilterBy.CurrentEstate = value;
            }
        }

        public ObservableCollection<EstateType> EstateTypeList
        {
            get;
            set;
        }

        public ICommand ListFilterCommand
        {
            get
            {
                if (_listFilterCommand == null)
                {
                    _listFilterCommand = new RelayCommand(() => ListFilter());
                }
                return _listFilterCommand;
            }

        }

        private void ListFilter()
        {
            Messenger.Default.Send<FilterBy>(FilterBy.Instance);
            navigationService.NavigateTo("ListTour");
        }

    }
}