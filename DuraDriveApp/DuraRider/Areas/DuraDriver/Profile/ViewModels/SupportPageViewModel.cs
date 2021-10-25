using DuraRider.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Profile.ViewModels
{
   public class SupportPageViewModel : AppBaseViewModel
    {
        public ICommand ChatSupportCommand => new Command(async (obj) =>
        {
            //await RichNavigation.PushAsync(new ChatSupportPage(), typeof(ChatSupportPage));
        });
    }
}
