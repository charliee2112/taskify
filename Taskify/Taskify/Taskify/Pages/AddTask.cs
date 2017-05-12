using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Taskify.Item;
using Taskify.Users;
using Taskify.Controls;

namespace Taskify.Pages
{
    class AddTask : StackLayout
    {
        private NoLineEntry nameEntry = new NoLineEntry() {TextColor = Color.Black};
        private NoLineEditor descEntry = new NoLineEditor() {TextColor = Color.Gray};
        private DatePicker expDatePicker;
        private TimePicker expTimePicker;
        private User user;
        private List<User> users;
        private Label error;
        private StackLayout stack;
        private StackLayout s1;
        private StackLayout s2;
        private StackLayout se;
        private StackLayout sep;
        private HomePage home;

        public AddTask(User aUser, List<User> someUsers, HomePage h)
        {
            home = h;
            user = aUser;
            users = someUsers;

            load();

        }

        public string addTask()
        {
            DateTime d = expDatePicker.Date;

            d.Add(expTimePicker.Time);

            if (!string.IsNullOrEmpty(nameEntry.Text))
            {
                if (nameEntry.Text.Length > 16)
                {
                    return "El nombre puede tener hasta 16 caracteres incluyendo espacios.";
                }
                else
                {
                    Item.Item i = new Item.Item(nameEntry.Text, d);
                    i.desc = descEntry.Text;
                    user.getTasks().Add(i);
                    return "";
                }
               
            }
            else
            {
                return "No se especifico un nombre para la tarea!";
            }
           
        }

        private void load()
        {

            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.White;

            stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = this.WidthRequest - 30,
                Padding = new Thickness(15, 0, 15, 0),
                Spacing = 15
            };
            nameEntry.Placeholder = "Nombre";
            nameEntry.FontSize = 21;
            //nameEntry.TranslationX = -13;
            nameEntry.TranslationY = 20;
            nameEntry.Focused += NameEntry_Focused;
            nameEntry.Unfocused += NameEntry_Unfocused;
            descEntry.Text = "Descripción";
            descEntry.FontSize = 21;
            //descEntry.TranslationX = -13;
            descEntry.TranslationY = 20;
            descEntry.Focused += DescEntry_Focused;
            descEntry.Unfocused += DescEntry_Unfocused;

            expDatePicker = new ColorDatePicker()
            {
                //TranslationX = -10,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            expTimePicker = new ColorTimePicker()
            {
                //TranslationX = -10,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };


            StackLayout otroStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                TranslationY = 10,

            };

            error = new Label()
            {
                Text = "No se puede crear una tarea sin nombre",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Red,
                IsVisible = false

            };

            nameEntry.TextChanged += NameEntry_TextChanged;

            sep = new StackLayout() {BackgroundColor = Color.Gray, HeightRequest = 1};
            se = new StackLayout() {HeightRequest = 1, BackgroundColor = Color.Gray};


            Children.Add(stack);
            stack.Children.Add(nameEntry);
            stack.Children.Add(se);

            stack.Children.Add(descEntry);
            stack.Children.Add(sep);

            StackLayout sDate = new StackLayout() {HorizontalOptions = LayoutOptions.StartAndExpand};
            StackLayout sTime = new StackLayout() {HorizontalOptions = LayoutOptions.EndAndExpand};
            StackLayout SDate = new StackLayout() {HorizontalOptions = LayoutOptions.FillAndExpand};
            StackLayout STime = new StackLayout() {HorizontalOptions = LayoutOptions.FillAndExpand};
            StackLayout SSDate = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TranslationY = 12,
                Orientation = StackOrientation.Horizontal
            };
            StackLayout SSTime = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TranslationY = 12,
                Orientation = StackOrientation.Horizontal
            };
            sDate.Children.Add(SDate);
            sTime.Children.Add(STime);
            SDate.Children.Add(SSDate);
            STime.Children.Add(SSTime);


            SSDate.Children.Add(expDatePicker);
            SSDate.Children.Add(new Label() {Text = "              "});
            SSTime.Children.Add(expTimePicker);
            SSTime.Children.Add(new Label() {Text = "   "});

            SSDate.Children.Add(new Image()
            {
                Source = "detailIcon.png",
                Scale = 1.5,
                HorizontalOptions = LayoutOptions.EndAndExpand

            });
            SSTime.Children.Add(new Image()
            {
                Source = "detailIcon.png",
                Scale = 1.5,
                HorizontalOptions = LayoutOptions.EndAndExpand

            });

            s1 = new StackLayout() {BackgroundColor = Color.Gray, HeightRequest = 1};
            s2 = new StackLayout() {HeightRequest = 1, BackgroundColor = Color.Gray};

            sDate.Children.Add(s1);
            sTime.Children.Add(s2);

            otroStack.Children.Add(sDate);
            otroStack.Children.Add(sTime);

            stack.Children.Add(otroStack);
            stack.Children.Add(error);


            TapGestureRecognizer t1 = new TapGestureRecognizer();
            t1.Tapped += T1_Tapped;
            sDate.GestureRecognizers.Add(t1);
            TapGestureRecognizer t2 = new TapGestureRecognizer();
            t2.Tapped += T2_Tapped;
            sTime.GestureRecognizers.Add(t2);

            
            //expDatePicker. += ExpDatePicker_DateSelected;
        }

        private void ExpDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            ((ColorDatePicker) (sender)).Focus();
            
        }

        private void T3_Tapped(object sender, EventArgs e)
        {
            //expDatePicker = new ColorDatePicker();
            //expDatePicker.Focus();
       

        }



        private void T1_Tapped(object sender, EventArgs e)
        {
            //DatePicker aux = (DatePicker)((StackLayout)(((StackLayout)(((StackLayout)(sender)).Children[0])).Children[0])).Children[0];
            
            



        }
        private void T2_Tapped(object sender, EventArgs e)
        {
            //expTimePicker= new ColorTimePicker();
            //expTimePicker.Focus();
        }
        private void NameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            se.BackgroundColor = Color.Gray;
            se.HeightRequest = 1;
        }

        private void NameEntry_Focused(object sender, FocusEventArgs e)
        {
            
            se.BackgroundColor = Color.FromRgb(0, 122, 255);
            se.HeightRequest = 2;
        }

        private void DescEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (((NoLineEditor)(sender)).Text.Equals(""))
            {
                ((NoLineEditor)(sender)).TextColor = Color.Gray;
                ((NoLineEditor)(sender)).Text = "Descripción";
            }
            sep.BackgroundColor = Color.Gray;
            sep.HeightRequest = 1;
        }

        private void DescEntry_Focused(object sender, FocusEventArgs e)
        {
            if (((NoLineEditor)(sender)).Text.Equals("Descripción"))
            {
                ((NoLineEditor)(sender)).TextColor = Color.Black;
                ((NoLineEditor)(sender)).Text = "";
            }
            
            sep.BackgroundColor = Color.FromRgb(0, 122, 255);
            sep.HeightRequest = 2;
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
           /*if (e.NewTextValue.Equals(""))
            {
                expTimePicker = new ColorTimePicker()
                {
                    Time = expTimePicker.Time
                };
                expDatePicker = new ColorDatePicker()
                {
                    Date = expDatePicker.Date
                };
            }
            else
            {
                expTimePicker = new TimePicker()
                {
                    Time = expTimePicker.Time
                };
                expDatePicker = new DatePicker()
                {
                    Date = expDatePicker.Date
                };

            }*/
        }
      
    }
}

