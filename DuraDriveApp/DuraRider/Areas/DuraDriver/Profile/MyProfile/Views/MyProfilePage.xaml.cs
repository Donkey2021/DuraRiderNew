using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuraRider.Areas.DuraDriver.Profile.MyProfile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
        }
        //private async void btnTake1_Clicked(object sender, EventArgs e)
        //{
        //    if (!CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        await DisplayAlert("no upload", "picking a photo is not supported", "ok");
        //        return;
        //    }

        //    var file = await CrossMedia.Current.PickPhotoAsync();
        //    if (file == null)
        //        return;

        //    photo_placeholder.Source = ImageSource.FromStream(() => file.GetStream());
        //} 
    }
}