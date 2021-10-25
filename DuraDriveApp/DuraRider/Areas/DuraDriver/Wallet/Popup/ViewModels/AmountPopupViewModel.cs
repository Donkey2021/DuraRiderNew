using DuraRider.Areas.Common.PopupView.View;
using DuraRider.Areas.DuraDriver.Home.ViewModels;
using DuraRider.Areas.DuraDriver.Wallet.ViewModels;
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
using Xamarin.CommunityToolkit.Extensions;

namespace DuraRider.Areas.DuraDriver.Wallet.Popup.ViewModels
{
    public class AmountPopupViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;

        private bool _isAmount;
        public bool IsAmount
        {
            get { return _isAmount; }
            set { _isAmount = value; OnPropertyChanged(nameof(IsAmount)); }
        }
        private bool _isAddAmount;
        public bool IsAddAmount
        {
            get { return _isAddAmount; }
            set { _isAddAmount = value; OnPropertyChanged(nameof(IsAddAmount)); }
        }
        private bool _isAddAmountDetails;
        public bool IsAddAmountDetails
        {
            get { return _isAddAmountDetails; }
            set { _isAddAmountDetails = value; OnPropertyChanged(nameof(IsAddAmountDetails)); }
        }
        private bool _isTransactionWith;
        public bool IsTransactionWith
        {
            get { return _isAddAmountDetails; }
            set { _isAddAmountDetails = value; OnPropertyChanged(nameof(IsTransactionWith)); }
        }
         
        public IAsyncCommand TopUpWalletCommand { get; set; }
        public IAsyncCommand AddMoneyCommand { get; set; }
        public IAsyncCommand AddMoneyDetailsCommand { get; set; }
        public IAsyncCommand RequesttoGcashCommand { get; set; }
        public AmountPopupViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
            TopUpWalletCommand = new AsyncCommand(TopUpWalletCommandExecute);
            AddMoneyCommand = new AsyncCommand(AddMoneyCommandExecute);
            AddMoneyDetailsCommand = new AsyncCommand(AddMoneyDetailsCommandExecute);
            RequesttoGcashCommand = new AsyncCommand(RequesttoGcashCommandExecute);
        } 
        private async Task TopUpWalletCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            ShowLoading();
            try
            {
                IsAmount = false;
                IsAddAmount = true;
                IsAddAmountDetails = false;
                IsTransactionWith = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                HideLoading();
            }
        }
        private async Task AddMoneyCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            ShowLoading();
            try
            {
                IsAmount = false;
                IsAddAmount = false;
                IsAddAmountDetails = true;
                IsTransactionWith = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                HideLoading();
            }
        }
        private async Task AddMoneyDetailsCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            ShowLoading();
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(GCashPageViewModel))
                {
                    await _navigationService.ClosePopupsAsync();
                    await _navigationService.NavigateToAsync<GCashPageViewModel>();
                    //await App.Locator.DocumentPage.InitilizeData();
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
        private async Task RequesttoGcashCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            ShowLoading();
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(GCashPageViewModel))
                {
                    await _navigationService.ClosePopupsAsync();
                    await Navigation.ShowPopupAsync(new SuccessfullyPopup("Request to Gcash successfully"));
                    //await App.Locator.DocumentPage.InitilizeData();
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
        public async Task InitilizeData(String PageTitle)
        {
            if (PageTitle == "TopUpPopup")
            {
                IsAmount = true;
                IsAddAmount = false;
                IsAddAmountDetails = false;
                IsTransactionWith = false;
            }
            else if (PageTitle == "RequestCashoutPopup")
            {
                IsTransactionWith = true;
                IsAmount = false;
                IsAddAmount = false;
                IsAddAmountDetails = false;
            }
            else
            {
                //default
                IsAmount = true;
                IsAddAmount = false;
                IsAddAmountDetails = false;
                IsTransactionWith = false;
            }
        }

    }
}