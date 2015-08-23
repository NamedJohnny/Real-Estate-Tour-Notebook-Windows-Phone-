using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using RealEstateTourNotebook.Model.Tables;
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
    public class TourViewModel : ViewModelBase
    {
        private ICommand _checkListCommand;
        private ICommand _addEditTourCommand;
        private INavigationService navigationService;
        /// <summary>
        /// Initializes a new instance of the TourViewModel class.
        /// </summary>
        public TourViewModel(INavigationService navigationService)
        {
            Messenger.Default.Register<Tour>(this,
   tour =>
   {
       this.Tour = tour;
   });

            this.navigationService = navigationService;
        }

        #region Proprerties
        
        public Tour Tour
        {
            get;
            set;
        }

        public string Currency
        {
            get
            {
                return Utils.Utility.Currency;
            }
        }

        public ICommand CheckListCommand
        {
            get
            {
                if (_checkListCommand == null)
                {
                    _checkListCommand = new RelayCommand(() => CheckList());
                }
                return _checkListCommand;
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

        #endregion

        #region DBMethods

        public void AddEditTour(Tour tour)
        {
            Messenger.Default.Send<Tour, AddEditTourViewModel>(tour);
            navigationService.NavigateTo("NewTour");
        }
        private void CheckList()
        {
            Messenger.Default.Send<Tour, CheckListViewModel>(Tour);
            navigationService.NavigateTo("CheckList");
        }

        #endregion

    }
}