using DuraRider.Areas.DuraDriver.Home.Views;
using DuraRider.Core.Helpers;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Home.ViewModels
{
    public class VerifyItemPageViewModel : AppBaseViewModel
    {
        #region localVariable
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand ValidateCommand { get; set; }
        #endregion

        public VerifyItemPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            ValidateCommand = new AsyncCommand(ValidateCommandExecute);
        }
        #region Method 
        private async Task ValidateCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(ReachedLocationPageViewModel))
                { 
                    await _navigationService.NavigateToAsync<ReachedLocationPageViewModel>();
                    await App.Locator.ReachedLocationPage.InitilizeData("RechedDrop");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 
    }
}