using System;
using System.Collections.Generic;
using DuraRider.Helpers;
using Rg.Plugins.Popup.Extensions;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuraRider.Areas.DuraDriver.Home.Popup.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DuraExpressPopUpPage : ContentView
    {
        public DuraExpressPopUpPage()
        {
            InitializeComponent();
        }
        private void Decline_Clicked(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new DeclinePopUp("DeclinePopup"));
        }

        private async void Accept_Clicked(object sender, EventArgs e)
        {
            //Dismiss(null);
            //await RichNavigation.PopAsync();
            await Navigation.PopPopupAsync();
            await Navigation.PushPopupAsync(new TransparentModel(new ReachLocationPopUp())); 
        }
    }
}
