using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DuraRider.Helpers
{
    public class TransparentModel : PopupPage
    {
        public TransparentModel(View view, Rg.Plugins.Popup.Enums.MoveAnimationOptions moveAnimationOptions = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Bottom)
        {
            IsAnimationEnabled = true;
            CloseWhenBackgroundIsClicked = true;

            double bottom = 0;
            double top = 0;
            //if (UIApplication.SharedApplication.KeyWindow != null)
            //{
            //    var sa = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
            //    bottom = sa.Bottom;
            //    top = sa.Top;
            //}
            Content = view;
            Content.Margin = new Thickness(0, -top, 0, -bottom);
            Animation = new Rg.Plugins.Popup.Animations.ScaleAnimation()
            {
                PositionIn = moveAnimationOptions,
                PositionOut = moveAnimationOptions
            };
            // set the background to transparent color 
            // (actually darkened-transparency: notice the alpha value at the end)
            this.BackgroundColor = new Color(0, 0, 0, 0.4);
        }
        protected override bool OnBackgroundClicked()
        {
            Navigation.PopAsync();
            return base.OnBackgroundClicked();

        }
    }
    public class WhiteModel : PopupPage
    {
        public WhiteModel(View view, Rg.Plugins.Popup.Enums.MoveAnimationOptions moveAnimationOptions = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Bottom)
        {
            IsAnimationEnabled = true;
            CloseWhenBackgroundIsClicked = true;
            Content = view;
            Title = "Settings";

            Animation = new Rg.Plugins.Popup.Animations.ScaleAnimation()
            {
                PositionIn = moveAnimationOptions,
                PositionOut = moveAnimationOptions,
                EasingIn = Easing.CubicIn,
                EasingOut = Easing.CubicOut
            };
            // set the background to transparent color 
            // (actually darkened-transparency: notice the alpha value at the end)
            this.BackgroundColor = Color.White;
        }
    }
}
