using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Models.ResponseModels;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Helpers;
using DuraRider.Services;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DuraRider.Areas.Common.ViewModels
{
    public class ForgotPasswordPageViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private string _countriesTitle;
        private List<NewLocationDataResponse> locList;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand GetOTPCommand { get; set; }
        private ObservableCollection<NewLocationDataResponse> _allLocationList;
        public ObservableCollection<NewLocationDataResponse> AllLocationList
        {
            get { return _allLocationList; }
            set
            {
                SetProperty(ref _allLocationList, value);
            }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                this.OnPropertyChanged("PhoneNumber");
            }
        }

        private string _phoneError;
        public string PhoneError
        {
            get
            {
                return _phoneError;
            }
            set
            {
                SetProperty(ref _phoneError, value);

            }
        }



        private bool _phoneErrorVisibility;
        public bool PhoneErrorVisibility
        {
            get
            {
                return _phoneErrorVisibility;
            }
            set
            {
                SetProperty(ref _phoneErrorVisibility, value);
            }
        }
        private ObservableCollection<NewLocationDataResponse> _allLocationListCode;
        public ObservableCollection<NewLocationDataResponse> AllLocationListCode
        {
            get { return _allLocationListCode; }
            set
            {
                SetProperty(ref _allLocationListCode, value);
            }
        }
        public ForgotPasswordPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            GetOTPCommand = new AsyncCommand(GetOTPCommandExecute, allowsMultipleExecutions: false);
            GetAllLocation();
        }
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

        private NewLocationDataResponse _selectedCountries;
        public NewLocationDataResponse SelectedCountries
        {
            get { return _selectedCountries; }
            set
            {
                _selectedCountries = value;
                this.OnPropertyChanged(nameof(SelectedCountries));
                this.OnPropertyChanged(nameof(CountriesTitle));
                CountriesTitle = $"+{_selectedCountries.country_code}";
            }
        }
        public string CountriesTitle
        {
            get { return _countriesTitle; }
            set
            {
                _countriesTitle = value;
                this.OnPropertyChanged("CountriesTitle");
            }
        }
        private bool UserEnteredAllRecord()
        {
            try
            {
                PhoneErrorVisibility = false;
                PhoneError = "";
                PhoneError = string.IsNullOrEmpty(PhoneNumber) ? ErrorMessages.MobileTextEmpty : PhoneError;
                if (!string.IsNullOrEmpty(PhoneNumber))
                {
                    PhoneError = !RegexUtilities.PhoneNumberValidation(PhoneNumber) ? ErrorMessages.PhoneWrong : PhoneError;
                }
                if (!string.IsNullOrEmpty(PhoneError))
                {
                    PhoneErrorVisibility = true;
                    return PhoneError == "";
                }
                return true;
            }
            catch (Exception ex)
            {
                //  Task.Run(async () => await ErrorHelper.SendError(ex));
                return false;
            }
        }
        public async Task GetAllLocation()
        {
            ShowLoading();
            if (CheckConnection())
            {
                try
                {
                    locList = new List<NewLocationDataResponse>();
                    var result = await TryWithErrorAsync(_authenticationService.GetAllLocationsNew(), true, false);
                    if (result?.ResultType == ResultType.Ok && result?.Data?.data != null)
                    {
                        foreach (var item in result?.Data?.data)
                        {
                            locList.Add(item);
                        }
                    }
                    else
                    {
                        HideLoading();
                        ShowAlert(result?.Data?.message);
                    }
                }
                catch (Exception ex)
                {
                    HideLoading();
                }
                HideLoading();
                if (locList != null && locList.Count > 0)
                {
                    AllLocationList = new ObservableCollection<NewLocationDataResponse>(locList);
                    AllLocationListCode = new ObservableCollection<NewLocationDataResponse>(locList.GroupBy(x => x.country_code).Select(y => y.First()));
                    if (AllLocationListCode != null && AllLocationListCode.Count > 0)
                    {
                        CountriesTitle = $"+{AllLocationListCode.First().country_code}";
                    }
                }
            }
            else
                ShowToast(CommonMessages.NoInternet);
        }
        private async Task GetOTPCommandExecute()
        {
            if (!UserEnteredAllRecord())
            {
                return;
            }
            if (CheckConnection())
            {
                ShowLoading();
                try
                {
                    RegistrationData = new RegistrationData();
                    if (!string.IsNullOrEmpty(PhoneNumber))
                    {
                        RegistrationData.mobile = PhoneNumber;
                    }
                    if (!string.IsNullOrEmpty(CountriesTitle))
                    {
                        RegistrationData.countrycode = CountriesTitle;
                    }
                    string hashKey = System.Web.HttpUtility.UrlEncode(DependencyService.Get<IHashService>().GenerateHashkey());
                    var otp = CommonMethod.GenerateOTP().ToString();

                    string message = $"<#> Your OTP is {otp} {hashKey}";

                    var form = new MultipartFormDataContent();
                    form.Add(new StringContent(CountriesTitle), "country_code");
                    form.Add(new StringContent(PhoneNumber), "mobile");
                    form.Add(new StringContent(message), "message");
                    form.Add(new StringContent(otp), "otp");

                    var result = await TryWithErrorAsync(_authenticationService.SendOTPService(form), true, false);
                    if (result?.ResultType == ResultType.Ok && result?.Data?.status == 200)
                    {
                        RegistrationData.Otp = result?.Data?.otp;
                        RegistrationData.isfromforgotpassword = true;
                        DependencyService.Get<IHashService>().StartSMSRetriverReceiver();

                        if (_navigationService.GetCurrentPageViewModel() != typeof(OTPPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<OTPPageViewModel>();
                            await App.Locator.OTPPage.InitilizeData();
                        }
                    }
                    else
                    {
                        ShowAlert(result?.Data?.message);
                    }
                }
                catch (Exception ex)
                {
                    ShowToast(CommonMessages.ServerError);
                }
                finally
                {
                    HideLoading();
                }
            }
            else
                ShowToast(CommonMessages.NoInternet);
        }
    }
}
