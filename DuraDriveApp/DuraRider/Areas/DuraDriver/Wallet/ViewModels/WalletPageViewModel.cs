using DuraRider.Areas.DuraDriver.Home.HomeModels;
using DuraRider.Areas.DuraDriver.Home.ViewModels;
using DuraRider.Areas.DuraDriver.Wallet.Popup.Views;
using DuraRider.Core.Helpers;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Rg.Plugins.Popup.Services;
using System; 
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace DuraRider.Areas.DuraDriver.Wallet.ViewModels
{
   public class WalletPageViewModel : AppBaseViewModel
    {

        #region localVariable
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;
         
        public IAsyncCommand NotificationCommand { get; set; }
        public IAsyncCommand TopUpCommand { get; set; }
        public IAsyncCommand RequestCashoutCommand { get; set; }
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
        public WalletPageViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
            NotificationCommand = new AsyncCommand(NotificationCommandExecute);
            TopUpCommand = new AsyncCommand(TopUpCommandExecute);
            RequestCashoutCommand = new AsyncCommand(RequestCashoutCommandExecute);
            StkIsVisisble = false;
            ArrowImageRotation = 90;
        }
        #region Method 
        private async Task TopUpCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(AmountPopup))
                {
                    await PopupNavigation.Instance.PushAsync(new AmountPopup());
                    await App.Locator.AmountPopup.InitilizeData("TopUpPopup");
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
        private async Task RequestCashoutCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(AmountPopup))
                {
                    await PopupNavigation.Instance.PushAsync(new AmountPopup());
                    await App.Locator.AmountPopup.InitilizeData("RequestCashoutPopup");
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
        public System.Windows.Input.ICommand WalletExpanderViewCommand => new Xamarin.Forms.Command(async (obj) =>
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
        public async Task InitilizeData()
        {

        }
        public ObservableCollection<string> DaysList { get; set; } = new ObservableCollection<string>
        {
            "Today","Tomarrow"
        }; 

        //public System.Windows.Input.ICommand TopUpCommand => new Command(async (obj) =>
        //{
        //    // await RichNavigation.PushAsync(new ProfileTabPage(), typeof(ProfileTabPage));
        //});
        //public ICommand RequestCashoutCommand => new Xamarin.Forms.Command(async (obj) =>
        //{
        //    // await RichNavigation.PushAsync(new ProfileTabPage(), typeof(ProfileTabPage));
        //});

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
    }
}