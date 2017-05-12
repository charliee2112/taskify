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
[assembly: ExportRenderer(typeof(ColorTimePicker), typeof(ColorTimePickerRenderer))]
namespace Taskify.Droid.Renderers
{

    class ColorTimePickerRenderer : TimePickerRenderer
    {
        private Drawable originalDrawable;
        DisplayMetrics disp = new DisplayMetrics();
        private Android.Graphics.Color originalColor = Android.Graphics.Color.White;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                IWindowManager windowManager = Control.Context.GetSystemService(Android.Content.Context.WindowService).JavaCast<IWindowManager>();
                windowManager.DefaultDisplay.GetMetrics(disp);
                originalDrawable = Control.Background;
                if (originalDrawable is ColorDrawable)
                    originalColor = ((ColorDrawable)originalDrawable).Color;
            }
            var ln = (ColorTimePicker)Element;
            SetTextColor(ln);
            SetBorder(ln);

        }
        private void SetBorder(ColorTimePicker view)
        {


            int lefttop = 1;
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
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != InputView.KeyboardProperty.PropertyName)
                base.OnElementPropertyChanged(sender, e);
            var view = (ColorTimePicker)Element;

            if (e.PropertyName == ColorTimePicker.PlaceholderColorProperty.PropertyName)
                SetTextColor(view);
        }



        private void SetTextColor(ColorTimePicker entry)
        {
            var andColor = Color.Gray.ToAndroid();
            Control.SetTextColor(andColor);
        }
    }
}