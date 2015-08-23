using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using RealEstateTourNotebook.Model.DataServices;
using RealEstateTourNotebook.Model.Tables;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Globalization;
using RealEstateTourNotebook.ViewModel.Enum;
using RealEstateTourNotebook.Controls;
using System;
using RealEstateTourNotebook.Utils.EditableObject;

namespace RealEstateTourNotebook.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CheckListViewModel : ViewModelBase
    {
        #region private properties
        private ICommand _saveCommand;
        private ICommand _cancelCommand;
        private ICommand _viewPictureCommand;
        private ICommand _addPictureTourCommand;
        private ICommand _deletePictureTourCommand;
        private ICommand _deleteRepairCommand;
        private ICommand _addRepairCommand;
        private ICommand _addRecentRenovationCommand;
        private ICommand _addNearByCommand;
        private ICommand _deleteRecentRenovationCommand;
        private ICommand _deleteNearByCommand;
        private INavigationService navigationService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CheckListViewModel class.
        /// </summary>
        public CheckListViewModel(INavigationService navigationService)
        {
            Messenger.Default.Register<Tour>(this,
  tour =>
  {
      DataServiceTour dsTour = new DataServiceTour();
      Tour = dsTour.getTourById(tour.Id);
      RepairList = new ObservableCollection<Repair>(tour.Repair);
      PictureTourList = new ObservableCollection<PictureTour>(tour.Pictures);
      NearByList = new ObservableCollection<NearBy>(tour.NearBy);

      RecentRenovations = new ObservableCollection<RecentRenov>();
      foreach (var item in Utils.Utility.StringToList(tour.RecentRenovations))
      {
          RecentRenovations.Add(new RecentRenov(){Name= item});
      }
      RaisePropertyChanged("HasLand");
      EditableObject = new Caretaker<Tour>(this.Tour);
      EditableObject.BeginEdit();
      IsFormValid = false;
  });
            this.navigationService = navigationService;
        }
        #endregion

        #region public properties

        /// <summary>
        /// Mon objet editable, nécessaire pour annuler les changements
        /// </summary>
        private Caretaker<Tour> EditableObject { get; set; }

        public Tour Tour
        {
            get;
            set;
        }

        public ObservableCollection<Repair> RepairList
        {
            get;
            set;
        }

        public ObservableCollection<PictureTour> PictureTourList
        {
            get;
            set;
        }

        public ObservableCollection<RecentRenov> RecentRenovations
        {
            get;
            set;
        }

        public ObservableCollection<NearBy> NearByList
        {
            get;
            set;
        }

        public string UnitDistance
        {
            get
            {
                return Utils.Utility.UnitDistance;
            }
        }

        public int ConstructionYear
        {
            get
            {
                return Tour.ConstructionYear;
            }
            set
            {
                Tour.ConstructionYear = value;
                RaisePropertyChanged("ConstructionYear");
            }
        }

        public int Heating
        {
            get
            {
                return Tour.HeatingPrice;
            }
            set
            {
                Tour.HeatingPrice = value;
                RaisePropertyChanged("Heating");
            }
        }

        public string Notes
        {
            get
            {
                return Tour.Notes;
            }
            set
            {
                Tour.Notes = value;
                RaisePropertyChanged("Notes");
            }
        }

        public bool HasLand
        {
            get
            {
                return Tour.Land!=0;
            }
            set
            {
                if (value == false)
                {
                    Tour.Land = 0;
                }
            }
        }
        public int TourLand
        {
            get
            {
                return Tour.Land;
            }
            set
            {
                Tour.Land = value;
                RaisePropertyChanged("TourLand");
            }
        }

        public double NeighborhoodQuality
        {
            get { return Tour.NeighborhoodQuality; }
            set
            {
                Tour.NeighborhoodQuality = value;
                RaisePropertyChanged("NeighborhoodQuality");
            }
        }

        public string NeighborhoodNotes
        {
            get { return Tour.NeighborhoodNotes; }
            set
            {
                Tour.NeighborhoodNotes = value;
                RaisePropertyChanged("NeighborhoodNotes");
            }
        }

        public double Ambience
        {
            get { return Tour.Ambience; }
            set
            {
                Tour.Ambience = value;
                RaisePropertyChanged("Ambience");
            }
        }

        public string Currency
        {
            get
            {
                return Utils.Utility.Currency;
            }
        }

        public string AmbienceNotes
        {
            get { return Tour.AmbienceNotes; }
            set
            {
                Tour.AmbienceNotes = value;
                RaisePropertyChanged("AmbienceNotes");
            }
        }

        public bool IsFormValid
        {
            get;
            set;
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(() => SaveCheckList());
                }
                return _saveCommand;
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

        public ICommand AddRecentRenovation
        {
            get
            {
                if (_addRecentRenovationCommand == null)
                {
                    _addRecentRenovationCommand = new RelayCommand(() => AddRecentRenovationMethod());
                }
                return _addRecentRenovationCommand;
            }

        }

        public ICommand DeleteRecentRenovation
        {
            get
            {
                if (_deleteRecentRenovationCommand == null)
                {
                    _deleteRecentRenovationCommand = new RelayCommand<RecentRenov>((reno) => DeleteRecentRenovationMethod(reno));
                }
                return _deleteRecentRenovationCommand;
            }

        }

        public ICommand AddNearByCommand
        {
            get
            {
                if (_addNearByCommand == null)
                {
                    _addNearByCommand = new RelayCommand(() => AddNearBy());
                }
                return _addNearByCommand;
            }

        }

        public ICommand DeleteNearByCommand
        {
            get
            {
                if (_deleteNearByCommand == null)
                {
                    _deleteNearByCommand = new RelayCommand<NearBy>((near) => DeleteNearBy(near));
                }
                return _deleteNearByCommand;
            }

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

        public ICommand AddPictureTourCommand
        {
            get
            {
                if (_addPictureTourCommand == null)
                {
                    _addPictureTourCommand = new RelayCommand<byte[]>((data) => AddPictureTour(data));
                }
                return _addPictureTourCommand;
            }

        }



        public ICommand AddRepairCommand
        {
            get
            {
                if (_addRepairCommand == null)
                {
                    _addRepairCommand = new RelayCommand(() => AddRepair());
                }
                return _addRepairCommand;
            }

        }

        public ICommand DeletePictureTourCommand
        {
            get
            {
                if (_deletePictureTourCommand == null)
                {
                    _deletePictureTourCommand = new RelayCommand<PictureTour>((data) => DeletePictureTour(data));
                }
                return _deletePictureTourCommand;
            }

        }

        public ICommand DeleteRepairCommand
        {
            get
            {
                if (_deleteRepairCommand == null)
                {
                    _deleteRepairCommand = new RelayCommand<Repair>((data) => DeleteRepair(data));
                }
                return _deleteRepairCommand;
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

        #endregion

        #region DBMethods
        private void SaveCheckList()
        {
            IsFormValid = true;

            this.Tour.RecentRenovations = Utils.Utility.ListToString(RecentRenovations.Select(reno => reno.Name).ToList());
            DataServiceTour dsTour = new DataServiceTour();
            dsTour.UpdateTour();
            EditableObject.EndEdit();
            navigationService.GoBack();
        }

        private void Cancel()
        {
            EditableObject.CancelEdit();
            DataServiceTour dsTour = new DataServiceTour();
            dsTour.RefreshTour(this.Tour);
        }


        public class RecentRenov
        {
            public string Name { get; set; }
        }

        private void AddRecentRenovationMethod()
        {
            RecentRenovations.Add(new RecentRenov() { Name = "" });
        }

        private void DeleteRecentRenovationMethod(RecentRenov value)
        {
            RecentRenovations.Remove(value);
        }

        private void AddNearBy()
        {
            DataServiceNearBy dsNear = new DataServiceNearBy();
            NearBy nearBy = new NearBy();
            Tour.NearBy.Add(nearBy);
            nearBy.Tour = Tour;
            dsNear.addNearBy(nearBy);
            NearByList.Add(nearBy);
        }

        private void DeleteNearBy(NearBy nearBy)
        {
            DataServiceNearBy dsNear = new DataServiceNearBy();
            dsNear.DeleteNearBy(nearBy);
            NearByList.Remove(nearBy);
        }

        private void AddRepair()
        {
            Repair rep = new Repair();
            rep.Tour = this.Tour;
            this.Tour.Repair.Add(rep);
            DataServiceRepair dsRep = new DataServiceRepair();
            dsRep.addRepair(rep);
            RepairList.Add(rep);
        }

        private void AddPictureTour(byte[] e)
        {
            DataServicePictureTour dsPicTour = new DataServicePictureTour();
            PictureTour picTour = new PictureTour();
            picTour.Tour = Tour;
            Tour.Pictures.Add(picTour);
            picTour.Image = e;
            dsPicTour.addPictureTour(picTour);
            PictureTourList.Add(picTour);

        }
        private void DeleteRepair(Repair e)
        {
            DataServiceRepair dsRepair = new DataServiceRepair();
            RepairList.Remove(e);
            dsRepair.DeleteRepair(e);
        }


        private void DeletePictureTour(PictureTour e)
        {
            DataServicePictureTour dsPicTour = new DataServicePictureTour();
            PictureTourList.Remove(e);
            dsPicTour.DeletePictureTour(e);
        }

        private void PictureView(Tuple<int, List<byte[]>> tuple)
        {
            Messenger.Default.Send<Tuple<int, List<byte[]>>, PictureViewModel>(tuple);
            navigationService.NavigateTo("PictureView");
        }
        #endregion

    }
}