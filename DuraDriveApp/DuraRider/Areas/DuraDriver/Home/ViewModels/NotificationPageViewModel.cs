using DuraRider.Core.Helpers;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Home.ViewModels
{
    public class NotificationPageViewModel : AppBaseViewModel
    {

        #region localVariable
        private INavigationService _navigationService;
        private IUserCoreService _userCoreService;

        public IAsyncCommand DoneCommand { get; set; }
        #endregion

        public NotificationPageViewModel(INavigationService navigationService, IUserCoreService userCoreService)
        {
            _navigationService = navigationService;
            _userCoreService = userCoreService;
            DoneCommand = new AsyncCommand(DoneCommandExecute);
        }
        #region Method 
        private async Task DoneCommandExecute()
        {
            if (!CheckConnection())
            {
                ShowToast(CommonMessages.NoInternet);
                return;
            }
            try
            {
                //if (_navigationService.GetCurrentPageViewModel() != typeof(PaymentSuccessPage))
                //{
                //    await _navigationService.NavigateToAsync<PaymentSuccessPage>();
                //    await App.Locator.SignUpPage.InitilizeData();
                //}
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        public async Task InitilizeData()
        {

        }
        public class NotificationModel
        {
            public string Images { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Date { get; set; }
        }
        public ObservableCollection<NotificationModel> NotificationData { get; set; } = new ObservableCollection<NotificationModel>()
        {
            new NotificationModel() { Title="Notifications Title", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,",Date="05 Feb 2021"},
            new NotificationModel() { Title="Notifications Title", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,",Date="05 Feb 2021"},
            new NotificationModel() { Title="Notifications Title", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,",Date="05 Feb 2021"},
            new NotificationModel() { Title="Notifications Title", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,",Date="05 Feb 2021"},
            new NotificationModel() { Title="Notifications Title", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,",Date="05 Feb 2021"},
            new NotificationModel() { Title="Notifications Title", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,",Date="05 Feb 2021"},
            new NotificationModel() { Title="Notifications Title", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,",Date="05 Feb 2021"},
        }; 
    }
}