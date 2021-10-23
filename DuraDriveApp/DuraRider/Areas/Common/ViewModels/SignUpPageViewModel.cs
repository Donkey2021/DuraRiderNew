using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using App.User.LocationInfo.Services;
using DuraRider.Areas.Common.Views;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Models.Common;
using DuraRider.Core.Models.RequestModels;
using DuraRider.Core.Models.ResponseModels;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Helpers;
using DuraRider.Services;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DuraRider.Areas.Common.ViewModels
{
    public class SignUpPageViewModel : AppBaseViewModel
    {
        #region Private Prop

        private string _countriesTitle;
        private IAuthenticationService _authenticationService;
        private ObservableCollection<Countries> _countriesList;
        private string _countryCodeText;
        private string _passwordText;
        private string _cnfpasswordText;
        private string _passwordError;
        private string _cnfpasswordError;
        private string _emailText;
        private string _logEmailError;
        private string _mobileText;
        private string _phoneError;
        private bool _passwordErrorVisibility;
        private bool _cnfpasswordErrorVisibility;
        private bool _phoneErrorVisibility;
        private bool _isTermsConditionChecked;
        private List<NewLocationDataResponse> locList;

        #region Properties
        public IAsyncCommand CheckCommand { get; set; }
        public IAsyncCommand SendOTPCommand { get; set; }
        public ObservableCollection<string> CountryCodeList { get; set; } = new ObservableCollection<string>
        {
            "+ 63","+ 91"
        };
        private ObservableCollection<NewLocationDataResponse> _allLocationList;
        public ObservableCollection<NewLocationDataResponse> AllLocationList
        {
            get { return _allLocationList; }
            set
            {
                SetProperty(ref _allLocationList, value);
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
        #endregion
        #endregion

        INavigationService _navigationService;
        public SignUpPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            CheckCommand = new AsyncCommand(CheckCommandExecute, allowsMultipleExecutions: false);
            SendOTPCommand = new AsyncCommand(SendOTPCommandExecute, allowsMultipleExecutions: false);
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
        private async Task SendOTPCommandExecute()
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
                    if (!string.IsNullOrEmpty(EmailText))
                    {
                        RegistrationData.email = EmailText;
                    }
                    if (!string.IsNullOrEmpty(PasswordText))
                    {
                        RegistrationData.password = PasswordText;
                    }
                    if (!string.IsNullOrEmpty(CnfPasswordText))
                    {
                        RegistrationData.password_confirmation = CnfPasswordText;
                    }
                    if (!string.IsNullOrEmpty(MobileText))
                    {
                        RegistrationData.mobile = MobileText;
                    }
                    if (!string.IsNullOrEmpty(MobileText))
                    {
                        RegistrationData.countrycode = CountriesTitle;
                    }
                    if (IsTermsConditionChecked == false)
                    {
                        ShowToast("Please accept terms and conditions of Duradrive Technologies Inc. to proceed further.");
                        return;
                    }

                    string hashKey = System.Web.HttpUtility.UrlEncode(DependencyService.Get<IHashService>().GenerateHashkey());
                    var otp = CommonMethod.GenerateOTP().ToString();

                    string message = $"<#> Your OTP is {otp} {hashKey}";

                    var form = new MultipartFormDataContent();
                    form.Add(new StringContent(CountriesTitle), "country_code");
                    form.Add(new StringContent(MobileText), "mobile");
                    form.Add(new StringContent(message), "message");
                    form.Add(new StringContent(otp), "otp");

                    var result = await TryWithErrorAsync(_authenticationService.SendOTPService(form), true, false);
                    if (result?.ResultType == ResultType.Ok && result?.Data?.status == 200)
                    {

                        RegistrationData.Otp = result?.Data?.otp;
                        bool forgotPassword = false;
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

        private async Task CheckCommandExecute()
        {
            Uri uri = new Uri(Constant.Terms_Condition);
            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Hide,
                PreferredToolbarColor = Color.Blue,
                PreferredControlColor = Color.Black
            });

        }

        internal async Task InitilizeData()
        {
            await GetAllLocation();
        }
        #region Public Propeties

        //public async Task GetCountryCode()
        //{
        //    try
        //    {
        //        string jsonFileName = "countriescode.json";

        //        CountriesList = new ObservableCollection<Countries>();
        //        var assembly = typeof(SignUpPage).GetTypeInfo().Assembly;
        //        Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.MockData.{jsonFileName}");
        //        using (var reader = new System.IO.StreamReader(stream))
        //        {
        //            var jsonString = reader.ReadToEnd();
        //            CountriesList = JsonConvert.DeserializeObject<ObservableCollection<Countries>>(jsonString);
        //        }

        //        var country_name = await TrackingService.GetUserCountryNameAsync();
        //        GetCountryBasedOnCurrentCountry("Philippines");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private Countries GetCountryBasedOnCurrentCountry(string countryname)
        //{
        //    Countries countries = new Countries();

        //    if (!string.IsNullOrEmpty(countryname))
        //    {
        //        if (CountriesList != null && CountriesList.Count > 0)
        //        {
        //            countries = CountriesList.Where(x => x.name == countryname).FirstOrDefault();
        //            SelectedCountries = countries;
        //            CountriesTitle = SelectedCountries.dial_code;
        //        }
        //    }
        //    return countries;
        //}

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

        public string CountryCodeText
        {
            get
            {
                return _countryCodeText;
            }
            set
            {
                SetProperty(ref _countryCodeText, value);
            }
        }


        public string PasswordText
        {
            get
            {
                return _passwordText;
            }
            set
            {
                SetProperty(ref _passwordText, value);
            }
        }

        public string CnfPasswordText
        {
            get
            {
                return _cnfpasswordText;
            }
            set
            {
                SetProperty(ref _cnfpasswordText, value);
            }
        }


        public string PasswordError
        {
            get
            {
                return _passwordError;
            }
            set
            {
                SetProperty(ref _passwordError, value);
            }
        }

        public string CnfPasswordError
        {
            get
            {
                return _cnfpasswordError;
            }
            set
            {
                SetProperty(ref _cnfpasswordError, value);
            }
        }

        public string EmailText
        {
            get
            {
                return _emailText;
            }
            set
            {
                SetProperty(ref _emailText, value);
            }
        }

        public string LogEmailError
        {
            get
            {
                return _logEmailError;
            }
            set
            {
                SetProperty(ref _logEmailError, value);
            }
        }

        public string MobileText
        {
            get
            {
                return _mobileText;
            }
            set
            {
                SetProperty(ref _mobileText, value);
            }
        }


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

        public bool PasswordErrorVisibility
        {
            get
            {
                return _passwordErrorVisibility;
            }
            set
            {
                SetProperty(ref _passwordErrorVisibility, value);
            }
        }
        public bool IsTermsConditionChecked
        {
            get
            {
                return _isTermsConditionChecked;
            }
            set
            {
                SetProperty(ref _isTermsConditionChecked, value);
            }
        }
        public bool CnfPasswordErrorVisibility
        {
            get
            {
                return _cnfpasswordErrorVisibility;
            }
            set
            {
                SetProperty(ref _cnfpasswordErrorVisibility, value);
            }
        }

        private bool _logEmailErrorVisibility;
        public bool LogEmailErrorVisibility
        {
            get
            {
                return _logEmailErrorVisibility;
            }
            set
            {
                SetProperty(ref _logEmailErrorVisibility, value);
            }
        }


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
        #endregion

        #region method

        public bool UserEnteredAllRecord()
        {
            try
            {
                PhoneErrorVisibility = false;
                LogEmailErrorVisibility = false;
                PasswordErrorVisibility = false;
                CnfPasswordErrorVisibility = false;

                PhoneError = "";
                PhoneError = string.IsNullOrEmpty(MobileText) ? ErrorMessages.MobileTextEmpty : PhoneError;
                if (!string.IsNullOrEmpty(MobileText))
                {
                    PhoneError = !RegexUtilities.PhoneNumberValidation(MobileText) ? ErrorMessages.PhoneWrong : PhoneError;
                }
                if (!string.IsNullOrEmpty(PhoneError))
                {
                    PhoneErrorVisibility = true;
                    return PhoneError == "";
                }


                LogEmailError = "";
                LogEmailError = string.IsNullOrEmpty(EmailText) ? ErrorMessages.EmailTextEmpty : LogEmailError;
                if (!string.IsNullOrEmpty(EmailText))
                {
                    LogEmailError = !RegexUtilities.EmailValidation(EmailText) ? ErrorMessages.EmailWrong : LogEmailError;
                }
                if (!string.IsNullOrEmpty(LogEmailError))
                {
                    LogEmailErrorVisibility = true;
                    return LogEmailError == "";
                }

                PasswordError = "";
                PasswordError = string.IsNullOrEmpty(PasswordText) ? ErrorMessages.PasswordEmpty : PasswordError;
                if (!string.IsNullOrEmpty(PasswordText))
                {
                    PasswordError = !Regex.IsMatch(PasswordText, "(?=.*[A-Z])(?=.*\\d)(?=.*[¡!@#$%*.¿?\\-_.\\(\\)])[A-Za-z\\d¡!@#$%*.¿?\\-\\(\\)&]{8,20}") ? ErrorMessages.PasswordWrong : PasswordError; /*Password.Length < 6 ? ErrorMessages.PasswordLength : ErrorMessage;*/

                    //PasswordError = !RegexUtilities.PasswordValidation(PasswordText) ? ErrorMessages.PasswordWrong : PasswordError;
                }
                if (!string.IsNullOrEmpty(PasswordError))
                {
                    PasswordErrorVisibility = true;
                    return PasswordError == "";
                }
                CnfPasswordError = "";
                CnfPasswordError = string.IsNullOrEmpty(CnfPasswordText) ? ErrorMessages.CnfPasswordEmpty : CnfPasswordError;
                if (!string.IsNullOrEmpty(CnfPasswordText))
                {
                    CnfPasswordError = CnfPasswordText != PasswordText ? ErrorMessages.CnfPasswordWrong : CnfPasswordError; /*Password.Length < 6 ? ErrorMessages.PasswordLength : ErrorMessage;*/

                    //PasswordError = !RegexUtilities.PasswordValidation(PasswordText) ? ErrorMessages.PasswordWrong : PasswordError;
                }
                if (!string.IsNullOrEmpty(CnfPasswordError))
                {
                    CnfPasswordErrorVisibility = true;
                    return CnfPasswordError == "";
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}
