using System;
using System.Net.Http;
using System.Threading.Tasks;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Helpers;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DuraRider.Areas.Common.ViewModels
{
    public class GCashAccountDetailsPageViewModel : AppBaseViewModel
    {
        #region PrivateProp

        private string _accountNameText;
        private string _gCNumberText;
        private string _reapetGCNumberText;

        #endregion
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
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


        public GCashAccountDetailsPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            SubmitCommand = new AsyncCommand(SubmitCommandExecute, allowsMultipleExecutions: false);
            RegistrationData = App.Locator.DocumentPage.RegistrationData;
        }

        private async Task SubmitCommandExecute()
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
                    var currentLoc = await LocationHelpers.getCurrentPosition();
                    if (currentLoc == null)
                    {
                        ShowToast("Current location not found, try again");
                        return;
                    }

                    var accountname = string.IsNullOrEmpty(AccountNameText) ? string.Empty : AccountNameText;
                    var driverid = RegistrationData.driverid == 0 ? 0 : RegistrationData.driverid;
                    var latitude = currentLoc.Latitude == 0 ? 0 : currentLoc.Latitude;
                    var longitude = currentLoc.Longitude == 0 ? 0 : currentLoc.Longitude;
                    var gcashnumber = string.IsNullOrEmpty(ReapetGCNumberText) ? string.Empty : ReapetGCNumberText;

                    var form = new MultipartFormDataContent();
                    form.Add(new StringContent(Convert.ToString(accountname)), "g_cash_accont_name");
                    form.Add(new StringContent(Convert.ToString(gcashnumber)), "g_cash_no");
                    form.Add(new StringContent(Convert.ToString(latitude)), "latitude");
                    form.Add(new StringContent(Convert.ToString(longitude)), "longitude");
                    form.Add(new StringContent(Convert.ToString(driverid)), "driver_id");
                    form.Add(new StringContent(string.Empty), "profilephoto_url");

                    var result = await TryWithErrorAsync(_authenticationService.SaveDriverGCashAccountDetails(form), true, false);
                    if (result?.ResultType == ResultType.Ok && result.Data.status == 200)
                    {
                        ShowToast("Gcash details added successfully.");
                        if (_navigationService.GetCurrentPageViewModel() != typeof(ProfilePicPageViewModel))
                        {
                            await _navigationService.NavigateToAsync<ProfilePicPageViewModel>();
                            await App.Locator.ProfilePicPage.InitilizeData();
                        }
                    }
                    else
                    {
                        ShowToast("Error in adding bank details");
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
        internal async Task InitilizeData()
        {

        }

        #region Public Prop

        public string AccountNameText
        {
            get
            {
                return _accountNameText;
            }
            set
            {
                SetProperty(ref _accountNameText, value);
            }
        }

        public string GCNumberText
        {
            get
            {
                return _gCNumberText;
            }
            set
            {
                SetProperty(ref _gCNumberText, value);
            }
        }

        public string ReapetGCNumberText
        {
            get
            {
                return _reapetGCNumberText;
            }
            set
            {
                SetProperty(ref _reapetGCNumberText, value);
            }
        }
        private bool _gCNumberTextErrorVisibility;
        public bool GCNumberTextErrorVisibility
        {
            get
            {
                return _gCNumberTextErrorVisibility;
            }
            set
            {
                SetProperty(ref _gCNumberTextErrorVisibility, value);
            }
        }
        private bool _cnfLogEmailErrorVisibility;
        public bool CnfLogEmailErrorVisibility
        {
            get
            {
                return _cnfLogEmailErrorVisibility;
            }
            set
            {
                SetProperty(ref _cnfLogEmailErrorVisibility, value);
            }
        }
        private string _gCNumberTextError;
        public string GCNumberTextError
        {
            get
            {
                return _gCNumberTextError;
            }
            set
            {
                SetProperty(ref _gCNumberTextError, value);
            }
        }
        private string _cnfGcashEror;
        public string CnfGcashEror
        {
            get
            {
                return _cnfGcashEror;
            }
            set
            {
                SetProperty(ref _cnfGcashEror, value);
            }
        }

        public bool UserEnteredAllRecord()
        {
            GCNumberTextErrorVisibility = false;
            CnfLogEmailErrorVisibility = false;
            GCNumberTextError = "";
            CnfGcashEror = "";

            GCNumberTextError = string.IsNullOrEmpty(GCNumberText) ? ErrorMessages.GcashnoEmpty : GCNumberTextError;

            if (!string.IsNullOrEmpty(GCNumberTextError))
            {
                GCNumberTextErrorVisibility = true;
                return GCNumberTextError == "";
            }
            CnfGcashEror = string.IsNullOrEmpty(ReapetGCNumberText) ? ErrorMessages.CnfGcashnoEmpty : CnfGcashEror;
            if (!string.IsNullOrEmpty(ReapetGCNumberText))
            {
                CnfGcashEror = ReapetGCNumberText != GCNumberText ? ErrorMessages.CnfgcashWrong : CnfGcashEror; /*Password.Length < 6 ? ErrorMessages.PasswordLength : ErrorMessage;*/
            }
            if (!string.IsNullOrEmpty(CnfGcashEror))
            {
                CnfLogEmailErrorVisibility = true;
                return CnfGcashEror == "";
            }
            return true;
        }

        #endregion
    }
}
