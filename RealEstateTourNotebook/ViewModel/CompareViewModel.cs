using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using RealEstateTourNotebook.Model.Tables;
using System;
using System.Collections.Generic;
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
    public class CompareViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private ICommand _viewPictureCommand;
        /// <summary>
        /// Initializes a new instance of the CompareVIewModel class.
        /// </summary>
        public CompareViewModel(INavigationService navigationService)
        {
            Messenger.Default.Register<List<Tour>>(this,
tours =>
{
    TourList = new ObservableCollection<Tour>(tours);
});
            this.navigationService = navigationService;
        }

        public ObservableCollection<Tour> TourList
        {
            get;
            set;
        }

        public ICommand ViewPictureCommand
        {
            get
            {
                if (_viewPictureCommand == null)
                {
                    _viewPictureCommand = new RelayCommand<Tuple<int, List<byte[]>>>((list) => PictureView(list));
                }
                return _viewPictureCommand;
            }

        }

        public string UnitDistance
        {
            get
            {
                return Utils.Utility.UnitDistance;
            }
        }
        private void PictureView(Tuple<int, List<byte[]>> tuple)
        {
            Messenger.Default.Send<Tuple<int, List<byte[]>>, PictureViewModel>(tuple);
            navigationService.NavigateTo("PictureView");
        }
    }
}