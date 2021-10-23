using System;
using System.Net.Http;
using System.Threading.Tasks;
using DuraRider.Areas.Common.ViewModels;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DuraRider.Areas.Common.PopupView.ViewModel
{
    public class ReferralPopUpPageViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand SkipCommand { get; set; }
        public IAsyncCommand SubmitCommand { get; set; }
        private RegistrationData _registrationData;
        public RegistrationData RegistrationData
        {
            get { return _registrationData; }
            set
            {
                _registrationData = value;
                OnPropertyChanged(nameof(RegistrationData));
            }
        }

        private string _referralCode;
        public string ReferralCode
        {
            get { return _referralCode; }
            set
            {
                _referralCode = value;
                OnPropertyChanged(nameof(ReferralCode));
            }
        }

        public ReferralPopUpPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            SkipCommand = new AsyncCommand(SkipCommandExecute, allowsMultipleExecutions: false);
            SubmitCommand = new AsyncCommand(SubmitCommandExecute, allowsMultipleExecutions: false);
        }

        private async Task SubmitCommandExecute()
        {
            if (string.IsNullOrEmpty(ReferralCode))
            {
                ShowToast("Please enter regerral code to proceed");
                return;
            }
            await SavePersonalInfo();
        }

        private async Task SkipCommandExecute()
        {
            await SavePersonalInfo();
        }

        private async Task SavePersonalInfo()
        {
            if (RegistrationData == null)
            {
                return;
            }
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            ShowLoading();
            try
            {
                var firstName = string.IsNullOrEmpty(RegistrationData?.firstname) ? string.Empty : RegistrationData?.firstname;
                var lastName = string.IsNullOrEmpty(RegistrationData?.lastname) ? string.Empty : RegistrationData?.lastname;
                var middleName = string.IsNullOrEmpty(RegistrationData?.middlename) ? string.Empty : RegistrationData?.middlename;
                var referralcode = string.IsNullOrEmpty(ReferralCode) ? string.Empty : ReferralCode;
                var managerid = string.IsNullOrEmpty(RegistrationData?.manger_id) ? string.Empty : RegistrationData?.manger_id;
                var dob = string.IsNullOrEmpty(RegistrationData?.dob) ? string.Empty : RegistrationData?.dob;

                var form = new MultipartFormDataContent();
                form.Add(new StringContent(Convert.ToString(RegistrationData.driverid)), "driver_id");
                form.Add(new StringContent(firstName), "firstname");
                form.Add(new StringContent(middleName), "middlename");
                form.Add(new StringContent(lastName), "lastname");
                form.Add(new StringContent(managerid), "manger_id");
                form.Add(new StringContent(dob), "dob");
                form.Add(new StringContent(referralcode), "refreal_no");
                form.Add(new StringContent(Convert.ToString(RegistrationData.latitude)), "latitude");
                form.Add(new StringContent(Convert.ToString(RegistrationData.longitude)), "longitude");

                var result = await TryWithErrorAsync(_authenticationService.SaveDriverPersonalInfo(form), true, false);
                if (result?.ResultType == ResultType.Ok && result?.Data?.status == 200)
                {
                    await _navigationService.ClosePopupsAsync();
                    if (_navigationService.GetCurrentPageViewModel() != typeof(DocumentPageViewModel))
                    {
                        await _navigationService.NavigateToAsync<DocumentPageViewModel>();
                        await App.Locator.DocumentPage.InitilizeData();
                    }
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
        internal async Task InitilizeData()
        {
            RegistrationData = App.Locator.OTPPage.RegistrationData;
        }
    }
}
