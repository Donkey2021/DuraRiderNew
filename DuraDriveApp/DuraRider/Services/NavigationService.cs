using DuraDriveRider.Pages.ViewModels;
using DuraRider.Areas.Common.PopupView.View;
using DuraRider.Areas.Common.PopupView.ViewModel;
using DuraRider.Areas.Common.ViewModels;
using DuraRider.Areas.Common.Views;
using DuraRider.Areas.DuraDriver.Home.Popup.ViewModels;
using DuraRider.Areas.DuraDriver.Home.Popup.Views;
using DuraRider.Areas.DuraDriver.Home.ViewModels;
using DuraRider.Areas.DuraDriver.Home.Views;
using DuraRider.Interfaces;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DuraRider.Services
{
    public class NavigationService : INavigationService
    {
        protected readonly IList<Tuple<Type, Type, bool>> _mappings;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public IList<Tuple<Type, Type, bool>> _mappingsRead { get => _mappings; }

        //Re-Exposed Navigation events for hooking into page changes
        public EventHandler ModalPopped;
        public EventHandler ModalPushed;
        private readonly IUserService _userService;

        public NavigationService(IUserService userService)
        {
            _userService = userService;
            CurrentApplication.ModalPopped += (sender, e) => ModalPopped?.Invoke(sender, e);
            CurrentApplication.ModalPushed += (sender, e) => ModalPushed?.Invoke(sender, e);

            _mappings = new List<Tuple<Type, Type, bool>>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            try
            {

                await NavigateToAsync<LoginPageViewModels>();
                await App.Locator.LoginPage.InitilizeData();


                //await App.Locator.TimerPage.InitilizeData();
                //await NavigateToAsync<TimerPageViewModel>();
                //await App.Locator.TimerPage.UpdateTextColor(0);
                //await App.Locator.TimerPage.UpdateHoursTextColor(1);
            }
            catch (Exception ex)
            {

            }
        }

        public Task NavigateToAsync<TViewModel>(TViewModel viewModel) where TViewModel : AppBaseViewModel
        {

            return InternalNavigateToAsync(typeof(TViewModel), viewModel, null);

        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : AppBaseViewModel
        {

            return InternalNavigateToAsync(typeof(TViewModel), null, null);

        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : AppBaseViewModel
        {

            return InternalNavigateToAsync(typeof(TViewModel), null, parameter);

        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, null, parameter);
        }

        public async Task ShellGoToAsync(string route)
        {
            await Shell.Current.GoToAsync(route, animate: true);
        }

        public async Task ShellNavigationPushAsync(Page page)
        {
            await Shell.Current.Navigation.PushAsync(page);
        }

        public async Task ShellGoBackAsync()
        {
            Shell.Current.SendBackButtonPressed();
        }

        public async Task ShellNavigationPopAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public async Task ShellNavigationPopToRootAsync()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public Type GetCurrentPageViewModel()
        {
            if (CurrentApplication.MainPage != null)
            {
                Page currentPage = CurrentApplication.MainPage.Navigation.NavigationStack.Last();
                if (currentPage?.BindingContext != null)
                    return currentPage.BindingContext.GetType();
            }
            return null;
        }

        public bool SetCurrentPageTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Page currentPage = CurrentApplication.MainPage.Navigation.NavigationStack.Last();
                if (currentPage != null)
                {
                    currentPage.Title = title;
                    return true;
                }
            }
            return false;
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as Page;

            mainPage.Navigation.RemovePage(
                mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);

            return Task.FromResult(true);
        }
        public virtual Task ClearNavigationStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as Page;
            for (int i = 0; i < mainPage.Navigation.NavigationStack.ToList().Count - 1; i++)
            {
                mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[i]);
            }
            return Task.FromResult(true);
        }

        public Task NavigateToPopupAsync<TViewModel>(bool animate, TViewModel viewModel) where TViewModel : AppBaseViewModel
        {
            return NavigateToPopupAsync(null, animate, viewModel);
        }

        public async Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate, TViewModel vieModel) where TViewModel : AppBaseViewModel
        {
            var page = CreateAndBindPage(typeof(TViewModel), vieModel, parameter, true);

            await (page.BindingContext as AppBaseViewModel).InitializeAsync(parameter);

            if (page is PopupPage)
            {
                await PopupNavigation.Instance.PushAsync(page as PopupPage, animate);
            }
            else
            {
                throw new ArgumentException($"The type ${typeof(TViewModel)} its not a PopupPage type");
            }
        }

        public async Task CloseAllPopupsAsync()
        {
            await PopupNavigation.Instance.PopAllAsync(true);
        }

        public async Task ClosePopupsAsync()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object viewModel, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, viewModel, parameter, false);

            if (page is IRootView)
            {
                //TODO prob need to check the navigation stack at this point to ensure there are no pages on top??
                if (page is IMainView)
                {
                    if (page is IShelll)
                    {
                        //object bindingContext = page.BindingContext;
                        // page = new NavigationPage(page);
                        //page.BindingContext = bindingContext;
                    }
                    else
                    {
                        object bindingContext = page.BindingContext;
                        page = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#211E66"), BarTextColor = Color.White };
                        page.BindingContext = bindingContext;
                    }
                }
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is IMainView || CurrentApplication.MainPage is NavigationPage) // Implemented as an interface so we can have different main views on different platforms
            {
                var mainPage = CurrentApplication.MainPage as Page;

                if (mainPage.GetType() != page.GetType())
                {
                    var transitionNavigationPage = CurrentApplication.MainPage as NavigationPage;
                    if (transitionNavigationPage != null)
                    {
                        await mainPage.Navigation.PushAsync(page);
                    }
                    else
                    {
                        await mainPage.Navigation.PushAsync(page);
                    }
                }
            }
            //await (page.BindingContext as BasePageViewModel).InitializeAsync(parameter);
        }


        protected Type GetPageTypeForViewModel(Type viewModelType, bool isPopup)
        {
            Type pageType = null;
            if (!isPopup)
                pageType = _mappings.FirstOrDefault(_ => (_.Item1 == viewModelType) && !_.Item3).Item2;
            else
                pageType = _mappings.FirstOrDefault(_ => (_.Item1 == viewModelType) && _.Item3).Item2;

            if (pageType == null)
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");

            return pageType;
        }

        protected Page CreateAndBindPage(Type viewModelType, object viewModelObj, object parameter, bool isPopup)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType, isPopup);
            Page page = null;
            if (pageType == null)
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            try
            {
                page = Activator.CreateInstance(pageType) as Page;
                AppBaseViewModel viewModel;

                if (viewModelObj != null)
                    viewModel = viewModelObj as AppBaseViewModel;
                else
                    viewModel = App.Container.Resolve(viewModelType) as AppBaseViewModel;
                if (page is PopupPage)
                    page.BindingContext = viewModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Code result's in {e.Message}");
            }
            return page;
        }

        private void CreatePageViewModelMappings()
        {
            //Pages
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(LoginPageViewModels), typeof(LoginPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(SignUpPageViewModel), typeof(SignUpPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(OTPPageViewModel), typeof(OTPPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(PersonalInfoPageViewModel), typeof(PersonalInfoPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(DocumentPageViewModel), typeof(DocumentPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(GCashAccountDetailsPageViewModel), typeof(GCashAccountDetailsPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(ProfilePageViewModel), typeof(ProfilePage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(DuraBageServicePageViewModel), typeof(DuraBageServicePage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(HomePageViewModel), typeof(HomePage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(NotificationPageViewModel), typeof(NotificationPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(VerifyItemPageViewModel), typeof(VerifyItemPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(ReachedPickupLocationPageViewModel), typeof(ReachedPickupLocationPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(ReachedLocationPageViewModel), typeof(ReachedLocationPage), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(ReviewPageViewModel), typeof(ReviewPage), false));
            //PopupViews
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(ReferralPopUpPageViewModel), typeof(ReferralPopUpPage), true));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(CameraGalleryPopupPageViewModel), typeof(CameraGalleryPopupPage), true));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(ReachLocationPopUpViewModel), typeof(ReachLocationPopUp), false));
            _mappings.Add(new Tuple<Type, Type, bool>(typeof(PaymentCollectedPopupViewModel), typeof(PaymentCollectedPopup), false));

        }


    }
}
