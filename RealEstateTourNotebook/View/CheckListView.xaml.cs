using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RealEstateTourNotebook.ViewModel;
using RealEstateTourNotebook.Resources;
using System.Windows.Media;
using GalaSoft.MvvmLight.Messaging;
using RealEstateTourNotebook.Controls;
using RealEstateTourNotebook.Model.Tables;
using Microsoft.Phone.Tasks;

namespace RealEstateTourNotebook.View
{
    public partial class CheckListView : PhoneApplicationPage
    {
        // Constructor
        public CheckListView()
        {
            InitializeComponent();
            RatingAmbience.Value = ViewModel.Ambience;
            RatingNeighbor.Value = ViewModel.NeighborhoodQuality;
        }

        public CheckListViewModel ViewModel
        {
            get
            {
                return this.DataContext as CheckListViewModel;
            }
        }

        private void Rating_ValueChanged(object sender, EventArgs e)
        {
            ViewModel.NeighborhoodQuality = (sender as Rating).Value;
        }

        private void RatingAmbience_ValueChanged(object sender, EventArgs e)
        {
            ViewModel.Ambience = (sender as Rating).Value;
        }

        private void IconSave_Click(object sender, EventArgs e)
        {
            ViewModel.SaveCommand.Execute(null);
        }

        private void IconCancel_Click(object sender, EventArgs e)
        {
            ViewModel.GoBackCommand.Execute(null);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Save;
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Cancel;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back  && !ViewModel.IsFormValid)
            {
                ViewModel.CancelCommand.Execute(null);
            }
            base.OnNavigatedFrom(e);
        }

        private void PhoneTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[0-9]");
        }

        private void YearTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = new SolidColorBrush(Colors.Black);
        }

        private void RepairTodo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.AddRepairCommand.Execute(null);
        }

        private void AddObjectUserControl_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.AddRecentRenovation.Execute(null);
        }

        private void NearBy_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.AddNearByCommand.Execute(null);
        }

        private void DelRecentReno_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.DeleteRecentRenovation.Execute((sender as Image).DataContext as RealEstateTourNotebook.ViewModel.CheckListViewModel.RecentRenov);
        }

        private void DelNearBy_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.DeleteNearByCommand.Execute((sender as Image).DataContext as NearBy);
        }

        private void TileUserControl_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                byte[] myArray = Utils.Utility.ReadFully(e.ChosenPhoto);
                ViewModel.AddPictureTourCommand.Execute(myArray);
            }

            PhotoHubLLS.ItemsSource = ViewModel.PictureTourList;
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PictureTour selected = (sender as StackPanel).DataContext as PictureTour;

            List<byte[]> tempByte = new List<byte[]>();
            foreach (PictureTour tour_Loop in ViewModel.PictureTourList)
                tempByte.Add(tour_Loop.Image);

            Tuple<int, List<byte[]>> tuple = new Tuple<int, List<byte[]>>(ViewModel.PictureTourList.IndexOf(selected), tempByte);
            ViewModel.ViewPictureCommand.Execute(tuple);
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var menu = (ContextMenu)sender;
            var owner = (FrameworkElement)menu.Owner;
            if (owner.DataContext != menu.DataContext)
                menu.DataContext = owner.DataContext;
        }

        private void DeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && ((sender as MenuItem).DataContext is PictureTour))
            {
                ViewModel.DeletePictureTourCommand.Execute((sender as MenuItem).DataContext as PictureTour);
            }
        }


        private Repair _temp = null;

        void photoChooserTask2_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                byte[] myArray = Utils.Utility.ReadFully(e.ChosenPhoto);
                _temp.Image = myArray;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _temp = (sender as Button).DataContext as Repair;
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask2_Completed;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.Show();
        }

        private void RepairImageTap_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Repair selected = (sender as Image).DataContext as Repair;

            List<byte[]> tempByte = new List<byte[]>();
            tempByte.Add(selected.Image);
            Tuple<int, List<byte[]>> tuple = new Tuple<int, List<byte[]>>(0, tempByte);
            ViewModel.ViewPictureCommand.Execute(tuple);
        }

        private void DelRepair_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Repair img = (sender as Image).DataContext as Repair;
            ViewModel.DeleteRepairCommand.Execute(img);
        }

        private void DeletePhotoRepair_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && ((sender as MenuItem).DataContext is Repair))
            {
                ((sender as MenuItem).DataContext as Repair).Image = null;
            }
        }

    }
}