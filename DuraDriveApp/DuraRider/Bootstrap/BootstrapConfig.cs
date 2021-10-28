using System;
using System.Diagnostics;
using FreshMvvm;
using System.Net.Http;
using DuraRider.Services.Interfaces;
using DuraRider.Core.Services;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services;
using DuraRider.Areas.Common.ViewModels;
using DuraRider.Areas.Common.PopupView.ViewModel;
using DuraRider.Areas.DuraDriver.Home.ViewModels; 
using DuraRider.Areas.DuraDriver.Home.Popup.ViewModels;
using DuraRider.Areas.DuraDriver.Wallet.Popup.ViewModels;
using DuraRider.Areas.DuraDriver.Wallet.ViewModels;
using DuraRider.Areas.DuraDriver.Profile.ViewModels;
using DuraRider.ViewModels;
using DuraRider.Areas.DuraDriver.Orders.ViewModels;
using DuraRider.Areas.DuraDriver.Profile.MyProfile.ViewModels;

namespace DuraRider.Bootstrap
{
    public class BootstrapConfig
    {
        public BootstrapConfig()
        {
        }
        public static void RegisterService()
        {
            try
            {
                FreshIOC.Container.Register<HttpClient>(new HttpClient());
            }
            catch (Exception e)
            {
                Debug.WriteLine($"already initiated ={e.Message}");
            }

            FreshIOC.Container.Register<IUserService, UserService>();
            FreshIOC.Container.Register<INavigationService, NavigationService>();
            FreshIOC.Container.Register<HttpService, HttpService>();
            FreshIOC.Container.Register<IAuthenticationService, AuthenticationService>();
            FreshIOC.Container.Register<IUserCoreService, UserCoreService>();
        }
        public static void RegisterViewModel()
        {
            FreshIOC.Container.Register<CurrentUser, CurrentUser>().AsSingleton();
            FreshIOC.Container.Register<AppShellViewModel, AppShellViewModel>().AsSingleton();
            FreshIOC.Container.Register<LoginPageViewModels, LoginPageViewModels>().AsSingleton();
            FreshIOC.Container.Register<SignUpPageViewModel, SignUpPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<OTPPageViewModel, OTPPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<PersonalInfoPageViewModel, PersonalInfoPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ReferralPopUpPageViewModel, ReferralPopUpPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<DocumentPageViewModel, DocumentPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<CameraGalleryPopupPageViewModel, CameraGalleryPopupPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<GCashAccountDetailsPageViewModel, GCashAccountDetailsPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ProfilePicPageViewModel,ProfilePicPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<DuraBageServicePageViewModel, DuraBageServicePageViewModel>().AsSingleton();
            FreshIOC.Container.Register<HomePageViewModel, HomePageViewModel>().AsSingleton();
            FreshIOC.Container.Register<OrderPageViewModel, OrderPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<WalletPageViewModel, WalletPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ProfilePageViewModel, ProfilePageViewModel>().AsSingleton();
            FreshIOC.Container.Register<NotificationPageViewModel, NotificationPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<DuraExpressPopupViewModel, DuraExpressPopupViewModel>().AsSingleton();
            FreshIOC.Container.Register<VerifyItemPageViewModel, VerifyItemPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ReachLocationPageViewModel, ReachLocationPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ReachedLocationPageViewModel, ReachedLocationPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ReachedPickupLocationPageViewModel, ReachedPickupLocationPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<PaymentCollectedPopupViewModel, PaymentCollectedPopupViewModel>().AsSingleton();
            FreshIOC.Container.Register<ReviewPageViewModel, ReviewPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<AmountPopupViewModel, AmountPopupViewModel>().AsSingleton();
            FreshIOC.Container.Register<GCashPageViewModel, GCashPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<SuccessfulPageViewModel, SuccessfulPageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<AboutusPageViewModel, AboutusPageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<HelpCenterPageViewModel, HelpCenterPageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<MyRatingPageViewModel, MyRatingPageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<PrivacyPolicyPageViewModel, PrivacyPolicyPageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<MyProfilePageViewModel, MyProfilePageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<QrCodePageViewModel, QrCodePageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<ReferRiderPageViewModel, ReferRiderPageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<SupportPageViewModel, SupportPageViewModel>().AsSingleton(); 
            FreshIOC.Container.Register<TermsConditionPageViewModel, TermsConditionPageViewModel>().AsSingleton(); 
        }
    }
}