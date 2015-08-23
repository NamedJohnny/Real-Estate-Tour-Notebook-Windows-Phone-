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
using RealEstateTourNotebook.Utils;

namespace RealEstateTourNotebook.View
{
    public partial class FilterByView : PhoneApplicationPage
    {
        // Constructor
        public FilterByView()
        {
            InitializeComponent();
            Transitions.UseSlideUpDownTransition(this);
        }

        private FilterByViewModel ViewModel
        {
            get
            {
                return this.DataContext as FilterByViewModel;
            }
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //twice
            if (e.NavigationMode != NavigationMode.Back)
            {
                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedFrom(e);
        }

        private void IconCheck_Click(object sender, EventArgs e)
        {
            ViewModel.ListFilterCommand.Execute(null);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.Buttons != null)
            {
                (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.ApplyFilter;
            }
        }
    }
}