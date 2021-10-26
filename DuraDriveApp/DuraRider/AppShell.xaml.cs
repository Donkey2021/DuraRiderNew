using System;
using System.Collections.Generic; 
using DuraRider.Areas.DuraDriver.Home.Views;
using DuraRider.Areas.DuraDriver.Orders.Views;
using DuraRider.Areas.DuraDriver.Profile.Views;
using DuraRider.Areas.DuraDriver.Wallet.Views;
using DuraRider.Interfaces;
using Xamarin.Forms;

namespace DuraRider
{
    public partial class AppShell : Shell, IRootView, IMainView, IShelll
    {
        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }
        public AppShell()
        {
            RegisterRoutes();
            InitializeComponent();
        }
        void RegisterRoutes()
        {
            routes.Add("HomePage", typeof(HomePage)); 
            routes.Add("OrderPage", typeof(OrderPage)); 
            routes.Add("WalletPage", typeof(WalletPage)); 
            routes.Add("ProfilePage", typeof(ProfilePage)); 

            //routes.Add(AppConst.HomeNavPage, typeof(HomePage));
            //routes.Add(AppConst.BrowserPageNavUrl, typeof(BrowseAdventurePage));
            //routes.Add(AppConst.SignUpPageNavUrl, typeof(SignUpPage));
            //routes.Add(AppConst.SearchResultPageNavUrl, typeof(SearchResultPage));
            //routes.Add(AppConst.ChangeEmailPageNavUrl, typeof(ChangeEmailPage));
            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}
