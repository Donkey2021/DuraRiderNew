using DuraRider.Areas.Common.PopupView.View;
using DuraRider.Areas.DuraDriver.Home.Popup.ViewModels;
using DuraRider.Areas.DuraDriver.Home.Popup.Views;
using DuraRider.Core.Helpers;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Home.ViewModels
{
   public class ReachedPickupLocationPageViewModel : AppBaseViewModel
    {
        #region localVariable
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand CancelVerifyItemsCommand { get; set; }
        public IAsyncCommand VerifyItemsCommand { get; set; }
        public IAsyncCommand DeliveryNotDoneCommand { get; set; }
        public IAsyncCommand CollectPaymentCommand { get; set; }
        #endregion 
 
        private bool _isVerifyItems;
        public bool IsVerifyItems
        {
            get { return _isVerifyItems; }
            set { _isVerifyItems = value; OnPropertyChanged(nameof(IsVerifyItems)); }
        }
        private bool _isPaymentStatus;
        public bool IsPaymentStatus
        {
            get { return _isPaymentStatus; }
            set { _isPaymentStatus = value; OnPropertyChanged(nameof(IsPaymentStatus)); }
        }
        public ReachedPickupLocationPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            CancelVerifyItemsCommand = new AsyncCommand(CancelVerifyItemsExecute);
            VerifyItemsCommand = new AsyncCommand(VerifyItemsCommandExecute);
            DeliveryNotDoneCommand = new AsyncCommand(DeliveryNotDoneCommandExecute);
            CollectPaymentCommand = new AsyncCommand(CollectPaymentCommandExecute);
        }
        #region Method 
        private async Task CancelVerifyItemsExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(VerifyItemPageViewModel))
                {
                     Navigation.ShowPopup(new DeclinePopUp("CancelPopup"));
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async Task VerifyItemsCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(VerifyItemPageViewModel))
                { 
                    await _navigationService.NavigateToAsync<VerifyItemPageViewModel>();
                    //await App.Locator.VerifyItemPage.InitilizeData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async Task DeliveryNotDoneCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(VerifyItemPageViewModel))
                {
                    Navigation.ShowPopup(new DeclinePopUp("RejectPopup"));
                    //await App.Locator.VerifyItemPage.InitilizeData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async Task CollectPaymentCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(PaymentCollectedPopup))
                { 
                    await PopupNavigation.Instance.PushAsync(new PaymentCollectedPopup());
                }
            }
            catch (Exception ex)
            {

            }
        }
         public async Task InitilizeData(String PageTitle)
            {
            if (PageTitle == "VerifyItems")
            {
                IsVerifyItems = true;
                IsPaymentStatus = false;
            }
            else if (PageTitle == "DeliveryStatus")
            {
                IsPaymentStatus = true;
                IsVerifyItems = false;
            }
            else
            {
                IsVerifyItems = true;
                IsPaymentStatus = false;
            }
        }
        #endregion 
    }
}