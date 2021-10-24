using System;
using System.Collections.Generic;
using DuraRider.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuraRider.Areas.Common.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : IRootView, IMainView
    {
        public LoginPage()
        {
            InitializeComponent();
            App.Locator.LoginPage.InitilizeData();
        }
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            pickerCountryCode.Focus();
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");

                if (result)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow(); // Or anything else
                }
            });
            return true;
        }
    }
}
