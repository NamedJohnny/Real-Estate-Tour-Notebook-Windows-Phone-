using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using RealEstateTourNotebook.Model.DataServices;
using RealEstateTourNotebook.Model.Tables;
using RealEstateTourNotebook.Utils.EditableObject;
using RealEstateTourNotebook.ViewModel.Enum;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace RealEstateTourNotebook.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddEditTourViewModel : ViewModelBase
    {
        #region private properties
        private int _selectedIndexPivot;
        private ICommand _addEditTourCommand;
        private ICommand _cancelCommand;
        private Mode _currentMode;
        private INavigationService navigationService;
        private bool _loading = true;
        private bool _isVisibleApp = false;
        private int _selectedAppType = 0;
        private int _selectedEstateType =0;

        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the AddEditTourViewModel class.
        /// </summary>
        public AddEditTourViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            TourTypeList = new ObservableCollection<TourType>();
            foreach (TourType item in System.Enum.GetValues(typeof(TourType)))
            {
                if(item!=TourType.AllTourType)
                    TourTypeList.Add(item);
            }

            EstateTypeList = new ObservableCollection<EstateType>();
            foreach (EstateType item in System.Enum.GetValues(typeof(EstateType)))
            {
                if(item != EstateType.AllEstateType)
                    EstateTypeList.Add(item);
            }

            AppTypeList = new ObservableCollection<AppType>();
            foreach (AppType item in System.Enum.GetValues(typeof(AppType)))
            {
                AppTypeList.Add(item);
            }

            Messenger.Default.Register<Tour>(this, SetTour);
        }
        #endregion constructor

        #region properties

        public bool Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                RaisePropertyChanged("Loading");
            }
        }

        public ObservableCollection<TourType> TourTypeList
        {
            get;
            set;
        }

        public ObservableCollection<EstateType> EstateTypeList
        {
            get;
            set;
        }

        public int SelectedEstateType
        {
            get
            {
                return _selectedEstateType;
            }
            set
            {
                _selectedEstateType = value;
                IsVisibleApp = (EstateTypeList[value] == EstateType.Apartment);
                Tour.EstateType = EstateTypeList[value];
                RaisePropertyChanged("SelectedEstateType");
            }
        }

        /// <summary>
        /// Mon objet editable, nécessaire pour annuler les changements
        /// </summary>
        private Caretaker<Tour> EditableObject { get; set; }

        public ObservableCollection<AppType> AppTypeList
        {
            get;
            set;
        }

        public int SelectedAppType
        {
            get
            {
                return _selectedAppType;
            }
            set
            {
                _selectedAppType = value;
                if (!IsVisibleApp)
                    Tour.AppType = null;
                else
                    Tour.AppType = AppTypeList[value];
                RaisePropertyChanged("SelectedAppType");
            }
        }

        public bool IsVisibleApp
        {
            get
            {
                return _isVisibleApp;
            }
            set
            {
                _isVisibleApp = value;
                RaisePropertyChanged("IsVisibleApp");
            }
        }

        public byte[] MainImage
        {
            get
            {
                return Tour.MainImage;
            }
            set
            {
                Tour.MainImage = value;
                RaisePropertyChanged("MainImage");
            }
        }

        public string Currency
        {
            get
            {
                return Utils.Utility.Currency;
            }
        }
        public Mode CurrentMode
        {
            get
            {
                return _currentMode;
            }
            set
            {
                _currentMode = value;
                RaisePropertyChanged("CurrentMode");
            }
        }

        /// <summary>
        /// Current Tour
        /// </summary>
        public Tour Tour
        {
            get;
            set;
        }

        public string OwnerName
        {
            get { return Tour.OwnerName; }
            set
            {
                Tour.OwnerName = value;
                RaisePropertyChanged("OwnerName");
            }
        }

        public int PhoneNumber
        {
            get { return Tour.PhoneNumber; }
            set
            {
                Tour.PhoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        public int Price
        {
            get { return Tour.Price; }
            set
            {
                Tour.Price = value;
                RaisePropertyChanged("Price");
            }
        }

        public DateTime Date
        {
            get
            {
                return Tour.Date;
            }
            set
            {
                Tour.Date = value;
                RaisePropertyChanged("Date");
            }
        }
        public TourType SelectedItem
        {
            get { return Tour.Type; }
            set
            {
                Tour.Type = value;
                RaisePropertyChanged("SelectedItem");
                SelectedIndexPivot = 1;
            }
        }

        public int SelectedIndexPivot
        {
            get { return _selectedIndexPivot; }
            set
            {
                _selectedIndexPivot = value;
                RaisePropertyChanged("SelectedIndexPivot");
            }
        }

        public ICommand AddEditTourCommand
        {
            get
            {
                if (_addEditTourCommand == null)
                {
                    _addEditTourCommand = new RelayCommand(() => AddEditTour());
                }
                return _addEditTourCommand;
            }

        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(() => Cancel());
                }
                return _cancelCommand;
            }

        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new RelayCommand(() => navigationService.GoBack());
                }
                return _goBackCommand;
            }

        }

        public bool IsFormValid
        {
            get;
            set;
        }


        #endregion properties

        #region methods

        public void Cancel()
        {
            EditableObject.CancelEdit();
        }
        private void AddEditTour()
        {
            IsFormValid = true;
            if (CurrentMode == Mode.NewTour)
            {
                Tour.ConstructionYear = DateTime.Now.Year;
                AddTour();
            }
            else
            {
                EditTour();
            }
            EditableObject.EndEdit();
            Messenger.Default.Send<Tour, CheckListViewModel>(Tour);
            navigationService.NavigateTo("CheckList");
        }

        private void SetTour(Tour tour)
        {
            if (tour != null)
            {
                Tour = tour;
                this.CurrentMode = Mode.EditTour;
                if (Tour.AppType != null)
                    SelectedAppType = AppTypeList.IndexOf(Tour.AppType.Value);
                SelectedEstateType = EstateTypeList.IndexOf(Tour.EstateType);
            }
            else
            {
                Tour = new Tour();
                Tour.Type = TourType.Rent;
                SelectedIndexPivot = 0;
                Tour.Date = DateTime.Now;
                this.CurrentMode = Mode.NewTour;
                SelectedAppType = 0;
                SelectedEstateType = 0;
            }
            IsFormValid = false;
            EditableObject = new Caretaker<Tour>(this.Tour);
            EditableObject.BeginEdit();

        }
        public void AddTour()
        {
            DataServiceTour tour = new DataServiceTour();
            tour.addTour(this.Tour);
        }

        public void EditTour()
        {
            DataServiceTour tour = new DataServiceTour();
            tour.UpdateTour();
        }

        #endregion methods

    }
}