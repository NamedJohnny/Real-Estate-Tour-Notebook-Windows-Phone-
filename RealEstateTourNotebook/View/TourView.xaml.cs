using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RealEstateTourNotebook.Resources;
using Microsoft.Phone.Tasks;
using RealEstateTourNotebook.ViewModel;
using Windows.Devices.Geolocation;
using System.Device.Location;
using RealEstateTourNotebook.Utils;

namespace RealEstateTourNotebook.View
{
    public partial class TourView : PhoneApplicationPage
    {
        // Constructor
        public TourView()
        {
            InitializeComponent();
            TiltEffect.TiltableItems.Add(typeof(Border));
            Transitions.UseTurnstileTransition(this);
        }

        private TourViewModel ViewModel
        {
            get
            {
                return this.DataContext as TourViewModel;
            }
        }

        private void IconEdit_Click(object sender, EventArgs e)
        {
            ViewModel.AddEditTourCommand.Execute(ViewModel.Tour);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Edit;
            }

            PhoneCall.TextValue = (ViewModel.Tour.OwnerName !=null ? ViewModel.Tour.OwnerName.ToUpper() :"");
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneCall_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();
            phoneCallTask.PhoneNumber = ViewModel.Tour.PhoneNumber.ToString();
            phoneCallTask.DisplayName = ViewModel.Tour.OwnerName;
            phoneCallTask.Show();
        }

        private void Checklist_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.CheckListCommand.Execute(ViewModel.Tour);
        }

        private async void MapIt_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                BingMapsDirectionsTask bingMap = new BingMapsDirectionsTask();
                // Get my current location.
                Geolocator myGeolocator = new Geolocator();
                Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
                Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
                bingMap.Start = new LabeledMapLocation();
                bingMap.End = new LabeledMapLocation();
                bingMap.Start.Label = AppResources.MyPosition;
                bingMap.Start.Location = new GeoCoordinate(myGeocoordinate.Point.Position.Latitude, myGeocoordinate.Point.Position.Longitude);
                bingMap.End.Location = ViewModel.Tour.Coordinate;
                bingMap.End.Label = "Destination";
                bingMap.Show();
            }
            catch (Exception)
            {
                // the app does not have the right capability or the location master switch is off 
                MessageBox.Show(AppResources.LocationError, AppResources.Warning, MessageBoxButton.OK);
            }
        }

    }
}