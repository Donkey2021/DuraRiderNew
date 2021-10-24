using DuraDriveRider.Pages.ViewModels;
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
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Home.ViewModels
{
    public class HomePageViewModel : AppBaseViewModel
    {
        #region localVariable 
        public IAsyncCommand DoneCommand { get; set; }
        public IAsyncCommand NotificationCommand { get; set; }
        #endregion
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;

        private int _arrowImageRotation;
        public int ArrowImageRotation
        {
            get { return _arrowImageRotation; }
            set { _arrowImageRotation = value; OnPropertyChanged(); }
        }
        private bool _stkIsVisisble;
        public bool StkIsVisisble
        {
            get { return _stkIsVisisble; }
            set { _stkIsVisisble = value; OnPropertyChanged(); }
        }

        private Color _personalDetailsTextColor = Color.Black;
        public Color PersonalDetailsTextColor
        {
            get => _personalDetailsTextColor;
            set { _personalDetailsTextColor = value; OnPropertyChanged(); }
        }

        private Color _personalDetailsBoxviewColor = Color.FromHex("#267EAA");
        public Color PersonalDetailsBoxviewColor
        {
            get => _personalDetailsBoxviewColor;
            set { _personalDetailsBoxviewColor = value; OnPropertyChanged(); }
        }
        private Color _officalDetailsTextColor = Color.Gray;
        public Color OfficalDetailsTextColor
        {
            get => _officalDetailsTextColor;
            set { _officalDetailsTextColor = value; OnPropertyChanged(); }
        }

        private Color _officalDetailsBoxviewColor = Color.WhiteSmoke;
        public Color OfficalDetailsBoxviewColor
        {
            get => _officalDetailsBoxviewColor;
            set { _officalDetailsBoxviewColor = value; OnPropertyChanged(); }
        }

        private Color _paymentDetailsTextColor = Color.Gray;
        public Color PaymentDetailsTextColor
        {
            get => _paymentDetailsTextColor;
            set { _paymentDetailsTextColor = value; OnPropertyChanged(); }
        }

        private Color _paymentDetailsBoxviewColor = Color.WhiteSmoke;
        public Color PaymentDetailsBoxviewColor
        {
            get => _paymentDetailsBoxviewColor;
            set { _paymentDetailsBoxviewColor = value; OnPropertyChanged(); }
        }

        private bool _personalDetailsIsVisible;
        public bool PersonalDetailsIsVisible
        {
            get { return _personalDetailsIsVisible; }
            set { _personalDetailsIsVisible = value; OnPropertyChanged(); }
        }

        private bool _officialDetailsIsVisible;
        public bool OfficialDetailsIsVisible
        {
            get { return _officialDetailsIsVisible; }
            set { _officialDetailsIsVisible = value; OnPropertyChanged(); }
        }


        private bool _paymentDetailsIsVisible;
        public bool PaymentDetailsIsVisible
        {
            get { return _paymentDetailsIsVisible; }
            set { _paymentDetailsIsVisible = value; OnPropertyChanged(); }
        }

        private ImageSource _profileImage;
        public ImageSource ProfileImage
        {
            get { return _profileImage; }
            set { _profileImage = value; OnPropertyChanged(nameof(ProfileImage)); }
        }
        private string _accountName;
        public string AccountName
        {
            get { return _accountName; }
            set { _accountName = value; OnPropertyChanged(); }
        }
        private string _paypalId;
        public string PaypalId
        {
            get { return _paypalId; }
            set { _paypalId = value; OnPropertyChanged(); }
        }
        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set { _bankName = value; OnPropertyChanged(); }
        }
        private string _accountNumber;
        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; OnPropertyChanged(); }
        }
        private bool _homeIsVisible;
        public bool HomeIsVisible
        {
            get { return _homeIsVisible; }
            set { _homeIsVisible = value; OnPropertyChanged(); }
        }
        public HomePageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            DoneCommand = new AsyncCommand(DoneCommandExecute);
            NotificationCommand = new AsyncCommand(NotificationCommandExecute);
            PersonalDetailsIsVisible = true;
            OfficialDetailsIsVisible = false;
            PaymentDetailsIsVisible = false;
            PersonalDetailsTextColor = Color.White;
            PersonalDetailsBoxviewColor = Color.FromHex("#211E66");
            OfficalDetailsBoxviewColor = Color.Transparent;
            OfficalDetailsTextColor = Color.FromHex("#75747F");
            PaymentDetailsBoxviewColor = Color.Transparent;
            PaymentDetailsTextColor = Color.FromHex("#75747F"); 
            HomeIsVisible = false;
            StkIsVisisble = false;
            ArrowImageRotation = 90;
        }

        #region Method   
        private async Task DoneCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(HomePageViewModel))
                {
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                    await App.Locator.SignUpPage.InitilizeData();
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
        private async Task NotificationCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(NotificationPageViewModel))
                {
                    await _navigationService.NavigateToAsync<NotificationPageViewModel>();
                    await App.Locator.SignUpPage.InitilizeData();
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
        #endregion
        public async Task InitilizeData()
        {

        }
        public ObservableCollection<string> DaysList { get; set; } = new ObservableCollection<string>
        {
            "Today"
        };

        public ICommand HomeExpanderViewCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            if (StkIsVisisble)
            {
                StkIsVisisble = false;
                ArrowImageRotation = 90;
            }
            else
            {
                StkIsVisisble = true;
                ArrowImageRotation = -90;
            }
        });
        public ICommand OrderExpanderViewCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            if (StkIsVisisble)
            {
                StkIsVisisble = false;
                ArrowImageRotation = 90;
            }
            else
            {
                StkIsVisisble = true;
                ArrowImageRotation = -90;
                //await RichNavigation.PushAsync(new HomeXctTab(0), typeof(HomeXctTab));
            }
        });
        public ICommand WalletExpanderViewCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            if (StkIsVisisble)
            {
                StkIsVisisble = false;
                ArrowImageRotation = 90;
            }
            else
            {
                StkIsVisisble = true;
                ArrowImageRotation = -90;
                // await RichNavigation.PushAsync(new HomeXctTab(0), typeof(HomeXctTab));
            }
        });
        public ICommand ExpanderHomeCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            if (HomeIsVisible)
            {
                HomeIsVisible = false;
            }
            else
            {
                HomeIsVisible = true;
            }
        });

        public ICommand PersonalDetailsCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            PersonalDetailsIsVisible = true;
            OfficialDetailsIsVisible = false;
            PaymentDetailsIsVisible = false;
            PersonalDetailsTextColor = Color.White;
            PersonalDetailsBoxviewColor = Color.FromHex("#211E66");
            OfficalDetailsBoxviewColor = Color.Transparent;
            OfficalDetailsTextColor = Color.FromHex("#75747F");
            PaymentDetailsBoxviewColor = Color.Transparent;
            PaymentDetailsTextColor = Color.FromHex("#75747F");
        });
        public ICommand OfficalDetailsCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            PersonalDetailsIsVisible = false;
            OfficialDetailsIsVisible = true;
            PaymentDetailsIsVisible = false;
            PersonalDetailsTextColor = Color.FromHex("#75747F");
            PersonalDetailsBoxviewColor = Color.Transparent;
            OfficalDetailsBoxviewColor = Color.FromHex("#211E66");
            OfficalDetailsTextColor = Color.White;
            PaymentDetailsTextColor = Color.FromHex("#75747F"); ;
            PaymentDetailsBoxviewColor = Color.Transparent;
        });
        public ICommand PaymentDetailsCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            PersonalDetailsIsVisible = false;
            OfficialDetailsIsVisible = false;
            PaymentDetailsIsVisible = true;
            PersonalDetailsBoxviewColor = Color.Transparent;
            PersonalDetailsTextColor = Color.FromHex("#75747F");
            OfficalDetailsBoxviewColor = Color.Transparent;
            OfficalDetailsTextColor = Color.FromHex("#75747F");
            PaymentDetailsTextColor = Color.White;
            PaymentDetailsBoxviewColor = Color.FromHex("#211E66");
        });
        public ObservableCollection<OrderModel> HomeOfList { get; set; } = new ObservableCollection<OrderModel>()
        {
            new OrderModel{  Name="Dura Express", Status="Ongoing", StatusTextColor="#006FFF", StatusBgColor="#C1D5FF", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Continue to delivery",ChargesTextColor="#211E66", date="" },
            new OrderModel{  Name="Dura Shop", Status="Cancelled", StatusTextColor="#C80000", StatusBgColor="#FFB8B8", Address="2540 Makati, Metro Manila Phillipnes", Charges="Chages : ₱ 2.50",ChargesTextColor="#D72625", date="July 25 2020 at 05:30 pm" },
            new OrderModel{  Name="Dura Eats", Status="Delivered", StatusTextColor="#009700", StatusBgColor="#98FFB0", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Earned : ₱ 15.00",ChargesTextColor="#109A00", date="July 25 2020 at 05:30 pm" },
            new OrderModel{  Name="Dura Express", Status="Ongoing", StatusTextColor="#006FFF", StatusBgColor="#C1D5FF", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Continue to delivery",ChargesTextColor="#211E66", date="" },
            new OrderModel{  Name="Dura Shop", Status="Cancelled", StatusTextColor="#C80000", StatusBgColor="#FFB8B8", Address="2540 Makati, Metro Manila Phillipnes", Charges="Chages : ₱ 2.50",ChargesTextColor="#D72625", date="July 25 2020 at 05:30 pm" },
            new OrderModel{  Name="Dura Eats", Status="Delivered", StatusTextColor="#009700", StatusBgColor="#98FFB0", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Earned : ₱ 15.00",ChargesTextColor="#109A00", date="July 25 2020 at 05:30 pm" },
        };
        public ObservableCollection<OrderModel> DuraExpressOfList { get; set; } = new ObservableCollection<OrderModel>()
        {
            new OrderModel{  Name="Dura Express", Status="Delivered", StatusTextColor="#009700", StatusBgColor="#98FFB0", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Earned : ₱ 30.90",ChargesTextColor="#109A00", date="July 25 2020 at 05:30 pm" },
            new OrderModel{  Name="Dura Express", Status="Cancelled", StatusTextColor="#C80000", StatusBgColor="#FFB8B8", Address="2540 Makati, Metro Manila Phillipnes", Charges="Charges : ₱ 2.50",ChargesTextColor="#D72625", date="July 25 2020 at 05:30 pm" },
            new OrderModel{  Name="Dura Express", Status="Delivered", StatusTextColor="#009700", StatusBgColor="#98FFB0", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Earned : ₱ 30.90",ChargesTextColor="#109A00", date="July 25 2020 at 05:30 pm" },
        };
        public ObservableCollection<OrderModel> DuraShopsOfList { get; set; } = new ObservableCollection<OrderModel>()
        {
            new OrderModel{  Name="Dura Shops", Status="Delivered", StatusTextColor="#009700", StatusBgColor="#98FFB0", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Earned : ₱ 30.90",ChargesTextColor="#109A00", date="July 25 2020 at 05:30 pm" },
            new OrderModel{  Name="Dura Express", Status="Delivered", StatusTextColor="#009700", StatusBgColor="#98FFB0", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Earned : ₱ 20.80",ChargesTextColor="#109A00", date="July 25 2020 at 05:30 pm" },
        };
        public ObservableCollection<OrderModel> DuraOfList { get; set; } = new ObservableCollection<OrderModel>()
        {
            new OrderModel{  Name="Dura Eats", Status="Delivered", StatusTextColor="#009700", StatusBgColor="#98FFB0", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Earned : ₱ 30.90",ChargesTextColor="#109A00", date="July 25 2020 at 05:30 pm" },
            new OrderModel{  Name="Dura Eats", Status="Cancelled", StatusTextColor="#C80000", StatusBgColor="#FFB8B8", Address="2540 Makati, Metro Manila Phillipnes", Charges="Charges : ₱ 2.50",ChargesTextColor="#D72625", date="July 25 2020 at 05:30 pm" },
            new OrderModel{  Name="Dura Eats", Status="Delivered", StatusTextColor="#009700", StatusBgColor="#98FFB0", Address="1976 Capt. M. Reyes, Makati, Metro Manila Phillipnes", Charges="Earned : ₱ 30.90",ChargesTextColor="#109A00", date="July 25 2020 at 05:30 pm" },
        };

        //Wallet
        //public ICommand TopUpCommand => new Command(async (obj) =>
        //{
        //    // await RichNavigation.PushAsync(new ProfileTabPage(), typeof(ProfileTabPage));
        //});
        public ICommand RequestCashoutCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            // await RichNavigation.PushAsync(new ProfileTabPage(), typeof(ProfileTabPage));
        });

        public ObservableCollection<string> ProfileNameList { get; set; } = new ObservableCollection<string>
        {
            "hi, John"
        };

        public ObservableCollection<string> PaymentList { get; set; } = new ObservableCollection<string>
        {
            "All","E-Wallet txn","Cash Wallet"
        };
        //private void MaterialMenuButton_MenuSelected(object sender, MenuSelectedEventArgs e)
        //{
        //    MaterialDialog.Instance.AlertAsync("MenuSelected");
        //}
       // public MaterialMenuItem[] Actions => new MaterialMenuItem[]
       //{
       //     new MaterialMenuItem
       //     {
       //         Text = "Edit"
       //     },
       //     new MaterialMenuItem
       //     {
       //         Text = "Delete"
       //     }
       //};

        //public ICommand MenuCommand = new Command(
        //    execute: (arg) =>
        //    {
        //        MaterialDialog.Instance.AlertAsync("MenuCommand");
        //    },
        //    canExecute: (x) =>
        //    {
        //        bool? retval = MaterialDialog.Instance.ConfirmAsync(message: "Allow Menu?", confirmingText: "Yes", dismissiveText: "No").Result;
        //        return (retval.HasValue && retval.Value == true);
        //    });

        public ObservableCollection<ProfileModel> EWalletOfList { get; set; } = new ObservableCollection<ProfileModel>()
        {
            new ProfileModel{ TitleName = "#14565636664" },
            new ProfileModel{ TitleName = "#14565636664" },
            new ProfileModel{ TitleName = "#14565636664" },
            new ProfileModel{ TitleName = "#14565636664" },
            new ProfileModel{ TitleName = "#14565636664" },
            new ProfileModel{ TitleName = "#14565636664" },
            new ProfileModel{ TitleName = "#14565636664" },
            new ProfileModel{ TitleName = "#14565636664" },
            new ProfileModel{ TitleName = "#14565636664" },
        };
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

        //public ICommand ViewProfileCommand => new Command(async (obj) =>
        //{
        //    await RichNavigation.PushAsync(new ProfileTabPage(), typeof(ProfileTabPage));
        //});

        public ICommand LogOutCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            //var myValue = SecureStorageCustom.Remove("token");
            //if (myValue)
            //{
            //    App.Current.MainPage = new MaterialNavigationPage(new LoginPage());
            //}
            //Settings.IsWalkthroughCompleted = false;
        });

        //public ICommand ProfileTabCommand => new Command(async (obj) =>
        //{
        //    var ProfileMdl = obj as ProfileModel;
        //    switch (ProfileMdl.id)
        //    {
        //        case 1:
        //            await RichNavigation.PushAsync(new QrCodePage(), typeof(QrCodePage));
        //            break;
        //        case 2:
        //            await RichNavigation.PushAsync(new ReferRiderPage(), typeof(ReferRiderPage));
        //            break;
        //        case 3:
        //            await RichNavigation.PushAsync(new MyRatingPage(), typeof(MyRatingPage));
        //            break;
        //        case 4:
        //            await RichNavigation.PushAsync(new SupportPage(), typeof(SupportPage));
        //            break;
        //        case 5:
        //            await RichNavigation.PushAsync(new HelpCenterPage(), typeof(HelpCenterPage));
        //            break;
        //        case 6:
        //            await RichNavigation.PushAsync(new AboutusPage(), typeof(AboutusPage));
        //            break;
        //        case 7:
        //            await RichNavigation.PushAsync(new PrivacyPolicy(), typeof(PrivacyPolicy));
        //            break;
        //        case 8:
        //            await RichNavigation.PushAsync(new TermsConditionPage(), typeof(TermsConditionPage));
        //            break;
        //        case 9:
        //            // App.Current.MainPage = new MaterialNavigationPage(new LogInPage());
        //            //Settings.IsWalkthroughCompleted = false;
        //            break;
        //    }
        //});
    }
}
     