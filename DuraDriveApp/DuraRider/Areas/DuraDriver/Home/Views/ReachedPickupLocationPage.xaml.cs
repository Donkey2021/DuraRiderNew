using DuraRider.Areas.DuraDriver.Home.Popup.Views;
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
    public partial class ReachedPickupLocationPage 
    { 
        public ReachedPickupLocationPage()
        { 
            InitializeComponent(); 
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            //Navigation.ShowPopup(new DeclinePopUp("CancelPopup")); 
        }

        private void DeliveryNotDone_Clicked(object sender, EventArgs e)
        {
            //Navigation.ShowPopup(new DeclinePopUp("RejectPopup"));
        }

        private void CollectPayment_Clicked(object sender, EventArgs e)
        { 
            //Navigation.ShowPopup(new PaymentCollectedPopup());
        }
    }
}