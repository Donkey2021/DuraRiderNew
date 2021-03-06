using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DuraRider.Areas.Common.PopupView.ViewModel;
using DuraRider.Core.Helpers.Enums;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace DuraRider.Areas.Common.PopupView.View
{
    public partial class CameraGalleryPopupPage : PopupPage
    {
        private TaskCompletionSource<Tuple<ProfilePicSelectionType, string>> _taskCompletionSource;
        public Task<Tuple<ProfilePicSelectionType, string>> PopupClosedTask => _taskCompletionSource.Task;

        public CameraGalleryPopupPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _taskCompletionSource = new TaskCompletionSource<Tuple<ProfilePicSelectionType, string>>();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _taskCompletionSource.SetResult(((CameraGalleryPopupPageViewModel)BindingContext).ReturnValue);
        }
    }
}
