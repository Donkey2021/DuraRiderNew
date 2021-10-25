using DuraRider.Areas.Common.ViewModels;
using DuraRider.Areas.DuraDriver.Home.ViewModels;
using DuraRider.Core.Helpers;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DuraRider.Areas.DuraDriver.Home.Popup.ViewModels
{
   public class PaymentCollectedPopupViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;

        public IAsyncCommand ConfirmDeliveryCommand { get; set; }
        public PaymentCollectedPopupViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
            ConfirmDeliveryCommand = new AsyncCommand(ConfirmDeliveryCommandExecute); 
        } 
        private async Task ConfirmDeliveryCommandExecute()
        { 
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            ShowLoading();
            try
            { 
                if (_navigationService.GetCurrentPageViewModel() != typeof(ReviewPageViewModel))
                {
                    await _navigationService.CloseAllPopupsAsync();
                    await _navigationService.NavigateToAsync<ReviewPageViewModel>();
                    await App.Locator.DocumentPage.InitilizeData();
                }
                else
                {
                    ShowToast("Personal info-API Error.");
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
    }
}
