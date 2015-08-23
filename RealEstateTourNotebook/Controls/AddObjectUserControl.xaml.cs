using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RealEstateTourNotebook.Controls
{
    public partial class AddObjectUserControl : UserControl
    {
        public AddObjectUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty TextAddProperty =
      DependencyProperty.Register("TextAdd", typeof(string), typeof(AddObjectUserControl), null);

        public string TextAdd
        {
            get { return (string)base.GetValue(TextAddProperty); }
            set
            {
                base.SetValue(TextAddProperty, value);
            }
        }

    }
}
