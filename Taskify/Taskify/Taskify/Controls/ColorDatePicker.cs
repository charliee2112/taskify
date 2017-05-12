using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace Taskify.Controls
{
    class ColorDatePicker : DatePicker
    {
        public static readonly BindableProperty HasCornerProperty = BindableProperty.Create<ColorDatePicker, bool>(p => p.HasCorner, true);

        public bool HasCorner
        {
            get { return (bool)GetValue(HasCornerProperty); }
            set { SetValue(HasCornerProperty, value); }
        }

        public static readonly BindableProperty CornerWidthProperty = BindableProperty.Create<ColorDatePicker, int>(p => p.CornerWidth, 4);

        public int CornerWidth
        {
            get { return (int)GetValue(CornerWidthProperty); }
            set { SetValue(CornerWidthProperty, value); }

        }
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create<ColorDatePicker, Color>(p => p.BackgroundColor, Color.White);

        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set
            {
                base.SetValue(Entry.BackgroundColorProperty, Color.Transparent);
                SetValue(BackgroundColorProperty, value);
            }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor", typeof(Color), typeof(ColorDatePicker), Color.Black);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
    }
}
