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
   public class ReachedLocationPageViewModel : AppBaseViewModel
    {
        #region localVariable
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand ValidateCommand { get; set; }
        public IAsyncCommand NavigateCommand { get; set; }
        public IAsyncCommand ReachedDropoffLocatioCommand { get; set; }
        #endregion
        private bool _isServiceType;
        public bool IsServiceType
        {
            get { return _isServiceType; }
            set { _isServiceType = value;OnPropertyChanged(nameof(IsServiceType)); }
        }
        private bool _sItems;
        public bool IsItems
        {
            get { return _sItems; }
            set { _sItems = value;OnPropertyChanged(nameof(IsItems)); }
        }

        public ReachedLocationPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            ValidateCommand = new AsyncCommand(ValidateCommandExecute);
            NavigateCommand = new AsyncCommand(NavigateCommandExecute);
            ReachedDropoffLocatioCommand = new AsyncCommand(ReachedDropoffLocatioCommandExecute);
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
                //if (_navigationService.GetCurrentPageViewModel() != typeof(PaymentSuccessPage))
                //{
                //await RichNavigation.PopUppopAsync();
                //await RichNavigation.PushAsync(new ReachedPickupLocationPage("VerifyItems"), typeof(ReachedPickupLocationPage));
                //}
            }
            catch (Exception ex)
            {

            }
        }         
        private async Task NavigateCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                //if (_navigationService.GetCurrentPageViewModel() != typeof(PaymentSuccessPage))
                //{
                //await RichNavigation.PopUppopAsync();
                //await RichNavigation.PushAsync(new MapRoutePage(), typeof(MapRoutePage));
                //}
            }
            catch (Exception ex)
            {

            }
        }
        private async Task ReachedDropoffLocatioCommandExecute()
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
                    await App.Locator.ReachedPickupLocationPage.InitilizeData("DeliveryStatus");
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task InitilizeData(string Title)
        {
            if (Title == "ReachedPicked")
            {
                IsServiceType = true;
                IsItems = false;
            }
            else
            {
                IsItems = true;
                IsServiceType = false;
            }
        } 
        #endregion
    }
}