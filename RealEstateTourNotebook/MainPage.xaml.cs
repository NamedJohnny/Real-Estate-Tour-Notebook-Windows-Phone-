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
using RealEstateTourNotebook.Utils;
using RealEstateTourNotebook.Controls;
using RealEstateTourNotebook.ViewModel;

namespace RealEstateTourNotebook
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Transitions.UseTurnstileTransition(this);
            TiltEffect.TiltableItems.Add(typeof(TileUserControl));
            TiltEffect.TiltableItems.Add(typeof(AddObjectUserControl));
        }

        private MainViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainViewModel;
            }
        }

        private void ListVisit_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.ListTourCommand.Execute(null);
        }

        private void Settings_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.SettingsCommand.Execute(null);
        }

        private void NewVisit_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModel.AddEditTourCommand.Execute(null);
        }

        
    }
}