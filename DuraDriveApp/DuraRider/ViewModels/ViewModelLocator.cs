using FreshMvvm;
using DuraRider.Areas.Common.ViewModels;
using DuraRider.Services;
using DuraRider.Areas.Common.PopupView.ViewModel;
using DuraRider.Areas.DuraDriver.Home.ViewModels;
using DuraDriveRider.Pages.ViewModels; 
using DuraRider.Areas.DuraDriver.Home.Popup.ViewModels;

namespace DuraRider.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {

        }
        static ViewModelLocator()
        {

        }
        public CurrentUser CurrentUser => FreshIOC.Container.Resolve<CurrentUser>();
        public AppShellViewModel AppShell => FreshIOC.Container.Resolve<AppShellViewModel>();
        public LoginPageViewModels LoginPage => FreshIOC.Container.Resolve<LoginPageViewModels>();
        public OTPPageViewModel OTPPage => FreshIOC.Container.Resolve<OTPPageViewModel>();
        public SignUpPageViewModel SignUpPage => FreshIOC.Container.Resolve<SignUpPageViewModel>();
        public PersonalInfoPageViewModel PersonalInfoPage => FreshIOC.Container.Resolve<PersonalInfoPageViewModel>();
        public ReferralPopUpPageViewModel ReferralPopUpPage => FreshIOC.Container.Resolve<ReferralPopUpPageViewModel>();
        public DocumentPageViewModel DocumentPage => FreshIOC.Container.Resolve<DocumentPageViewModel>();
        public CameraGalleryPopupPageViewModel CameraGalleryPopupPage => FreshIOC.Container.Resolve<CameraGalleryPopupPageViewModel>();
        public GCashAccountDetailsPageViewModel GCashAccountDetailsPage => FreshIOC.Container.Resolve<GCashAccountDetailsPageViewModel>();
        public ProfilePageViewModel ProfilePage => FreshIOC.Container.Resolve<ProfilePageViewModel>();
        public DuraBageServicePageViewModel DuraBageServicePage => FreshIOC.Container.Resolve<DuraBageServicePageViewModel>();
        public HomePageViewModel HomePage => FreshIOC.Container.Resolve<HomePageViewModel>();
        public NotificationPageViewModel NotificationPage => FreshIOC.Container.Resolve<NotificationPageViewModel>();
        public VerifyItemPageViewModel VerifyItemPage => FreshIOC.Container.Resolve<VerifyItemPageViewModel>();
        public ReachedLocationPageViewModel ReachedLocationPage => FreshIOC.Container.Resolve<ReachedLocationPageViewModel>();
        public ReachLocationPopUpViewModel ReachLocationPopUp => FreshIOC.Container.Resolve<ReachLocationPopUpViewModel>();
        public ReachedPickupLocationPageViewModel ReachedPickupLocationPage => FreshIOC.Container.Resolve<ReachedPickupLocationPageViewModel>();
        public PaymentCollectedPopupViewModel PaymentCollectedPopup => FreshIOC.Container.Resolve<PaymentCollectedPopupViewModel>();
        public ReviewPageViewModel ReviewPage => FreshIOC.Container.Resolve<ReviewPageViewModel>();

    }
}
