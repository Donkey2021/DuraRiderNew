using Acr.UserDialogs;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using MvvmHelpers;
using DuraRider.Services.Interfaces;
using DuraRider.Services;
using DuraRider.Core.Models.Result;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Helpers;
using DuraRider.Areas.Common.PopupView.View;

namespace DuraRider.ViewModels
{
    public class AppBaseViewModel : BaseViewModel
    {
        protected NavigationPage _navigation => (Application.Current.MainPage as NavigationPage) ?? Application.Current.MainPage as NavigationPage;
        protected IUserDialogs _userDialogs => UserDialogs.Instance;
        protected readonly INavigationService NavigationService;
        private object _parameter;

        public bool IsInitializedBase { get; protected set; }

        public bool IsInitialized { get; protected set; } = false;

        public ICommand ShellNavigateBackCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand ClosePopupCommand { get; private set; }
        public object Parameter
        {
            get => _parameter;
            set => SetProperty(ref _parameter, value);
        }
        string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }
        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        bool _canNavigate = true;
        public bool CanNavigate
        {
            get { return _canNavigate; }
            set
            {
                _canNavigate = value;
                OnPropertyChanged();
            }
        }
        public AppBaseViewModel()
        {
            NavigationService = App.Container.Resolve<INavigationService>();
            ShellNavigateBackCommand = new Command(async () => await NavigationService.ShellNavigationPopAsync());
            BackCommand = new Command(async () => await NavigationService.NavigateBackAsync());
            ClosePopupCommand = new Command(async () => await NavigationService.ClosePopupsAsync());
            Initialize();
        }

        /// <summary>
        /// Tries to call a service method and shows the user an error if the request isn't successful
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="action"></param>
        /// <param name="setLoading"></param>
        /// <param name="failWithToast"></param>
        /// <returns></returns>
        protected async Task<Result<TType>> TryWithErrorAsync<TType>(
            Task<Result<TType>> action,
            bool setLoading = false,
            bool failWithToast = false,
            bool failWithAlert = false
            )
        {
            Result<TType> result = null;
            try
            {
                if (setLoading)
                    ShowLoading();
                result = await action;
                if (setLoading)
                    HideLoading();
                if (result?.ResultType != ResultType.Ok)
                {
                    // special case for unauthorized. Boot them back to the start.
                    if (result.ResultType == ResultType.Unauthorized)
                    {
                        //Clear all user data and navigate to Login
                        ShowAlert("Looks Like Your Session Has Expired! Please Sign Back In.", "Session Expired", "Ok");

                    }
                    else if (result.ResultType == ResultType.PartialOk)
                    {
                        Debug.WriteLine("Partial OK request detected");
                        if (failWithToast)
                            ShowToast("Not all data was able to sync.");
                    }
                    else
                    {
                        // otherwise, continue with the error handling
                        var errorMessage = result?.Errors.FirstOrDefault();
                        if (failWithAlert)
                            ShowAlert(errorMessage, "Oops!");
                        else if (failWithToast)
                            ShowToast(errorMessage);
                        Debug.WriteLine(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message, "Oops!");
                if (setLoading)
                    HideLoading();
                Debug.WriteLine(ex);
                Console.WriteLine(ex);
                //Crashes.TrackError(ex);
            }
            return result;
        }

        private void Initialize()
        {
            if (IsInitializedBase)
                return;
            IsInitializedBase = true;
        }
        protected async Task<Tuple<ProfilePicSelectionType, string>> ShowCameraPopup()
        {
            //await App.Locator.CameraGalleryPopupPage.InitilizeData();
            var popup = new CameraGalleryPopupPage();
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popup);
            var ret = await popup.PopupClosedTask;

            return ret;
        }
        public virtual async Task OnPageAppearing() { }

        public virtual async Task OnPageDisappearing() { }

        protected void ShowLoading() { _userDialogs.ShowLoading(); }
        protected void HideLoading() { _userDialogs.HideLoading(); }
        protected void ShowAlert(string message, string title = "", string acknowledgeText = "OK")
        {
            _userDialogs.Alert(message, title, acknowledgeText);
        }
        protected async Task<bool> ShowConfirmationAlert(string message, string title = "", string acknowledgeOkText = "OK", string acknowledgeCancelText = "CANCEL")
        {
            var result = await _userDialogs.ConfirmAsync(message, title, acknowledgeOkText, acknowledgeCancelText);
            return result;
        }
        protected void ShowToast(string message)
        {
            _userDialogs.Toast(message);
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        public virtual void ClearData()
        {
            IsInitializedBase = false;
        }
        protected bool CheckConnection(bool AlertIfNot = false)
        {
            if (ConnectivityService.IsConnected())
            {
                return true;
            }
            else
            {
                if (AlertIfNot)
                    ThreadingHelpers.InvokeOnMainThread(() => { _userDialogs.Alert("No Internet Connection", "Offline!"); });
                return false;
            }
        }
    }
}
