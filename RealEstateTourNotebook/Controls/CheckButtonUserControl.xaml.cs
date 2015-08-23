using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using RealEstateTourNotebook.ViewModel.Enum;

namespace RealEstateTourNotebook.Controls
{
    public partial class CheckButtonUserControl : UserControl
    {
        public CheckButtonUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty TourTypeProperty =
         DependencyProperty.Register("Type", typeof(TourType), typeof(CheckButtonUserControl), null);

        public TourType Type
        {
            get { return (TourType)base.GetValue(TourTypeProperty) ; }
            set
            {
                base.SetValue(TourTypeProperty, value);
            }
        }

        public static readonly DependencyProperty IsCheckedProperty =
         DependencyProperty.Register("IsChecked", typeof(bool), typeof(CheckButtonUserControl), null);

        /// <summary>
        /// Si le bouton est en mode enfoncé
        /// </summary>
        public bool IsChecked
        {
            get { return (bool)base.GetValue(IsCheckedProperty); }
            set
            {
                base.SetValue(IsCheckedProperty, value);
            }
        }

        public void SetImageTitle()
        {
            Uri uri = new Uri(Utils.Utility.ImageSourceFromEnum((TourType)Type), UriKind.Relative);
            ImageSource imgSource = new BitmapImage(uri);
            this.Image.Source = imgSource; 
            this.Title.Text = Utils.Utility.Display(this.Type);
        }

        private void CheckBoxUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetImageTitle();
        }

    }
}
