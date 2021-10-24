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

namespace DuraRider.Areas.DuraDriver.Home.ViewModels
{
  public  class ReviewPageViewModel : AppBaseViewModel
    {
        #region localVariable
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand SubmitCommand { get; set; }
        public IAsyncCommand ReviewCommand { get; set; }
        public IAsyncCommand ReachedDropoffLocatioCommand { get; set; }
        #endregion 
        public ReviewPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            SubmitCommand = new AsyncCommand(SubmitCommandExecute);
            ReviewCommand = new AsyncCommand(ReviewCommandExecute); 
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
                if (_navigationService.GetCurrentPageViewModel() != typeof(HomePageViewModel))
                {
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                    await App.Locator.DocumentPage.InitilizeData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async Task ReviewCommandExecute()
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
                    await App.Locator.HomePage.InitilizeData();
                }
            }
            catch (Exception ex)
            {

            }
        }  
        public async Task InitilizeData()
        {
            
        }
        #endregion
    }
}