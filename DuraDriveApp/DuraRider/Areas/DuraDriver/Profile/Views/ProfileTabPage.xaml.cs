using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuraRider.Areas.DuraDriver.Profile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileTabPage : ContentPage
    {
        public ProfileTabPage()
        {
            InitializeComponent(); 
        }

        private async void btnTake1_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("no upload", "picking a photo is not supported", "ok");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;

            photo_placeholder.Source = ImageSource.FromStream(() => file.GetStream());
        }
    }
}