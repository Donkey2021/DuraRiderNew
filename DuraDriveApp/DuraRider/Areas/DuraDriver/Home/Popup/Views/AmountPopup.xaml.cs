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
    public partial class AmountPopup  
    {
        private string PageTitle;
        public AmountPopup(string PageTitle)
        {
            InitializeComponent();
            if (PageTitle== "TopUpPopup")
            {
                StkAmount.IsVisible = true;
                StkAddAmount.IsVisible = false;
                StkAddAmountDetails.IsVisible = false;
                StkTransactionWith.IsVisible = false;
            }
           else if (PageTitle== "RequestCashoutPopup")
            {
                StkTransactionWith.IsVisible = true;
                StkAmount.IsVisible = false;
                StkAddAmount.IsVisible = false;
                StkAddAmountDetails.IsVisible = false;               
            }
            else
            {
                //default
                StkAmount.IsVisible = true;
                StkAddAmount.IsVisible = false;
                StkAddAmountDetails.IsVisible = false;
                StkTransactionWith.IsVisible = false;
            }
           
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);            
        }

        private void TopUp_Clicked(object sender, EventArgs e)
        {
            StkAmount.IsVisible = false;           
            StkAddAmount.IsVisible = true;
            StkAddAmountDetails.IsVisible = false;
        }

        private void AddMoney_Tapped(object sender, EventArgs e)
        {
            StkAmount.IsVisible = false;
            StkAddAmount.IsVisible = false;
            StkAddAmountDetails.IsVisible = true;
        }

        private async void AddMoneyDetails_Tapped(object sender, EventArgs e)
        {
            Dismiss(null);
           // await RichNavigation.PushAsync(new GCashPage(), typeof(GCashPage));
        }

        private async void RequesttoGcash_Tapped(object sender, EventArgs e)
        {
            Dismiss(null);
            //await Navigation.ShowPopupAsync(new SuccessfullyPopup("Request to Gcash successfully"));
        }
    }
}