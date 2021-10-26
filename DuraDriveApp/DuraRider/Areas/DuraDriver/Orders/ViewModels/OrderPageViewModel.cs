using DuraRider.Areas.DuraDriver.Home.HomeModels;
using DuraRider.Areas.DuraDriver.Home.ViewModels;
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
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Orders.ViewModels
{
    public class OrderPageViewModel : AppBaseViewModel
    {

        #region localVariable
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;

        public IAsyncCommand NotificationCommand { get; set; }
        #endregion
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
        public OrderPageViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
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
                //if (_navigationService.GetCurrentPageViewModel() != typeof(PaymentSuccessPage))
                //{
                //    await _navigationService.NavigateToAsync<PaymentSuccessPage>();
                //    await App.Locator.SignUpPage.InitilizeData();
                //}
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        public async Task InitilizeData()
        {

        }
        public ObservableCollection<string> DaysList { get; set; } = new ObservableCollection<string>
        {
            "Today","Tomarrow"
        };
         
        public System.Windows.Input.ICommand OrderExpanderViewCommand => new Xamarin.Forms.Command(async (obj) =>
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
                    await App.Locator.NotificationPage.InitilizeData();
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
        public System.Windows.Input.ICommand PersonalDetailsCommand => new Xamarin.Forms.Command(async (obj) =>
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
        public System.Windows.Input.ICommand OfficalDetailsCommand => new Xamarin.Forms.Command(async (obj) =>
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
        public System.Windows.Input.ICommand PaymentDetailsCommand => new Xamarin.Forms.Command(async (obj) =>
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
    } 
}