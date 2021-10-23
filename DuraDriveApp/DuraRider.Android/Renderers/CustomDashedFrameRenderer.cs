using System;
using Android.Content;
using Android.Graphics.Drawables;
using DuraRider.Controls;
using DuraRider.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDashedFrame), typeof(CustomDashedFrameRenderer))]
namespace DuraRider.Droid.Renderers
{
    public class CustomDashedFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public CustomDashedFrameRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            CustomDashedFrame customFrame = Element as CustomDashedFrame;
            float r = customFrame.CornerRadius;
            GradientDrawable shape = new GradientDrawable();

            shape.SetCornerRadii(new float[] { r, r, r, r, r, r, r, r });
            shape.SetColor(customFrame.BackgroundColor.ToAndroid());
            shape.SetStroke(2, Android.Graphics.Color.Rgb(33, 30, 102), 20f, 5f);
            shape.SetShape(ShapeType.Rectangle);
            //shape.SetCornerRadius(r);
            //shape.SetColors(new int[] { 0,92,19 });
            //Control.SetBackgroundColor(customFrame.BackgroundColor.ToAndroid());
            Control.SetBackground(shape);

        }
    }
}
