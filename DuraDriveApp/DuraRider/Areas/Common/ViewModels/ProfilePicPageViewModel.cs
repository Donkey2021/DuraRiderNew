using System;
using System.Net.Http;
using System.Threading.Tasks;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Helpers;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DuraRider.Areas.Common.ViewModels
{
    public class ProfilePicPageViewModel : AppBaseViewModel
    {
        byte[] imageByteArray;
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand ProfilePicTap { get; set; }
        public IAsyncCommand SubmitCommand { get; set; }
        private RegistrationData _registrationData;
        public RegistrationData RegistrationData
        {
            get { return _registrationData; }
            set
            {
                _registrationData = value;
                OnPropertyChanged(nameof(RegistrationData));
            }
        }
        private ImageSource _profile_image;
        public ImageSource Profile_Image
        {
            get { return _profile_image; }
            set
            {
                _profile_image = value;
                OnPropertyChanged(nameof(Profile_Image));
            }
        }
        public ProfilePicPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            SubmitCommand = new AsyncCommand(SubmitCommandExecute, allowsMultipleExecutions: false);
            ProfilePicTap = new AsyncCommand(ProfilePicTapExecute, allowsMultipleExecutions: false);
            RegistrationData = App.Locator.GCashAccountDetailsPage.RegistrationData;

            Profile_Image = "photo_placeholder";
        }

        private async Task SubmitCommandExecute()
        {
            if (_navigationService.GetCurrentPageViewModel() != typeof(DuraBageServicePageViewModel))
            {
                await _navigationService.NavigateToAsync<DuraBageServicePageViewModel>();
                await App.Locator.DuraBageServicePage.InitilizeData();
            }
        }

        private async Task ProfilePicTapExecute()
        {
            await PickImage();
        }
        private async Task PickImage()
        {
            var res = await ShowCameraPopup();
            if (res != null)
            {
                if (res.Item1 == ProfilePicSelectionType.Camera)
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        ShowAlert("No Camera", ":( No camera avaialble.", "OK");
                        return;
                    }
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                        Directory = "Dura",
                        CompressionQuality = 92,
                        Name = "test.jpg"
                    });

                    await SetImageAndUpload(file);
                }
                else if (res.Item1 == ProfilePicSelectionType.Gallery)
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        ShowAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return;
                    }
                    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                        CompressionQuality = 82
                    });
                    await SetImageAndUpload(file);
                }
            }
        }

        internal Task InitilizeData()
        {
            throw new NotImplementedException();
        }

        private async Task SetImageAndUpload(MediaFile file)
        {
            if (file != null)
            {
                Profile_Image = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                imageByteArray = ImageHelper.ReadToEnd(file.GetStream());
                await UploadProfilePic();
            }
            else
            {
                ShowToast("error on uploading image,try again");
                Profile_Image = "photo_placeholder";
            }
        }

        private async Task UploadProfilePic()
        {
            if (imageByteArray == null)
            {
                ShowToast("Please select the profile image");
                return;
            }
            if (CheckConnection())
            {
                ShowLoading();
                try
                {
                    var currentLoc = await LocationHelpers.getCurrentPosition();
                    if (currentLoc == null)
                    {
                        ShowToast("Current location not found, try again");
                        return;
                    }

                    var driverid = RegistrationData.driverid == 0 ? 0 : RegistrationData.driverid;
                    var latitude = currentLoc.Latitude == 0 ? 0 : currentLoc.Latitude;
                    var longitude = currentLoc.Longitude == 0 ? 0 : RegistrationData.longitude;

                    var form = new MultipartFormDataContent();
                    form.Add(new ByteArrayContent(imageByteArray), "profilephoto_url", $"profilephoto_url_image.jpg");
                    form.Add(new StringContent(Convert.ToString(latitude)), "latitude");
                    form.Add(new StringContent(Convert.ToString(longitude)), "longitude");
                    form.Add(new StringContent(Convert.ToString(driverid)), "driver_id");

                    var result = await TryWithErrorAsync(_authenticationService.SaveProfilePic(form), true, false);
                    if (result?.ResultType == ResultType.Ok && result.Data.status == 200)
                    {
                        ShowToast("Profile picture added successfully.");
                    }
                    else
                    {
                        ShowToast("Error in adding profile pic");
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    HideLoading();
                }
            }
            else
                ShowToast(CommonMessages.NoInternet);
        }
    }
}

