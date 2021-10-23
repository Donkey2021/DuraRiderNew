using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DuraRider.Areas.Common.Views
{
    public partial class OTPPage : ContentPage
    {
        public OTPPage()
        {
            InitializeComponent();
        }
        void EntryPasscode1_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryPasscode1.Text.ToString()))
            {
                EntryPasscode1.Focus();
                //   DependencyService.Get<IKeyboardHelper>().HideKeyboard();
                //EntryPasscode1.TextChanged += (s, f) => EntryPasscode1.Focus();

            }
            else
            {
                EntryPasscode2.Focus();
                //EntryPasscode1.TextChanged += (s, f) => EntryPasscode2.Focus();

            }
        }

        void EntryPasscode2_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryPasscode2.Text.ToString()))
            {
                EntryPasscode1.Focus();
            }
            else
            {
                EntryPasscode3.Focus();
            }

        }

        void EntryPasscode3_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryPasscode3.Text.ToString()))
            {
                EntryPasscode2.Focus();
            }
            else
            {
                EntryPasscode4.Focus();
            }
        }

        void EntryPasscode4_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryPasscode4.Text.ToString()))
            {
                EntryPasscode3.Focus();
            }
            else
            {
                EntryPasscode4.Focus();
            }
        }


    }
}
