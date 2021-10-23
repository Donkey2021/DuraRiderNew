using System;
using DuraRider.Core.Helpers.Enums;
using FreshMvvm;
using DuraRider.Bootstrap;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuraRider
{
    public partial class App : Application
    {
        public static AppState State = AppState.Undefinded;
        public static double MainPageScreenWidth { get; set; } //screen width
        public static double MainPageScreenHeight { get; set; }//screen height
        public static ViewModelLocator Locator { get; set; }
        public INavigationService _navigationService;
        public static IFreshIOC Container => FreshIOC.Container;

        public App()
        {
            InitializeComponent();
            VersionTracking.Track();
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.Application.SetWindowSoftInputModeAdjust(this, Xamarin.Forms.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust.Resize);
            BootstrapConfig.RegisterViewModel();
            BootstrapConfig.RegisterService();
            //GoogleMapsApiService.Initialize(Tech.Med.Core.Helpers.Constants.GoogleMapsApiKey);
            Locator = new ViewModelLocator();
            _navigationService = Container.Resolve<INavigationService>();
            _navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
