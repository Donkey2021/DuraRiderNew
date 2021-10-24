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
    public class DuraBageServicePageViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand ProfilePicTap { get; set; }
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

        private bool _haveDuraBag = true;
        public bool HaveDuraBag
        {
            get { return _haveDuraBag; }
            set
            {
                _haveDuraBag = value;
                OnPropertyChanged(nameof(HaveDuraBag));
            }
        }

        private bool _haveNoDuraBag;
        public bool HaveNoDuraBag
        {
            get { return _haveNoDuraBag; }
            set
            {
                _haveNoDuraBag = value;
                OnPropertyChanged(nameof(HaveNoDuraBag));
            }
        }
        private string _durabagIdText;
        public string DurabagIdText
        {
            get { return _durabagIdText; }
            set { _durabagIdText = value; OnPropertyChanged(); }
        }
        object _selectionRadio;
        public object SelectionRadio
        {
            get => _selectionRadio;
            set
            {
                _selectionRadio = value;
                IsViewVisible(_selectionRadio as string);
                OnPropertyChanged(nameof(SelectionRadio));
            }
        }

        private bool _isVisibleDuraStack = true;
        public bool IsVisibleDuraBagStack
        {
            get { return _isVisibleDuraStack; }
            set
            {
                _isVisibleDuraStack = value;
                OnPropertyChanged(nameof(IsVisibleDuraBagStack));
            }
        }

        public DuraBageServicePageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            SubmitCommand = new AsyncCommand(SubmitCommandExecute, allowsMultipleExecutions: false);
            RegistrationData = App.Locator.GCashAccountDetailsPage.RegistrationData;
        }

        private void IsViewVisible(string value)
        {
            if (value != null)
            {
                if (value == "Dura")
                {
                    IsVisibleDuraBagStack = true;
                }
                else
                {
                    IsVisibleDuraBagStack = false;
                }
            }
            else
            {
                IsVisibleDuraBagStack = true;
            }
        }

        private async Task SubmitCommandExecute()
        {
            if (IsVisibleDuraBagStack && string.IsNullOrEmpty(DurabagIdText))
            {
                ShowToast("Please enter dura bag id");
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

                    var driverid = RegistrationData.driverid == 0 ? 0 : RegistrationData.driverid;
                    var latitude = currentLoc.Latitude == 0 ? 0 : currentLoc.Latitude;
                    var longitude = currentLoc.Longitude == 0 ? 0 : RegistrationData.longitude;
                    var duratext = string.IsNullOrEmpty(DurabagIdText) ? string.Empty : DurabagIdText;

                    var form = new MultipartFormDataContent();
                    form.Add(new StringContent(Convert.ToString(latitude)), "latitude");
                    form.Add(new StringContent(Convert.ToString(longitude)), "longitude");
                    form.Add(new StringContent(Convert.ToString(driverid)), "driver_id");
                    form.Add(new StringContent(duratext), "dura_bag_id");

                    var result = await TryWithErrorAsync(_authenticationService.SaveDriverDuraBag(form), true, false);
                    if (result?.ResultType == ResultType.Ok && result.Data.status == 200)
                    {
                        ShowToast("Dura bad added successfully.");
                    }
                    else
                    {
                        ShowToast("Error in adding dura bag");
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

        internal Task InitilizeData()
        {
            throw new NotImplementedException();
        }
    }
}
