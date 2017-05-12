using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace Taskify.Controls
{
    class ColorTimePicker : TimePicker
    {
        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create<ColorTimePicker, Color>(p => p.PlaceholderColor, Color.Default);

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }
    }

}
