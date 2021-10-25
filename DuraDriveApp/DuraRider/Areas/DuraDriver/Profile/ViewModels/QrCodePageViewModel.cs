using DuraRider.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Profile.ViewModels
{
   public class QrCodePageViewModel : AppBaseViewModel
    {
        public ICommand ShareCommand => new Command(async (obj) =>
        {
            //await RichNavigation.PushAsync(new MyPointsPage(), typeof(MyPointsPage));
        });
    }
}
