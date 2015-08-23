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
using RealEstateTourNotebook.Model.Tables;
using RealEstateTourNotebook.Resources;

namespace RealEstateTourNotebook.View
{
    public partial class CompareView : PhoneApplicationPage
    {
        // Constructor
        public CompareView()
        {
            InitializeComponent();
        }

        public CompareViewModel ViewModel
        {
            get
            {
                return this.DataContext as CompareViewModel;
            }
        }

        private void IconRemove_Click(object sender, EventArgs e)
        {
            if (ViewModel.TourList.Count == 1)
            {
                NavigationService.GoBack();
            }
            else
            {
                ViewModel.TourList.Remove((MainPivot.SelectedItem as Tour));
                MainPivot.ItemsSource = null;
                MainPivot.ItemsSource = ViewModel.TourList;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Remove;
            }
        }

        public void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (sender is StackPanel)
            {
                PictureTour selected = (sender as StackPanel).DataContext as PictureTour;

                List<byte[]> tempByte = new List<byte[]>();
                foreach (PictureTour tour_Loop in (MainPivot.SelectedItem as Tour).Pictures)
                    tempByte.Add(tour_Loop.Image);

                Tuple<int, List<byte[]>> tuple = new Tuple<int, List<byte[]>>((MainPivot.SelectedItem as Tour).Pictures.IndexOf(selected), tempByte);
                ViewModel.ViewPictureCommand.Execute(tuple);
            }
            else if (sender is Image)
            {
                Repair selected = (sender as Image).DataContext as Repair;

                List<byte[]> tempByte = new List<byte[]>();
                tempByte.Add(selected.Image);
                Tuple<int, List<byte[]>> tuple = new Tuple<int, List<byte[]>>(0, tempByte);
                ViewModel.ViewPictureCommand.Execute(tuple);
            }
            
        }
      
    }
}