using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Taskify.Controls
{
    public class NoLineEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create<NoLineEntry, Color>(p => p.BorderColor, Color.Black);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create<NoLineEntry, double>(p => p.FontSize, Font.Default.FontSize);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create<NoLineEntry, Color>(p => p.PlaceholderColor, Color.Default);

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

    }
}