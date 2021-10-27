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
   public class ReachLocationPageViewModel : AppBaseViewModel
    {
        #region localVariable
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand ReachedPickupLocationCommand { get; set; }
        #endregion

        public ReachLocationPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            ReachedPickupLocationCommand = new AsyncCommand(ReachedPickupLocationCommandExecute);
        }
        #region Method 
        private async Task ReachedPickupLocationCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(ReachedPickupLocationPageViewModel))
                { 
                     await _navigationService.NavigateToAsync<ReachedPickupLocationPageViewModel>();
                    await App.Locator.ReachedPickupLocationPage.InitilizeData("VerifyItems");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}