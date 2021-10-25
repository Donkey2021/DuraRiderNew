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
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Wallet.ViewModels
{
   public class GCashPageViewModel : AppBaseViewModel
    { 
        #region localVariable
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;

        public IAsyncCommand SubmitCommand { get; set; }
        #endregion

        public GCashPageViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
            SubmitCommand = new AsyncCommand(SubmitCommandExecute);
        }
        #region Method 
        private async Task SubmitCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(SuccessfulPageViewModel))
                {
                    await _navigationService.NavigateToAsync<SuccessfulPageViewModel>();
                    //await App.Locator.SuccessfulPage.InitilizeData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 
    }
}
