
using System;
using System.Collections.Generic;
using System.Text;
using DuraRider.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DuraRider.Infrastructure
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            //this.On<iOS>().SetUseSafeArea(true);

            var safeInsets = On<iOS>().SafeAreaInsets();
           // safeInsets.Left = 20;
            safeInsets.Top = 20;
            safeInsets.Bottom = 0;
            //safeInsets.Right = 20;
            Padding = safeInsets;
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }
        protected async override void OnAppearing()
        {
            await (this.BindingContext as AppBaseViewModel).OnPageAppearing();
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
