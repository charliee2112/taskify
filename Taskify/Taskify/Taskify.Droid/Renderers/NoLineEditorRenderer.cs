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

[assembly: ExportRenderer(typeof(NoLineEditor), typeof(NoLineEditorRenderer))]
namespace Taskify.Droid.Renderers
{
    class NoLineEditorRenderer : EditorRenderer
    {
        private Drawable originalDrawable;
        DisplayMetrics disp = new DisplayMetrics();
        private Android.Graphics.Color originalColor = Android.Graphics.Color.White;

        

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
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
            var ln = (NoLineEditor)Element;
            SetLineColor(ln);

        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != InputView.KeyboardProperty.PropertyName)
                base.OnElementPropertyChanged(sender, e);
            var view = (NoLineEditor)Element;

            if (e.PropertyName == NoLineEditor.BorderColorProperty.PropertyName)
                SetLineColor(view);
        }



        private void SetLineColor(NoLineEditor entry)
        {
            var andColor = Color.Transparent.ToAndroid();

            Control.SetHighlightColor(andColor);

            var shape = new ShapeDrawable(new RectShape());
            shape.Paint.Color = andColor;
            shape.Paint.StrokeWidth = 0;
            shape.Paint.SetStyle(Paint.Style.Stroke);
            Control.SetBackground(shape);

            Control.SetTextSize(ComplexUnitType.Px, 40);

        }
    }
}