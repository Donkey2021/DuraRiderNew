using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.User.LocationInfo.Services;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Common;
using DuraRider.Core.Models.ResponseModels;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace DuraRider.Areas.Common.ViewModels
{
    public class LoginPageViewModels : AppBaseViewModel
    {
        INavigationService _navigationService;
        public IAsyncCommand NavigateToHomeCommand { get; set; }
        public IAsyncCommand RegisterCommand { get; set; }
        private string _buttonText = "Login With Email";
        private bool _isVisibleEmailLayout;
        private bool _isVisiblePhoneLayout = true;
        private List<NewLocationDataResponse> locList;
        public IAsyncCommand<object> SwitchToEmailLayout { get; set; }
        private ObservableCollection<NewLocationDataResponse> _allLocationList;
        private bool _isAreaErrorVisible;
        private bool _emailNotValid;
        private bool _passwordNotValid;
        private bool _phoneNumberNotValid;
        private bool _isLoginButtoinEnabled;
        private IAuthenticationService _authenticationService;

        private bool _isPhoneErrorVisible;
        public bool IsPhoneErrorVisible
        {
            get { return _isPhoneErrorVisible; }
            set { _isPhoneErrorVisible = value; OnPropertyChanged(nameof(IsPhoneErrorVisible)); }
        }
        private string _mobileNumber;
        private string _email;
        private string _password;
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                _mobileNumber = value;
                if (!string.IsNullOrEmpty(_mobileNumber))
                {
                    if (RegexUtilities.PhoneNumberValidation(_mobileNumber) && _mobileNumber.Length >= 10)
                    {
                        if (_mobileNumber.Contains("."))
                        {
                            IsPhoneErrorVisible = true;
                        }
                        else
                        {
                            IsPhoneErrorVisible = false;
                        }
                    }
                    else
                    {
                        IsPhoneErrorVisible = true;
                    }
                    CheckLoginValidation();
                }
                else
                {
                    IsPhoneErrorVisible = false;
                }


                this.OnPropertyChanged(nameof(MobileNumber));
            }
        }

        public LoginPageViewModels(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            SwitchToEmailLayout = new AsyncCommand<object>(SwitchCommandExecute, allowsMultipleExecutions: false);
            NavigateToHomeCommand = new AsyncCommand(LoginAndNavigateToHome, allowsMultipleExecutions: false);
            RegisterCommand = new AsyncCommand(RegisterCommandExecute, allowsMultipleExecutions: false);
        }

        private async Task LoginAndNavigateToHome()
        {

        }

        private async Task RegisterCommandExecute()
        {
            if (_navigationService.GetCurrentPageViewModel() != typeof(SignUpPageViewModel))
            {
                await _navigationService.NavigateToAsync<SignUpPageViewModel>();
                await App.Locator.SignUpPage.InitilizeData();
            }
        }
        private bool CheckLoginValidation()
        {
            if (IsVisiblePhoneNumberLayout)
            {
                if (string.IsNullOrEmpty(MobileNumber) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(SelectedLocation?.country_name))
                {
                    //ShowToast("Please again select Location.");
                    IsLoginButtoinEnabled = false;
                    return false;
                }
                else if (IsAreaErrorVisible || IsPhoneErrorVisible)
                {
                    IsLoginButtoinEnabled = false;
                    return false;
                }
                else
                {
                    IsLoginButtoinEnabled = true;
                    return true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(SelectedLocation?.country_name))
                {
                    //ShowToast("Please again select Location.");
                    IsLoginButtoinEnabled = false;
                    return false;
                }
                else if (IsAreaErrorVisible || IsEmailErrorVisible)
                {
                    IsLoginButtoinEnabled = false;
                    return false;
                }
                else
                {
                    IsLoginButtoinEnabled = true;
                    return true;
                }
            }
        }
        private bool _isEmailErrorVisible;
        public bool IsEmailErrorVisible
        {
            get { return _isEmailErrorVisible; }
            set { _isEmailErrorVisible = value; OnPropertyChanged(nameof(IsEmailErrorVisible)); }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                if (!string.IsNullOrEmpty(_email))
                {
                    if (RegexUtilities.EmailValidation(_email))
                        IsEmailErrorVisible = false;
                    else
                        IsEmailErrorVisible = true;
                    CheckLoginValidation();
                }
                else
                {
                    IsEmailErrorVisible = false;
                }
                this.OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                CheckLoginValidation();
                this.OnPropertyChanged(nameof(Password));
            }
        }
        private string _countriesTitle;
        private NewLocationDataResponse _selectedCountries;
        public NewLocationDataResponse SelectedCountries
        {
            get { return _selectedCountries; }
            set
            {
                _selectedCountries = value;
                try
                {
                    this.OnPropertyChanged(nameof(SelectedCountries));
                }
                catch (Exception ex) { }
            }
        }
        private NewLocationDataResponse _selectedCountriesCode;
        public NewLocationDataResponse SelectedCountriesCode
        {
            get { return _selectedCountriesCode; }
            set
            {
                _selectedCountriesCode = value;
                try
                {
                    this.OnPropertyChanged(nameof(SelectedCountriesCode));
                    //this.OnPropertyChanged(nameof(CountriesTitle));
                    CountriesTitleCode = $"+{_selectedCountriesCode.country_code}";
                }
                catch (Exception ex) { }
            }
        }
        private ObservableCollection<AreaData> _serviceAreaList;
        public ObservableCollection<AreaData> ServiceAreaList
        {
            get { return _serviceAreaList; }
            set
            {
                _serviceAreaList = value;
                if (ServiceAreaList.Count == 0)
                {
                    area = "No service area";
                }
                this.OnPropertyChanged(nameof(ServiceAreaList));
            }
        }

        private string _area;
        public string area
        {
            get { return _area; }
            set
            {
                _area = value;
                this.OnPropertyChanged(nameof(area));
            }


        }
        private bool _isPhoneNumberEmpty;
        public bool IsPhoneNumberEmpty
        {
            get { return _isPhoneNumberEmpty; }
            set
            {
                _isPhoneNumberEmpty = value;
                OnPropertyChanged(nameof(IsPhoneNumberEmpty));
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
        private string _countriesTitleCode;
        public string CountriesTitleCode
        {
            get { return _countriesTitleCode; }
            set
            {
                _countriesTitleCode = value;
                this.OnPropertyChanged("CountriesTitleCode");
            }
        }
        private ObservableCollection<Countries> _countriesList;
        public ObservableCollection<Countries> CountriesList
        {
            get { return _countriesList; }
            set
            {
                _countriesList = value;
                this.OnPropertyChanged(nameof(CountriesList));
            }
        }
        public ObservableCollection<NewLocationDataResponse> AllLocationList
        {
            get { return _allLocationList; }
            set
            {
                SetProperty(ref _allLocationList, value);
            }
        }

        private NewLocationDataResponse _selectedLocation;
        public NewLocationDataResponse SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                if (_selectedLocation != null)
                {
                    if (RegexUtilities.EmptyString(_selectedLocation.country_name))
                    {
                        this.OnPropertyChanged(nameof(CountriesTitle));
                        CountriesTitle = $"+{_selectedLocation.country_code}";
                        IsAreaErrorVisible = false;
                    }
                    else
                        IsAreaErrorVisible = true;
                }
                CheckLoginValidation();
                this.OnPropertyChanged(nameof(SelectedLocation));
            }
        }
        private NewLocationDataResponse _selectedLocationTemp;
        public NewLocationDataResponse SelectedLocationTemp
        {
            get { return _selectedLocationTemp; }
            set
            {
                _selectedLocationTemp = value;
                this.OnPropertyChanged(nameof(SelectedLocationTemp));
            }
        }
        public bool IsAreaErrorVisible
        {
            get { return _isAreaErrorVisible; }
            set { _isAreaErrorVisible = value; OnPropertyChanged(nameof(IsAreaErrorVisible)); }
        }
        public bool EmailNotValid
        {
            get { return _emailNotValid; }
            set { _emailNotValid = value; OnPropertyChanged(nameof(EmailNotValid)); }
        }
        public bool PasswordNotValid
        {
            get { return _passwordNotValid; }
            set { _passwordNotValid = value; OnPropertyChanged(nameof(PasswordNotValid)); }
        }
        public bool PhoneNumberNotValid
        {
            get { return _phoneNumberNotValid; }
            set { _phoneNumberNotValid = value; OnPropertyChanged(nameof(PhoneNumberNotValid)); }
        }
        public bool IsLoginButtoinEnabled
        {
            get { return _isLoginButtoinEnabled; }
            set { _isLoginButtoinEnabled = value; OnPropertyChanged(nameof(IsLoginButtoinEnabled)); }
        }
        public string ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = value; OnPropertyChanged(nameof(ButtonText)); }
        }
        public bool IsVisiblePhoneNumberLayout
        {
            get { return _isVisiblePhoneLayout; }
            set { _isVisiblePhoneLayout = value; OnPropertyChanged(nameof(IsVisiblePhoneNumberLayout)); }
        }
        public bool IsVisibleEmailLayout
        {
            get { return _isVisibleEmailLayout; }
            set { _isVisibleEmailLayout = value; OnPropertyChanged(nameof(IsVisibleEmailLayout)); }
        }

        private string _appVersion;
        public string AppVersion
        {
            get { return _appVersion; }
            set
            {
                _appVersion = value;
                OnPropertyChanged(nameof(AppVersion));
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
        private async Task SwitchCommandExecute(object arg)
        {
            var data = arg as LoginPageViewModels;
            if (data != null)
            {
                if (data.ButtonText == "Login With Email")
                {
                    IsVisibleEmailLayout = true;
                    IsVisiblePhoneNumberLayout = false;
                    ButtonText = "Login With Phone Number";
                }
                else
                {
                    IsVisibleEmailLayout = false;
                    IsVisiblePhoneNumberLayout = true;
                    ButtonText = "Login With Email";
                }
                //Password = string.Empty; 
                CheckLoginValidation();
            }
        }
        public async Task GetAllLocation()
        {
            if (CheckConnection())
            {

                try
                {
                    locList = new List<NewLocationDataResponse>();
                    ShowLoading();
                    var result = await TryWithErrorAsync(_authenticationService.GetAllLocationsNew(), true, false);
                    HideLoading();
                    if (result?.ResultType == ResultType.Ok && result?.Data?.data != null)
                    {
                        foreach (var item in result?.Data?.data)
                        {
                            locList.Add(item);
                        }
                    }
                    else
                    {
                        ShowAlert(result?.Data?.message);
                    }
                }
                catch (Exception ex)
                {
                    //ShowToast(CommonMessages.ServerError);
                }
                if (locList != null && locList.Count > 0)
                {
                    AllLocationList = new ObservableCollection<NewLocationDataResponse>(locList);


                    AllLocationListCode = new ObservableCollection<NewLocationDataResponse>(locList.GroupBy(x => x.country_code).Select(y => y.First()));
                    ShowLoading();
                    var country_code = await TrackingService.GetUserCountryCodeAsync();
                    int id = 0;
                    try
                    {
                        id = Convert.ToInt32(Preferences.Get("LocationKey", ""));
                    }
                    catch (Exception ex) { id = 0; }
                    if (id != 0)
                        SelectedCountries = AllLocationList.Where(x => x.id == Convert.ToInt32(id)).FirstOrDefault();
                    HideLoading();
                    if (country_code != null)
                    {
                        var countryData = AllLocationList.Where(x => x.iso == "PH").FirstOrDefault();
                        if (countryData != null)
                        {
                            SelectedLocation = countryData;
                            SelectedLocationTemp = countryData;
                            CountriesTitleCode = countryData != null ? $"+{countryData?.country_code}" : "";
                        }
                    }

                }

            }
            else
                ShowToast(CommonMessages.NoInternet);
        }

        public async Task GetServiceArea()
        {
            if (string.IsNullOrEmpty(CountriesTitle))
            {
                ShowToast("Please select country to get service area");
                return;
            }
            if (CheckConnection())
            {
                ShowLoading();
                try
                {
                    var form = new MultipartFormDataContent();
                    form.Add(new StringContent(CountriesTitle), "country_code");
                    var result = await TryWithErrorAsync(_authenticationService.GetServiceArea(form), true, false);

                    if (result?.ResultType == ResultType.Ok && result?.Data?.data != null)
                    {
                        if (result?.Data?.data?.areaData.Count > 0)
                        {
                            ServiceAreaList = new ObservableCollection<AreaData>();
                            foreach (var item in result?.Data?.data?.areaData)
                            {
                                ServiceAreaList.Add(item);
                            }
                        }
                    }
                    else
                    {
                        ShowAlert(result?.Data?.message);
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
            else
                ShowToast(CommonMessages.NoInternet);
        }

        public async Task InitilizeData()
        {
            Email = Password = MobileNumber = string.Empty;
            IsAreaErrorVisible = false;
            IsPhoneErrorVisible = false;
            CheckLoginValidation();
        }
    }
}
