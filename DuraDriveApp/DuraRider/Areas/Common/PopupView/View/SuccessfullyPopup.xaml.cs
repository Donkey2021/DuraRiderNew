using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuraRider.Areas.Common.PopupView.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessfullyPopup 
    {
        private string title; 
        public SuccessfullyPopup(string title)
        {
            InitializeComponent(); 
            lblTitle.Text = title;
            Dissmisspop();
        }

        private async void Dissmisspop()
        {
            await Task.Delay(800);
            Dismiss(null);
        }
    }
}