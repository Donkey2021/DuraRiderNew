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
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Wallet.ViewModels
{
   public class SuccessfulPageViewModel : AppBaseViewModel
    {
        #region localVariable
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;

        public IAsyncCommand OkayCommand { get; set; }
        #endregion

        public SuccessfulPageViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
            OkayCommand = new AsyncCommand(OkayCommandExecute);
        }
        #region Method 
        private async Task OkayCommandExecute()
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
                   // await RichNavigation.PushAsync(new HomePage(2), typeof(HomePage));
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                    await App.Locator.HomePage.InitilizeData();
                } 
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 
    }
}  