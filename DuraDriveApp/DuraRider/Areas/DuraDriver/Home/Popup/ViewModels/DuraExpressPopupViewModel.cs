using DuraRider.Areas.DuraDriver.Home.Popup.Views;
using DuraRider.Core.Helpers;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Helpers;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DuraRider.Areas.DuraDriver.Home.Popup.ViewModels
{
    public class DuraExpressPopupViewModel : AppBaseViewModel
    {

        #region localVariable
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;

        public IAsyncCommand AcceptCommand { get; set; }
        #endregion

        public DuraExpressPopupViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
            AcceptCommand = new AsyncCommand(AcceptCommandCommandExecute);
        }
        #region Method 
        private async Task AcceptCommandCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                if (_navigationService.GetCurrentPageViewModel() != typeof(ReachLocationPopUp))
                {
                    await Navigation.PopPopupAsync();
                    await Navigation.PushPopupAsync(new TransparentModel(new ReachLocationPopUp()));
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        public async Task InitilizeData()
        {

        }
    }
}