using System;
using System.Threading.Tasks;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DuraRider.Areas.Common.ViewModels
{
    public class ApprovalPageViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        public IAsyncCommand ProfilePicTap { get; set; }
        public IAsyncCommand ApprovalCommand { get; set; }
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
        public ApprovalPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            ApprovalCommand = new AsyncCommand(ApprovalCommandExecute, allowsMultipleExecutions: false);
            RegistrationData = App.Locator.GCashAccountDetailsPage.RegistrationData;
        }

        private async Task ApprovalCommandExecute()
        {

        }
    }
}
