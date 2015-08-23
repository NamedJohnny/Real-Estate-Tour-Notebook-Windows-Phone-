using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace RealEstateTourNotebook.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PictureViewModel : ViewModelBase
    {
        private int _selectedIndex;
        private ICommand _cancelCommand;
        private INavigationService navigationService;

        /// <summary>
        /// Initializes a new instance of the PhotoViewModel class.
        /// </summary>
        public PictureViewModel(INavigationService navService)
        {
            Messenger.Default.Register<Tuple<int, List<byte[]>>>(this,
 tuple =>
 {
     PictureList = tuple.Item2;
     SelectedIndex = tuple.Item1;

 });

            this.navigationService = navService;
        }

        public List<byte[]> PictureList
        {
            get;
            set;
        }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;

                if (_selectedIndex < 0)
                    _selectedIndex = PictureList.Count - 1;

                if (_selectedIndex >= PictureList.Count)
                    _selectedIndex = 0;
            }
        }

        public byte[] SelectedPicture
        {
            get
            {
                return PictureList[SelectedIndex];
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(() => navigationService.GoBack());
                }
                return _cancelCommand;
            }

        }

    }
}