using GalaSoft.MvvmLight;
using RealEstateTourNotebook.Resources;
using RealEstateTourNotebook.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace RealEstateTourNotebook.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SettingsViewModel : ViewModelBase
    {
        private int _selectedLang;
        /// <summary>
        /// Initializes a new instance of the SettingsViewModel class.
        /// </summary>
        public SettingsViewModel()
        {
            SetLanguageList();
            SetCurrencyList();
            SetDistanceList();
            SetDimensionList();
        }

        public Version Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public ObservableCollection<Language> LanguageList
        {
            get;
            set;
        }

        public ObservableCollection<string> DimensionList
        {
            get;
            set;
        }

        public ObservableCollection<string> DistanceList
        {
            get;
            set;
        }

        public ObservableCollection<string> CurrencyList
        {
            get;
            set;
        }

        public int SelectedLang
        {
            get
            {
                return _selectedLang;
            }
            set
            {
                _selectedLang = value;
                IsolatedStorageSettings.ApplicationSettings["Language"] = LanguageList[value];
                IsolatedStorageSettings.ApplicationSettings.Save();
                MessageBox.Show(AppResources.RestartApp, "Information", MessageBoxButton.OK);
                Application.Current.Terminate();
            }
        }

        public void SetLanguageList()
        {
            LanguageList = new ObservableCollection<Language>();
            foreach (Language item in System.Enum.GetValues(typeof(Language)))
            {
                LanguageList.Add(item);
            }

            _selectedLang = LanguageList.IndexOf(LanguageList.First(x => Thread.CurrentThread.CurrentCulture.Name.Contains(x.ToString())));
        }

        public void SetCurrencyList()
        {
            CurrencyList = new ObservableCollection<string>();
        }

        public void SetDimensionList()
        {
            DimensionList = new ObservableCollection<string>();
        }

        public void SetDistanceList()
        {
            DistanceList = new ObservableCollection<string>();
        }

    }
}