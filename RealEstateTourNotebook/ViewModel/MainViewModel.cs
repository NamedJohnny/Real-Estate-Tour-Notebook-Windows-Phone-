using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using RealEstateTourNotebook.Model;
using RealEstateTourNotebook.Model.Tables;
using RealEstateTourNotebook.Utils;
using RealEstateTourNotebook.ViewModel.Enum;
using System.Windows.Input;

namespace RealEstateTourNotebook.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region private
        private ICommand _addEditTourCommand;
        private ICommand _listTourCommand;
        private ICommand _settingsCommand;
        private INavigationService navigationService;
        #endregion private

        #region constructor
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
        {
            
            this.navigationService = navigationService;
        }
        #endregion constructor

        #region public properties
        public ICommand AddEditTourCommand
        {
            get
            {
                if (_addEditTourCommand == null)
                {
                    _addEditTourCommand = new RelayCommand(() => AddTour());
                }
                return _addEditTourCommand;
            }
        }
        public ICommand ListTourCommand
        {
            get
            {
                if (_listTourCommand == null)
                {
                    _listTourCommand = new RelayCommand(() => ListTour());
                }
                return _listTourCommand;
            }
        }

        public ICommand SettingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                {
                    _settingsCommand = new RelayCommand(() => navigationService.NavigateTo("Settings"));
                }
                return _settingsCommand;
            }
        }

        #endregion public properties

        #region Methods

        private void ListTour()
        {
            Messenger.Default.Send<FilterBy>(FilterBy.Instance);
            navigationService.NavigateTo("ListTour");
        }
        private void AddTour()
        {
            Messenger.Default.Send<Tour, AddEditTourViewModel>(null);
            navigationService.NavigateTo("NewTour");
        }

        #endregion Methods
    }
}