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
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using RealEstateTourNotebook.ViewModel.Enum;
using GalaSoft.MvvmLight.Messaging;
using RealEstateTourNotebook.Utils;

namespace RealEstateTourNotebook.View
{
    public partial class ListTourView : PhoneApplicationPage
    {
        // Constructor
        public ListTourView()
        {
            InitializeComponent();
            Transitions.UseTurnstileTransition(this);
        }

        private ListTourViewModel ViewModel
        {
            get
            {
                return this.DataContext as ListTourViewModel;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var appbar = this.Resources["AppBarList"] as ApplicationBar;
            if (appbar.Buttons != null)
            {
                (appbar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.NewTour;
                (appbar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Filtrer;
                (appbar.Buttons[2] as ApplicationBarIconButton).Text = AppResources.SelectTour;
            }

            var appbarSelect = this.Resources["AppBarListSelect"] as ApplicationBar;
            if (appbarSelect.Buttons != null)
            {
                (appbarSelect.Buttons[0] as ApplicationBarIconButton).Text = AppResources.Delete;
                (appbarSelect.Buttons[1] as ApplicationBarIconButton).Text = AppResources.Compare;
            }

            ApplicationBar = this.Resources["AppBarList"] as ApplicationBar;
            (ApplicationBar.Buttons[2] as ApplicationBarIconButton).IsEnabled = (tourLLS.ItemsSource.Count > 0);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                ViewModel.SetTourList();
                try
                {
                    tourLLS.ItemsSource = ViewModel.GroupedTours;
                }
                catch (Exception ) { }
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back && tourLLS.IsSelectionEnabled)
            {
                tourLLS.IsSelectionEnabled = false;
                e.Cancel = true;
            }
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && ((sender as MenuItem).DataContext is Tour))
            {
                Tour tourSelected = ((sender as MenuItem).DataContext as Tour);
                switch (menuItem.Name)
                {
                    case "EditTour":
                        ViewModel.AddEditTourCommand.Execute(tourSelected);
                        break;
                    case "DeleteTour":
                        ViewModel.DeleteTourCommand.Execute(tourSelected);
                        tourLLS.ItemsSource = ViewModel.GroupedTours;
                        break;
                    case "CheckListTour":
                        ViewModel.CheckListTourCommand.Execute(tourSelected);
                        break;
                }
            }
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            tourLLS.IsSelectionEnabled = false;
            if (!tourLLS.IsSelectionEnabled)
            {
                ViewModel.SelectedItem = (sender as FrameworkElement).DataContext as Tour;
            }
        }

        private void TourLLS_IsSelectionEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tourLLS.IsSelectionEnabled)
                ApplicationBar = this.Resources["AppBarListSelect"] as ApplicationBar;
            else
                ApplicationBar = this.Resources["AppBarList"] as ApplicationBar;

            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = !tourLLS.IsSelectionEnabled;
            (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = !tourLLS.IsSelectionEnabled;
        }

        private void TourLLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tourLLS.IsSelectionEnabled)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = (tourLLS.SelectedItems.Count > 0);
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = (tourLLS.SelectedItems.Count > 0);
            }
        }

        private void IconAdd_Click(object sender, EventArgs e)
        {
            ViewModel.AddEditTourCommand.Execute(null);
        }

        private void IconFilter_Click(object sender, EventArgs e)
        {
            ViewModel.FilterCommand.Execute(null);
        }

        private void IconSelect_Click(object sender, EventArgs e)
        {
            tourLLS.IsSelectionEnabled = !tourLLS.IsSelectionEnabled;
        }

        private void IconDelete_Click(object sender, EventArgs e)
        {
            List<Tour> tourList = new List<Tour>();
            foreach (Tour tour_Loop in tourLLS.SelectedItems)
                tourList.Add(tour_Loop);
            ViewModel.DeleteTourCommand.Execute(tourList);
            tourLLS.ItemsSource = ViewModel.GroupedTours;
        }

        private void ContextMenuTour_Opened(object sender, RoutedEventArgs e)
        {
            var menu = (ContextMenu)sender;
            var owner = (FrameworkElement)menu.Owner;
            if (owner.DataContext != menu.DataContext)
                menu.DataContext = owner.DataContext;
        }

        private void IconCompare_Click(object sender, EventArgs e)
        {
            List<Tour> tourList = new List<Tour>();
            foreach (Tour tour_Loop in tourLLS.SelectedItems)
                tourList.Add(tour_Loop);
            ViewModel.CompareCommand.Execute(tourList);
        }



    }
}