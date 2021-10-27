using DuraRider.Areas.DuraDriver.Home.HomeModels;
using DuraRider.Areas.DuraDriver.Home.Popup.Views;
using DuraRider.Core.Helpers;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Home.ViewModels
{
    public class HomePageViewModel : AppBaseViewModel
    {
        #region localVariable  
        public IAsyncCommand NotificationCommand { get; set; }
        public IAsyncCommand TopUpCommand { get; set; }
        public IAsyncCommand RequestCashoutCommand { get; set; } 
        #endregion
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService; 

        private int _arrowImageRotation;
        public int ArrowImageRotation
        {
            get { return _arrowImageRotation; }
            set { _arrowImageRotation = value; OnPropertyChanged(); }
        }
        private int _tripImageRotation;
        public int TripImageRotation
        {
            get { return _tripImageRotation; }
            set { _tripImageRotation = value; OnPropertyChanged(); }
        }
        private bool _stkIsVisisble;
        public bool StkIsVisisble
        {
            get { return _stkIsVisisble; }
            set { _stkIsVisisble = value; OnPropertyChanged(); }
        } 
        private bool _homeIsVisible;
        public bool HomeIsVisible
        {
            get { return _homeIsVisible; }
            set { _homeIsVisible = value; OnPropertyChanged(); }
        }
        public HomePageViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService; 
            NotificationCommand = new AsyncCommand(NotificationCommandExecute);  
            HomeIsVisible = false;
            StkIsVisisble = false;
            ArrowImageRotation = 90;
            TripImageRotation = -90;
        }

        #region Method    
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
        #endregion
        public async Task InitilizeData()
        {
            await PopupNavigation.Instance.PushAsync(new DuraExpressPopup());
        } 

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
        public ICommand ExpanderHomeCommand => new Xamarin.Forms.Command(async (obj) =>
        {
            if (HomeIsVisible)
            {
                HomeIsVisible = false;
                TripImageRotation = -90;
            }
            else
            {
                HomeIsVisible = true;
                TripImageRotation = 90;
            }
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

         
        public ObservableCollection<string> ProfileNameList { get; set; } = new ObservableCollection<string>
        {
            "hi, John"
        };  
        
    }
}