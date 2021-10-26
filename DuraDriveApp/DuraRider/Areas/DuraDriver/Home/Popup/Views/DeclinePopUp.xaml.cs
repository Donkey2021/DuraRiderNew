using DuraRider.Areas.Common.PopupView.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuraRider.Areas.DuraDriver.Home.Popup.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeclinePopUp
    {
        public DeclinePopUp(string Title)
        {
            InitializeComponent();
            DeclineFrm.IsVisible = false;
            CancleFrm.IsVisible = false;
            RejectFrm.IsVisible = false;
            if (Title== "DeclinePopup")
            {
                DeclineFrm.IsVisible = true;
                CancleFrm.IsVisible = false;
                RejectFrm.IsVisible = false;
            }
           else if(Title== "CancelPopup")
            {
                CancleFrm.IsVisible = true;
                DeclineFrm.IsVisible = false;
                RejectFrm.IsVisible = false;               
            }
           else if(Title== "RejectPopup")
            {
                RejectFrm.IsVisible = true;
                CancleFrm.IsVisible = false;
                DeclineFrm.IsVisible = false;
            }
            else
            {
                DeclineFrm.IsVisible = true;
                CancleFrm.IsVisible = false;
                RejectFrm.IsVisible = false;
            }
        }

        private void NoDiscard_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            string JobTitle = "Job Declined";
            Navigation.ShowPopup(new SuccessfullyPopup(JobTitle));
            Dismiss(null);
        } 
        private async void YesCancel_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
            // await RichNavigation.PushAsync(new HomePage(0), typeof(HomeXctTab));
            //await _navigationService.NavigateToAsync<AppShellViewModel>();
            //await App.Locator.HomePage.InitilizeData();
        }

        private async void YesReject_Clicked(object sender, EventArgs e)
        {
            string JobTitle = "Delivery rejected";
            Navigation.ShowPopup(new SuccessfullyPopup(JobTitle));
            Dismiss(null);
            await Task.Delay(500);
           // await RichNavigation.PushAsync(new HomeXctTab(0), typeof(HomeXctTab));
        }
    }
}