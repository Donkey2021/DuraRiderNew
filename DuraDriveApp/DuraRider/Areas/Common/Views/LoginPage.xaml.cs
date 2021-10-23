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
    }
}
