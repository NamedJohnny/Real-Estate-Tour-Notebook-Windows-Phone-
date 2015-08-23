using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using System.Windows.Media;

namespace RealEstateTourNotebook.Controls
{
    public partial class TileUserControl : UserControl
    {
        public TileUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty ImageSourceProperty =
         DependencyProperty.Register("ImageSource", typeof(string), typeof(TileUserControl), null);

        public string ImageSource
        {
            get { return (string)base.GetValue(ImageSourceProperty); }
            set
            {
                base.SetValue(ImageSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ImageVerticalAlignementProperty =
        DependencyProperty.Register("ImageVerticalAlignement", typeof(VerticalAlignment), typeof(TileUserControl), null);

        public VerticalAlignment ImageVerticalAlignement
        {
            get { return (VerticalAlignment)base.GetValue(ImageVerticalAlignementProperty); }
            set
            {
                base.SetValue(ImageVerticalAlignementProperty, value);
            }
        }

        public static readonly DependencyProperty TextVisibleProperty =
         DependencyProperty.Register("TextVisible", typeof(bool), typeof(TileUserControl), null);

        public bool TextVisible
        {
            get { return (bool)base.GetValue(TextVisibleProperty); }
            set
            {
                base.SetValue(TextVisibleProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty =
         DependencyProperty.Register("TextValue", typeof(string), typeof(TileUserControl), null);

        public string TextValue
        {
            get { return (string)base.GetValue(TextProperty); }
            set
            {
                base.SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty CustomColorProperty =
        DependencyProperty.Register("CustomColor", typeof(SolidColorBrush), typeof(TileUserControl), null);

        public SolidColorBrush CustomColor
        {
            get { return (SolidColorBrush)base.GetValue(CustomColorProperty); }
            set
            {
                base.SetValue(CustomColorProperty, value);
            }
        }
    }
}
