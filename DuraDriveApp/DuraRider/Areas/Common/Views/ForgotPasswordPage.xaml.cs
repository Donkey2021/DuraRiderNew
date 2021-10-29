using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DuraRider.Areas.Common.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            pickercountries.Focus();
        }
    }
}
