using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DuraRider.Areas.Common.PopupView.View;
using DuraRider.Areas.Common.PopupView.ViewModel;
using DuraRider.Core.Helpers;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Helpers;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DuraRider.Areas.Common.ViewModels
{
    public class PersonalInfoPageViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand NextCommand { get; set; }
        #region PrivatePropeties

        private string _firstName;
        private string _firstNameError;
        private bool _firstNameErrorVisibility;
        private string _midNameError;
        private bool _midNameErrorVisibility;
        private string _lastNameError;
        private bool _lastNameErrorVisibility;
        private string _middleName;
        private string _lastName;
        private DateTime _dOBText = DateTime.Now.AddYears(-31);
        private string _dOBError;
        private bool _dOBErrorVisibility;
        private string _manageAccountNumb;

        #endregion

        #region PublicPropeties
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
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                SetProperty(ref _firstName, value);

            }
        }

        public string FirstNameError
        {
            get
            {
                return _firstNameError;
            }
            set
            {
                SetProperty(ref _firstNameError, value);
            }
        }
        public string MidNameError
        {
            get
            {
                return _midNameError;
            }
            set
            {
                SetProperty(ref _midNameError, value);
            }
        }
        public string LastNameError
        {
            get
            {
                return _lastNameError;
            }
            set
            {
                SetProperty(ref _lastNameError, value);
            }
        }

        public bool FirstNameErrorVisibility
        {
            get
            {
                return _firstNameErrorVisibility;
            }
            set
            {
                SetProperty(ref _firstNameErrorVisibility, value);
            }
        }
        public bool MidNameErrorVisibility
        {
            get
            {
                return _midNameErrorVisibility;
            }
            set
            {
                SetProperty(ref _midNameErrorVisibility, value);
            }
        }
        public bool LastNameErrorVisibility
        {
            get
            {
                return _lastNameErrorVisibility;
            }
            set
            {
                SetProperty(ref _lastNameErrorVisibility, value);
            }
        }

        public string MiddleName
        {
            get
            {
                return _middleName;
            }
            set
            {
                SetProperty(ref _middleName, value);
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        public DateTime DOBText
        {
            get
            {
                return _dOBText;
            }
            set
            {
                SetProperty(ref _dOBText, value);


            }
        }
        public string DOBError
        {
            get
            {
                return _dOBError;
            }
            set
            {
                SetProperty(ref _dOBError, value);
            }
        }
        public bool DOBErrorVisibility
        {
            get
            {
                return _dOBErrorVisibility;
            }
            set
            {
                SetProperty(ref _dOBErrorVisibility, value);
            }
        }
        public string ManageAccountNumb
        {
            get
            {
                return _manageAccountNumb;
            }
            set
            {
                SetProperty(ref _manageAccountNumb, value);
            }
        }


        private DateTime _fromMiminumDate = DateTime.Now.AddYears(-60);
        public DateTime FromMiminumDate
        {
            get { return _fromMiminumDate; }
            set
            {
                if (_fromMiminumDate == value)
                    return;

                _fromMiminumDate = value;
                OnPropertyChanged(nameof(FromMiminumDate));
            }
        }
        private DateTime _fromMaximumDate = DateTime.Now.AddYears(-17);
        public DateTime FromMaximumDate
        {
            get { return _fromMaximumDate; }
            set
            {
                if (_fromMaximumDate == value)
                    return;

                _fromMaximumDate = value;
                OnPropertyChanged(nameof(FromMaximumDate));
            }
        }

        #endregion
        public PersonalInfoPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            RegistrationData = App.Locator.OTPPage.RegistrationData;
            NextCommand = new AsyncCommand(NextCommandExecute, allowsMultipleExecutions: false);
        }

        private async Task NextCommandExecute()
        {
            if (!UserEnteredAllRecord())
            {
                return;
            }
            if (UserEnteredAllRecord())
            {
                ShowLoading();
                var currentLoc = await LocationHelpers.getCurrentPosition();
                if (currentLoc == null)
                {
                    ShowToast("Current location not found");
                    return;
                }
                RegistrationData.latitude = currentLoc.Latitude;
                RegistrationData.longitude = currentLoc.Longitude;
                RegistrationData.firstname = FirstName;
                RegistrationData.middlename = MiddleName;
                RegistrationData.lastname = LastName;
                RegistrationData.manger_id = ManageAccountNumb;
                RegistrationData.dob = DOBText.ToString();
                HideLoading();

                if (_navigationService.GetCurrentPageViewModel() != typeof(ReferralPopUpPageViewModel))
                {
                    await App.Locator.ReferralPopUpPage.InitilizeData();
                    await PopupNavigation.Instance.PushAsync(new ReferralPopUpPage());
                }
            }
        }

        public bool UserEnteredAllRecord()
        {
            try
            {
                DOBErrorVisibility = false;
                FirstNameErrorVisibility = false;
                MidNameErrorVisibility = false;
                LastNameErrorVisibility = false;



                FirstNameError = "";
                FirstNameError = string.IsNullOrEmpty(FirstName) ? ErrorMessages.FirstNameEmpty : FirstNameError;
                if (!string.IsNullOrEmpty(FirstName))
                {
                    FirstNameError = !Regex.IsMatch(FirstName, "^[a-zA-Z]") ? ErrorMessages.FirstWrong : FirstNameError;
                }
                if (!string.IsNullOrEmpty(FirstNameError))
                {
                    FirstNameErrorVisibility = true;
                    return FirstNameError == "";
                }
                DOBError = "";
                DOBError = string.IsNullOrEmpty(DOBText.ToString("dd-MM-yyyy")) ? ErrorMessages.DOBTextEmpty : DOBError;
                if (!string.IsNullOrEmpty(DOBError))
                {
                    DOBErrorVisibility = true;
                    return DOBError == "";
                }
                MidNameError = "";
                MidNameError = string.IsNullOrEmpty(MiddleName) ? ErrorMessages.FirstNameEmpty : MidNameError;
                if (!string.IsNullOrEmpty(MiddleName))
                {
                    MidNameError = !Regex.IsMatch(MiddleName, "^[a-zA-Z]") ? ErrorMessages.FirstWrong : MidNameError;
                }
                if (!string.IsNullOrEmpty(MidNameError))
                {
                    MidNameErrorVisibility = true;
                    return MidNameError == "";
                }
                LastNameError = "";
                LastNameError = string.IsNullOrEmpty(LastName) ? ErrorMessages.FirstNameEmpty : LastNameError;
                if (!string.IsNullOrEmpty(LastName))
                {
                    LastNameError = !Regex.IsMatch(LastName, "^[a-zA-Z]") ? ErrorMessages.FirstWrong : LastNameError;
                }
                if (!string.IsNullOrEmpty(LastNameError))
                {
                    LastNameErrorVisibility = true;
                    return LastNameError == "";
                }
                return true;

            }
            catch (Exception ex)
            {
                //  Task.Run(async () => await ErrorHelper.SendError(ex));
                return false;
            }

        }
    }
}
