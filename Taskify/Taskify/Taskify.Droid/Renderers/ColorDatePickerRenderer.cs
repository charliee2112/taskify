using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using Taskify.Controls;
using Taskify.Droid.Renderers;
[assembly: ExportRenderer(typeof(ColorDatePicker), typeof(ColorDatePickerRenderer))]
namespace Taskify.Droid.Renderers
{

    class ColorDatePickerRenderer : DatePickerRenderer
    {

        DisplayMetrics disp = new DisplayMetrics();
        private Android.Graphics.Color originalColor = Android.Graphics.Color.White;
        private Drawable originalDrawable;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                IWindowManager windowManager = Control.Context.GetSystemService(Android.Content.Context.WindowService).JavaCast<IWindowManager>();
                windowManager.DefaultDisplay.GetMetrics(disp);
                originalDrawable = Control.Background;
                if (originalDrawable is ColorDrawable)
                    originalColor = ((ColorDrawable)originalDrawable).Color;
                if (e.NewElement.BackgroundColor != Color.Default)
                    originalColor = e.NewElement.BackgroundColor.ToAndroid();
            }

            var view = (ColorDatePicker)Element;
            if (view != null)
            {
                SetBorder(view);
                SetTextColor(view);
            }

        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            ColorDatePicker datePicker = (ColorDatePicker)Element;

            if (e.PropertyName == ColorDatePicker.TextColorProperty.PropertyName)
            {
                this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
            }
        }

        private void SetBorder(ColorDatePicker view)
        {

            if (view.HasCorner)
            {
                int lefttop = (int)(view.CornerWidth * disp.Density);
                int righttop = lefttop;
                int leftbottom = lefttop;
                int rightbottom = lefttop;

                RoundRectShape rect = new RoundRectShape(
                        new float[]
                            {
                                lefttop, lefttop,
                                righttop, righttop,
                                rightbottom, rightbottom,
                                leftbottom, leftbottom
                            }, null, null);
                ShapeDrawable bg = new ShapeDrawable(rect);
                bg.Paint.Color = view.BackgroundColor.ToAndroid();
                Control.Background = bg;
            }
            else
            {
                Control.Background = originalDrawable;
            }
        }

        private void SetTextColor(ColorDatePicker datePicker)
        {
            this.Control.SetTextColor(Color.Gray.ToAndroid());
            //this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
        }

    }
}