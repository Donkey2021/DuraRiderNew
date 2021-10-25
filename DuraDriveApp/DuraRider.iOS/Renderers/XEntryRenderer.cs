using DuraRider.Controls;
using DuraRider.iOS.Renderers;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(XEntry), typeof(XEntryRenderer))]
namespace DuraRider.iOS.Renderers
{
    public class XEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            try
            {
                this.Control.BorderStyle = UIKit.UITextBorderStyle.None;
                this.Control.TintColor = UIColor.FromRGB(44, 81, 88);
            }
            catch
            {

            }
        }
    }
}
