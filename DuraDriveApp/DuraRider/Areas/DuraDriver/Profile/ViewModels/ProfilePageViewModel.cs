using DuraRider.Areas.DuraDriver.Home.HomeModels;
using DuraRider.Core.Helpers;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace DuraRider.Areas.DuraDriver.Profile.ViewModels
{
    public class ProfilePageViewModel : AppBaseViewModel
    {

        #region localVariable
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;

        public IAsyncCommand ViewProfileCommand { get; set; }
        public IAsyncCommand<Object> ProfileTabCommand { get; set; }
        #endregion

        public ProfilePageViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
            ViewProfileCommand = new AsyncCommand(ViewProfileCommandExecute);
            ProfileTabCommand = new AsyncCommand<Object>(ProfileTabCommandExecute);
        }
        #region Method 
        private async Task ViewProfileCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(ProfileTabPageViewModel))
                {
                    await _navigationService.NavigateToAsync<ProfileTabPageViewModel>();
                    //await App.Locator.SignUpPage.InitilizeData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        //Profile
        public ObservableCollection<ProfileModel> ProfileItems { get; set; } = new ObservableCollection<ProfileModel>()
        {
            new ProfileModel {id=1,Images="qr_code", TitleName = "My QR Code" },
            new ProfileModel {id=2,Images="Share", TitleName = "Refer a Rider"},
            new ProfileModel {id=3,Images="my_ratings", TitleName = "My Ratings"},
            new ProfileModel {id=4,Images="support", TitleName = "Support"},
            new ProfileModel {id=5,Images="help_center", TitleName = "Help Center" },
            new ProfileModel {id=6,Images="Aboutus", TitleName = "About Us" },
            new ProfileModel {id=7,Images="privacy_policy", TitleName = "Privacy Policy" },
            new ProfileModel {id=8,Images="TermsCondition", TitleName = "Terms & Conditions" },
        }; 

        //public ICommand LogOutCommand => new Xamarin.Forms.Command(async (obj) =>
        //{
        //    var myValue = SecureStorageCustom.Remove("token");
        //    if (myValue)
        //    {
        //        App.Current.MainPage = new MaterialNavigationPage(new LoginPage());
        //    }
        //    Settings.IsWalkthroughCompleted = false;
        //});

        private async Task ProfileTabCommandExecute(object obj)
        {
            {
                var ProfileMdl = obj as ProfileModel;
                switch (ProfileMdl.id)
                {
                    case 1:
                        if (_navigationService.GetCurrentPageViewModel() != typeof(QrCodePageViewModel))
                        {
                            await _navigationService.NavigateToAsync<QrCodePageViewModel>();
                            //await App.Locator.SignUpPage.InitilizeData();                            
                        }
                        break;
                    case 2:
                        if (_navigationService.GetCurrentPageViewModel() != typeof(ReferRiderPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<ReferRiderPageViewModel>();
                            //await App.Locator.SignUpPage.InitilizeData();                            
                        }
                        break;
                    case 3:
                        if (_navigationService.GetCurrentPageViewModel() != typeof(MyRatingPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<MyRatingPageViewModel>();
                            //await App.Locator.SignUpPage.InitilizeData();                            
                        }
                        break;
                    case 4:
                        if (_navigationService.GetCurrentPageViewModel() != typeof(SupportPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<SupportPageViewModel>();
                            //await App.Locator.SignUpPage.InitilizeData();                            
                        }
                        break;
                    case 5:
                        if (_navigationService.GetCurrentPageViewModel() != typeof(HelpCenterPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<HelpCenterPageViewModel>();
                            //await App.Locator.SignUpPage.InitilizeData();                            
                        }
                        break;
                    case 6:
                        if (_navigationService.GetCurrentPageViewModel() != typeof(AboutusPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<AboutusPageViewModel>();
                            //await App.Locator.SignUpPage.InitilizeData();                            
                        }
                        break;
                    case 7:
                        if (_navigationService.GetCurrentPageViewModel() != typeof(PrivacyPolicyPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<PrivacyPolicyPageViewModel>();
                            //await App.Locator.SignUpPage.InitilizeData();                            
                        }
                        break;
                    case 8:
                        if (_navigationService.GetCurrentPageViewModel() != typeof(TermsConditionPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<TermsConditionPageViewModel>();
                            //await App.Locator.SignUpPage.InitilizeData();                            
                        }
                        break;
                    case 9:
                        await _navigationService.NavigateToAsync<TermsConditionPageViewModel>();
                        //Settings.IsWalkthroughCompleted = false;
                        break;
                }
            }
        }
    }
} 