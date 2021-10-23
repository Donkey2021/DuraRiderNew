using DuraRider.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DuraRider.Services.Interfaces
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : AppBaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : AppBaseViewModel;

        Task NavigateToAsync<TViewModel>(TViewModel viewModel) where TViewModel : AppBaseViewModel;

        Task NavigateToAsync(Type viewModelType);

        Task NavigateToAsync(Type viewModelType, object parameter);

        Task ShellGoToAsync(string route);

        Task ShellNavigationPushAsync(Page page);

        Task ShellGoBackAsync();

        Task ShellNavigationPopAsync();

        Task ShellNavigationPopToRootAsync();

        Task NavigateBackAsync();

        Task RemoveLastFromBackStackAsync();
        Task ClearNavigationStackAsync();

        Task NavigateToPopupAsync<TViewModel>(bool animate, TViewModel viewModel) where TViewModel : AppBaseViewModel;

        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate, TViewModel viewModel) where TViewModel : AppBaseViewModel;

        Task CloseAllPopupsAsync();

        Task ClosePopupsAsync();

        Type GetCurrentPageViewModel();

        bool SetCurrentPageTitle(string title);

        IList<Tuple<Type, Type, bool>> _mappingsRead { get; }
    }
}
