/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:RealEstateTourNotebook.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using RealEstateTourNotebook.Model;
using System;

namespace RealEstateTourNotebook.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ListTourViewModel>(true);
            SimpleIoc.Default.Register<AddEditTourViewModel>(true);
            SimpleIoc.Default.Register<CheckListViewModel>(true);
            SimpleIoc.Default.Register<TourViewModel>(true);
            SimpleIoc.Default.Register<FilterByViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<PictureViewModel>(true);
            SimpleIoc.Default.Register<CompareViewModel>(true);
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("MainPage", new Uri("/MainPage.xaml", UriKind.Relative));
            navigationService.Configure("NewTour", new Uri("/View/AddEditTourView.xaml", UriKind.Relative));
            navigationService.Configure("ListTour", new Uri("/View/ListTourView.xaml", UriKind.Relative));
            navigationService.Configure("CheckList", new Uri("/View/CheckListView.xaml", UriKind.Relative));
            navigationService.Configure("Settings", new Uri("/View/SettingsView.xaml", UriKind.Relative));
            navigationService.Configure("Tour", new Uri("/View/TourView.xaml", UriKind.Relative));
            navigationService.Configure("Filter", new Uri("/View/FilterByView.xaml", UriKind.Relative));
            navigationService.Configure("PictureView", new Uri("/View/PictureView.xaml", UriKind.Relative));
            navigationService.Configure("Compare", new Uri("/View/CompareView.xaml", UriKind.Relative));
            return navigationService;
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ListTourViewModel ListTour
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ListTourViewModel>();
            }
        }

        public AddEditTourViewModel NewTour
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddEditTourViewModel>();
            }
        }

        public CheckListViewModel CheckList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CheckListViewModel>();
            }
        }

        public TourViewModel TourView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TourViewModel>();
            }
        }

        public FilterByViewModel FilterBy
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FilterByViewModel>();
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public PictureViewModel PictureView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PictureViewModel>();
            }
        }

        public CompareViewModel CompareView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CompareViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}