using System;
using System.Threading.Tasks;
using DuraRider.Areas.DuraDriver.Home.ViewModels;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;

namespace DuraRider.Areas.Common.ViewModels
{
    public class LoginPageViewModels : AppBaseViewModel
    {
        INavigationService _navigationService;
        public IAsyncCommand NavigateToHomeCommand { get; set; }
        public IAsyncCommand RegisterCommand { get; set; }
        public LoginPageViewModels(INavigationService navigationService)
        {
            _navigationService = navigationService;
            RegisterCommand = new AsyncCommand(RegisterCommandExecute);
            NavigateToHomeCommand = new AsyncCommand(NavigateToHomeCommandExecute);
        }

        private async Task RegisterCommandExecute()
        {
            if (_navigationService.GetCurrentPageViewModel() != typeof(SignUpPageViewModel))
            {
                await _navigationService.NavigateToAsync<SignUpPageViewModel>();
                await App.Locator.SignUpPage.InitilizeData();
            }
        }
        private async Task NavigateToHomeCommandExecute()
        { 
            if (_navigationService.GetCurrentPageViewModel() != typeof(HomePageViewModel))
            {
                await _navigationService.NavigateToAsync<HomePageViewModel>();
                await App.Locator.SignUpPage.InitilizeData();
            }
        }

        public async Task InitilizeData()
        {
            //LoginEmail = string.Empty;
            //LoginPassword = string.Empty;
        }
    }
}
