﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using RealEstateTourNotebook.Utils;

namespace RealEstateTourNotebook.View
{
    public partial class SettingsView : PhoneApplicationPage
    {
        // Constructor
        public SettingsView()
        {
            InitializeComponent();
            Transitions.UseTurnstileTransition(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        
    }
}