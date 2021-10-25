using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DuraRider.Areas.Common.ViewModels
{
    public class ResetPasswordPageViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand UpdatePasswordCommand { get; set; }
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
        #region Properties

        private string _createPasscode;
        public string CreatePasscode
        {
            get => _createPasscode;
            set => SetProperty(ref _createPasscode, value);
        }
        private string _confirmPasscode;
        public string ConfirmPasscode
        {
            get => _confirmPasscode;
            set => SetProperty(ref _confirmPasscode, value);
        }
        private string _passwordError;
        public string PasswordError
        {
            get => _passwordError;
            set => SetProperty(ref _passwordError, value);
        }

        private string _cnfPasswordError;
        public string CnfPasswordError
        {
            get => _cnfPasswordError;
            set => SetProperty(ref _cnfPasswordError, value);
        }

        private bool _cnfPasswordErrorVisibility;
        public bool CnfPasswordErrorVisibility
        {
            get => _cnfPasswordErrorVisibility;
            set => SetProperty(ref _cnfPasswordErrorVisibility, value);
        }

        private bool _passwordErrorVisibility;
        public bool PasswordErrorVisibility
        {
            get => _passwordErrorVisibility;
            set => SetProperty(ref _passwordErrorVisibility, value);
        }

        #endregion

        #region Method
        public bool UserEnteredAllRecord()
        {
            try
            {
                PasswordErrorVisibility = false;
                CnfPasswordErrorVisibility = false;
                PasswordError = "";
                PasswordError = string.IsNullOrEmpty(CreatePasscode) ? ErrorMessages.PasswordEmpty : PasswordError;
                if (!string.IsNullOrEmpty(CreatePasscode))
                {
                    PasswordError = !Regex.IsMatch(CreatePasscode, "(?=.*[A-Z])(?=.*\\d)(?=.*[¡!@#$%*.¿?\\-_.\\(\\)])[A-Za-z\\d¡!@#$%*.¿?\\-\\(\\)&]{8,20}") ? ErrorMessages.PasswordWrong : PasswordError; /*Password.Length < 6 ? ErrorMessages.PasswordLength : ErrorMessage;*/

                    //PasswordError = !RegexUtilities.PasswordValidation(PasswordText) ? ErrorMessages.PasswordWrong : PasswordError;
                }
                if (!string.IsNullOrEmpty(PasswordError))
                {
                    PasswordErrorVisibility = true;
                    return PasswordError == "";
                }
                CnfPasswordError = "";
                CnfPasswordError = string.IsNullOrEmpty(ConfirmPasscode) ? ErrorMessages.CnfPasswordEmpty : CnfPasswordError;
                if (!string.IsNullOrEmpty(ConfirmPasscode))
                {
                    CnfPasswordError = ConfirmPasscode != CreatePasscode ? ErrorMessages.CnfPasswordWrong : CnfPasswordError; /*Password.Length < 6 ? ErrorMessages.PasswordLength : ErrorMessage;*/

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
                return true;
            }
        }
        #endregion
        public ResetPasswordPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            UpdatePasswordCommand = new AsyncCommand(UpdatePasswordCommandExecute, allowsMultipleExecutions: false);
            RegistrationData = App.Locator.GCashAccountDetailsPage.RegistrationData;
        }

        private async Task UpdatePasswordCommandExecute()
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
                var countrycode = string.IsNullOrEmpty(RegistrationData.countrycode) ? string.Empty : RegistrationData.countrycode;
                var mobile = string.IsNullOrEmpty(RegistrationData.mobile) ? string.Empty : RegistrationData.mobile;
                var password = string.IsNullOrEmpty(CreatePasscode) ? string.Empty : CreatePasscode;

                var form = new MultipartFormDataContent();
                form.Add(new StringContent(countrycode), "country_code");
                form.Add(new StringContent(mobile), "mobile");
                form.Add(new StringContent(password), "new_password");
                var result = await TryWithErrorAsync(_authenticationService.ForgetPassword(form), true, false);
                if (result?.ResultType == ResultType.Ok && result?.Data?.status == 200)
                {
                    //ShowToast("Your OTP verification is successfully done.");
                }
                else
                {
                    ShowToast("Error on updating password");
                }
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message.ToString());
            }
            finally
            {
                HideLoading();
            }
        }

        internal async Task InitilizeData()
        {

        }
    }
}
