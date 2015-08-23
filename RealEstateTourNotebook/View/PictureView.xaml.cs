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

namespace RealEstateTourNotebook.View
{
    public partial class PictureView : PhoneApplicationPage
    {
        public PictureView()
        {
            InitializeComponent();
        }

        public PictureViewModel ViewModel
        {
            get
            {
                return this.DataContext as PictureViewModel;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            myImage.Picture = ViewModel.SelectedPicture;
            base.OnNavigatedTo(e);
        }

        private void GestureListener_Flick(object sender, FlickGestureEventArgs e)
        {
            if (!myImage.IsOrigin)
                return;

            // User swap towards gauche
            if (e.HorizontalVelocity > 0)
            {
                // Load the next image 
                ViewModel.SelectedIndex -= 1;
            }

            // User swap towards droit
            if (e.HorizontalVelocity < 0)
            {
                // Load the previous image
                ViewModel.SelectedIndex += 1;
            }

            myImage.Picture = ViewModel.SelectedPicture;
        }

        private void IconExit_Click(object sender, EventArgs e)
        {
            ViewModel.CancelCommand.Execute(null);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Cancel;
            }
        }
    }
}