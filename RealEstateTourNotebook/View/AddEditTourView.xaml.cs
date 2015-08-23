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
using RealEstateTourNotebook.ViewModel;
using RealEstateTourNotebook.Model;
using RealEstateTourNotebook.Controls;
using System.Windows.Media;
using RealEstateTourNotebook.ViewModel.Enum;
using Microsoft.Phone.Tasks;
using RealEstateTourNotebook.Utils;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;

namespace RealEstateTourNotebook.View
{
    public partial class AddEditTourView : PhoneApplicationPage
    {
        // Constructor
        public AddEditTourView()
        {
            InitializeComponent();
            Transitions.UseTurnstileTransition(this);
            Utils.Utility.QueryComplete += Utility_QueryComplete;
        }

        private AddEditTourViewModel ViewModel
        {
            get
            {
                return this.DataContext as AddEditTourViewModel;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back && !ViewModel.IsFormValid)
            {
                ViewModel.CancelCommand.Execute(null);
            }
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Utils.Utility.AddToMap(myMap, new GeoCoordinate(ViewModel.Tour.Latitude, ViewModel.Tour.Longitude),NameTextBox,true);
            base.OnNavigatedTo(e);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Save;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Cancel;
            }

            SearchVisualTree(this.ListBox, ViewModel.Tour.Type);
            ViewModel.Loading = false;
        }

        private void IconCancel_Click(object sender, EventArgs e)
        {
            ViewModel.GoBackCommand.Execute(null);
        }

        private void IconSave_Click(object sender, EventArgs e)
        {
            this.Focus();
            // wait till the next UI thread tick so that the binding gets updated
            Dispatcher.BeginInvoke(() =>
            {
                Utils.Utility.AddToMap(myMap, new GeoCoordinate(ViewModel.Tour.Latitude, ViewModel.Tour.Longitude), NameTextBox, true);
                ViewModel.Tour.AddressName = NameTextBox.Text;
                ViewModel.AddEditTourCommand.Execute(null);
                NavigationService.RemoveBackEntry();
            });
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchVisualTree(sender as DependencyObject, (TourType)e.AddedItems[0]);
        }

        private void SearchVisualTree(DependencyObject targetElement, TourType type)
        {
            var count = VisualTreeHelper.GetChildrenCount(targetElement);
            if (count == 0)
                return;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(targetElement, i);
                if (child is CheckButtonUserControl)
                {
                    CheckButtonUserControl targetItem = (CheckButtonUserControl)child;
                    targetItem.IsChecked = targetItem.Type == type;
                }
                else
                {
                    SearchVisualTree(child, type);
                }
            }
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.PixelHeight = 480;
            photoChooserTask.PixelWidth = 700;
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
                (this.DataContext as AddEditTourViewModel).MainImage = Utils.Utility.ReadFully(e.ChosenPhoto);
        }

        private void myMap_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            GeoCoordinate coord = myMap.ConvertViewportPointToGeoCoordinate(e.GetPosition(myMap));
            Utils.Utility.AddToMap(myMap, coord, NameTextBox, true);
        }

        private void PhoneTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[0-9]");
        }

        private void btn_place_Click(object sender, RoutedEventArgs e)
        {
            this.Focus();
            Utils.Utility.AddToMap(myMap, null, NameTextBox, false);
        }


        void Utility_QueryComplete(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            try
            {
                if (!String.IsNullOrEmpty(NameTextBox.Text) && e.Result.Count == 0)
                {
                    MessageBox.Show(string.Format(AppResources.InvalideSearch, NameTextBox.Text), AppResources.Warning, MessageBoxButton.OK);
                    return;
                }

                GeoCoordinate coord = new GeoCoordinate();
                foreach (var item in e.Result)
                {
                    coord = item.GeoCoordinate;
                    NameTextBox.Text = Utils.Utility.getCompleteAddress(item.Information.Address);
                }

                MapLayer pinLayout = new MapLayer();
                Pushpin MyPushpin = new Pushpin();
                MapOverlay pinOverlay = new MapOverlay();
                if (myMap.Layers.Count > 0)
                    myMap.Layers.RemoveAt(myMap.Layers.Count - 1);

                myMap.Layers.Add(pinLayout);

                MyPushpin.GeoCoordinate = coord;

                pinOverlay.Content = MyPushpin;
                pinOverlay.GeoCoordinate = MyPushpin.GeoCoordinate;
                pinOverlay.PositionOrigin = new Point(0, 1);
                pinLayout.Add(pinOverlay);
                MyPushpin.Content = AppResources.MyTour;

                ViewModel.Tour.Latitude = coord.Latitude;
                ViewModel.Tour.Longitude = coord.Longitude;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}