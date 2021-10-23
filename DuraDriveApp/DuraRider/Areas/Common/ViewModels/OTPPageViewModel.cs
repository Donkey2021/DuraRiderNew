using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Helpers;
using DuraRider.Services;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DuraRider.Areas.Common.ViewModels
{
    public class OTPPageViewModel : AppBaseViewModel
    {
        #region localVariable
        private bool forgotPassword;
        public IAsyncCommand DoneCommand { get; set; }
        public IAsyncCommand ResendOTPCommand { get; set; }
        private string _oTPText;
        private string _passcode4;
        private string _passcode3;
        private string _passcode2;
        private string _passcode1;
        private string _phoneNumber;
        private bool _errorMessageVisibility;
        #endregion
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public OTPPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            DoneCommand = new AsyncCommand(DoneCommandExecute, allowsMultipleExecutions: false);
            ResendOTPCommand = new AsyncCommand(ResendOTPCommandExecute, allowsMultipleExecutions: false);
            RegistrationData = App.Locator.SignUpPage.RegistrationData;
            PhoneNumber = $"{RegistrationData.countrycode}{RegistrationData.mobile}";
            OTPText = RegistrationData.Otp;
            DependencyService.Get<IHashService>().StartSMSRetriverReceiver();
            MessagingCenter.Subscribe<string>(this, "ReceivedOTP", (message) =>
            {
                string[] words = message.Split();
                foreach (string item in words.ToList())
                {
                    var isNumeric = int.TryParse(item, out int n);
                    if (isNumeric)
                    {
                        Passcode1 = item[0].ToString();
                        Passcode2 = item[1].ToString();
                        Passcode3 = item[2].ToString();
                        Passcode4 = item[3].ToString();
                        break;
                    }
                }
            });
        }
        #region Method
        //async Task Done()
        //{
        //    if (UserEnteredAllRecord())
        //    {
        //        await RichNavigation.PushAsync(new NewPasswordPage(), typeof(NewPasswordPage));
        //    }
        //}



        private async Task ProceedWithRegistration()
        {
            if (!UserEnteredAllRecord())
            {
                return;
            }
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                var form = new MultipartFormDataContent();
                form.Add(new StringContent(RegistrationData.countrycode), "country_code");
                form.Add(new StringContent(RegistrationData.mobile), "mobile");
                form.Add(new StringContent(RegistrationData.email), "email");
                form.Add(new StringContent(RegistrationData.password), "password");
                form.Add(new StringContent(string.Empty), "firstname");
                form.Add(new StringContent(string.Empty), "middlename");
                form.Add(new StringContent(string.Empty), "lastname");
                form.Add(new StringContent(string.Empty), "dob");
                //form.Add(new StringContent(string.Empty), "is_manager");
                form.Add(new StringContent(string.Empty), "manager_account_no");
                //form.Add(new StringContent(string.Empty), "referralcode");
                form.Add(new StringContent(string.Empty), "g_cash_accont_name");
                form.Add(new StringContent(string.Empty), "g_cash_no");
                form.Add(new StringContent(string.Empty), "profilephoto_url");
                form.Add(new StringContent(string.Empty), "vehiclephoto");
                form.Add(new StringContent(string.Empty), "cr_no");
                form.Add(new StringContent(string.Empty), "police_clearance_no");
                form.Add(new StringContent(string.Empty), "backlicensephoto");
                form.Add(new StringContent(string.Empty), "frontlicensephoto");
                form.Add(new StringContent(string.Empty), "isbusinessaccout");
                form.Add(new StringContent(string.Empty), "vehicle_id");
                form.Add(new StringContent(string.Empty), "licence_no");
                form.Add(new StringContent(string.Empty), "vehicle_id");
                form.Add(new StringContent(string.Empty), "latitude");
                form.Add(new StringContent(string.Empty), "longitude");
                form.Add(new StringContent(string.Empty), "crno_image");
                form.Add(new StringContent(string.Empty), "police_clearance_image");
                form.Add(new StringContent(string.Empty), "dura_bag_id");
                var result = await TryWithErrorAsync(_authenticationService.DriverRegistrationService(form), true, false);
                if (result?.ResultType == ResultType.Ok && result?.Data?.status == 200)
                {
                    ShowToast("Your account is successfully registered.Please complete your profile section.");
                    RegistrationData.driverid = result?.Data?.data?.driverData?.id;
                    RegistrationData.token = result?.Data?.data?.token.original.token;
                    await SecureStorage.SetAsync("token", result?.Data?.data?.token.original.token);
                    await SecureStorage.SetAsync("driverid", result?.Data?.data?.driverData?.id.ToString());
                    if (_navigationService.GetCurrentPageViewModel() != typeof(PersonalInfoPageViewModel))
                    {
                        await _navigationService.NavigateToAsync<PersonalInfoPageViewModel>();
                        await App.Locator.OTPPage.InitilizeData();
                    }
                }
                else if (result?.Data?.status == 409)
                {
                    ShowToast("Your account is already with us.Please check to login.");
                }
                else if (result?.Data?.status == 405)
                {
                    await App.Current.MainPage.DisplayAlert("DuraAlert", "The OTP entered is incorrect. Please enter correct OTP or try regenerating the OTP.", "Ok");
                }
                else
                {
                    ShowToast("Register-API Error.");
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

        private bool UserEnteredAllRecord()
        {
            try
            {
                OTPText = Passcode1 + Passcode2 + Passcode3 + Passcode4;
                ErrorMessage = "";
                ErrorMessage = string.IsNullOrEmpty(OTPText) ? ErrorMessages.OTPEmpty : ErrorMessage;
                if (!string.IsNullOrEmpty(OTPText))
                {
                    ErrorMessage = OTPText.Length != 4 ? ErrorMessages.OTPWrong : ErrorMessage;
                }
                if (ErrorMessage != "")
                {
                    ErrorMessageVisibility = true;
                }
                return ErrorMessage == "";
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
        private async Task ResendOTPCommandExecute()
        {
            string hashKey = System.Web.HttpUtility.UrlEncode(DependencyService.Get<IHashService>().GenerateHashkey());
            var otp = CommonMethod.GenerateOTP().ToString();

            string message = $"<#> Your OTP is {otp} {hashKey}";

            var form = new MultipartFormDataContent();
            form.Add(new StringContent(RegistrationData.countrycode), "country_code");
            form.Add(new StringContent(RegistrationData.mobile), "mobile");
            form.Add(new StringContent(message), "message");
            form.Add(new StringContent(otp), "otp");

            var result = await TryWithErrorAsync(_authenticationService.SendOTPService(form), true, false);
            if (result?.ResultType == ResultType.Ok && result?.Data?.status == 200)
            {
                RegistrationData.Otp = result?.Data?.otp;
                ShowToast("OTP Resend Successfully.");
                DependencyService.Get<IHashService>().StartSMSRetriverReceiver();
            }
            else
            {
                ShowAlert("Please check your Phone number.It may be not reachable.");
            }
        }

        private async Task DoneCommandExecute()
        {
            if (!UserEnteredAllRecord())
            {
                return;
            }
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                ShowLoading();
                var form = new MultipartFormDataContent();
                form.Add(new StringContent(RegistrationData.countrycode), "country_code");
                form.Add(new StringContent(RegistrationData.mobile), "mobile");
                form.Add(new StringContent(OTPText), "otp");
                var result = await TryWithErrorAsync(_authenticationService.VerifyOTPService(form), true, false);
                if (result?.ResultType == ResultType.Ok && result?.Data?.status == 200)
                {
                    ShowToast("Your OTP verification is successfully done.");
                    await ProceedWithRegistration();
                }
                else
                {
                    ShowToast("OTP Sending- API Error.");
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

        }
        #region Properties
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

        public string Passcode4
        {
            get => _passcode4;
            set => SetProperty(ref _passcode4, value);
        }
        public string Passcode3
        {
            get => _passcode3;
            set => SetProperty(ref _passcode3, value);
        }
        public string Passcode2
        {
            get => _passcode2;
            set => SetProperty(ref _passcode2, value);
        }
        public string Passcode1
        {
            get => _passcode1;
            set => SetProperty(ref _passcode1, value);
        }
        public string OTPText
        {
            get => _oTPText;
            set => SetProperty(ref _oTPText, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }
        public bool ErrorMessageVisibility
        {
            get => _errorMessageVisibility;
            set => SetProperty(ref _errorMessageVisibility, value);
        }
        #endregion
    }
}
