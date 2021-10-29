
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
            FreshIOC.Container.Register<LoginPageViewModels, LoginPageViewModels>().AsSingleton();
            FreshIOC.Container.Register<SignUpPageViewModel, SignUpPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<OTPPageViewModel, OTPPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<PersonalInfoPageViewModel, PersonalInfoPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ReferralPopUpPageViewModel, ReferralPopUpPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<DocumentPageViewModel, DocumentPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<CameraGalleryPopupPageViewModel, CameraGalleryPopupPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<GCashAccountDetailsPageViewModel, GCashAccountDetailsPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ProfilePageViewModel, ProfilePageViewModel>().AsSingleton();
            FreshIOC.Container.Register<DuraBageServicePageViewModel, DuraBageServicePageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ApprovalPageViewModel, ApprovalPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ForgotPasswordPageViewModel, ForgotPasswordPageViewModel>().AsSingleton();
            FreshIOC.Container.Register<ResetPasswordPageViewModel, ResetPasswordPageViewModel>().AsSingleton();
        }
    }
}
