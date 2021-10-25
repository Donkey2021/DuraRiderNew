using DuraRider.Areas.DuraDriver.Home.Popup.Views;
using DuraRider.Helpers;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuraRider.Areas.DuraDriver.Home.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            //Navigation.PushPopupAsync(new TransparentModel(new DuraExpressPopUpPage()));
        }
        public HomePage(int SelectedTab)
        {
            InitializeComponent(); 
            if (SelectedTab == 1)
            {
                MyTabView.SelectedIndex = 1;
            }
            else if (SelectedTab == 2)
            {
                MyTabView.SelectedIndex = 2;
            }
            else if (SelectedTab == 3)
            {
                MyTabView.SelectedIndex = 3;
            }
            else
            {
                MyTabView.SelectedIndex = 0;
                Navigation.PushPopupAsync(new TransparentModel(new DuraExpressPopUpPage()));
                //Navigation.ShowPopup(new DuraExpressPopUp());
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            this.DisplayToastAsync("Toast");
        }

        //private async void TopUp_Clicked(object sender, EventArgs e)
        //{
        //  //  await Navigation.ShowPopupAsync(new AmountPopup("TopUpPopup"));
        //}

        //private async void RequestCashout_Clicked(object sender, EventArgs e)
        //{
        //  //  await Navigation.ShowPopupAsync(new AmountPopup("RequestCashoutPopup"));
        //}
    }
}