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
    class EditTask : StackLayout
    {
        private NoLineEntry nameEntry = new NoLineEntry() { TextColor = Color.Black };
        private NoLineEditor descEntry = new NoLineEditor() { TextColor = Color.Black };
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
        private Label labelState;
        private Item.Item task;
        private HomePage home;

        public EditTask(User aUser, List<User> someUsers, Item.Item  Task, HomePage h)
        {
            home = h;
            task = Task;
            user = aUser;
            users = someUsers;
            load();
            descEntry.Text = task.desc;
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
            nameEntry.Placeholder = task.title;
            nameEntry.FontSize = 21;
            nameEntry.TranslationY = 20;
            nameEntry.Focused += NameEntry_Focused;
            nameEntry.Unfocused += NameEntry_Unfocused;
            descEntry.Text = task.desc;
            descEntry.FontSize = 21;
            descEntry.TranslationY = 20;
            descEntry.Focused += DescEntry_Focused;
            descEntry.Unfocused += DescEntry_Unfocused;
            expDatePicker = new ColorDatePicker() { TranslationX = -5, HorizontalOptions = LayoutOptions.StartAndExpand, Date = task.GetExpDateTime().Date};
            expTimePicker = new ColorTimePicker() { TranslationX = -5, HorizontalOptions = LayoutOptions.StartAndExpand, Time = task.GetExpDateTime().TimeOfDay };
            //expTimePicker.Time.Hours = task.GetExpDateTime().Hour;
            
            labelState = new Label() {HorizontalOptions = LayoutOptions.StartAndExpand, TranslationY = -5, Text = task.state };

            StackLayout otroStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                TranslationY = 10
            };

            error = new Label()
            {
                Text = "No se puede crear una tarea sin nombre",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Red,
                IsVisible = false

            };

            //nameEntry.TextChanged += NameEntry_TextChanged;

            sep = new StackLayout() { BackgroundColor = Color.Gray, HeightRequest = 1 };
            se = new StackLayout() { HeightRequest = 1, BackgroundColor = Color.Gray };
            
            Children.Add(stack);
            stack.Children.Add(nameEntry);
            stack.Children.Add(se);

            stack.Children.Add(descEntry);
            stack.Children.Add(sep);

            StackLayout sDate = new StackLayout() { HorizontalOptions = LayoutOptions.StartAndExpand };
            StackLayout sTime = new StackLayout() { HorizontalOptions = LayoutOptions.EndAndExpand };
            StackLayout SDate = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand };
            StackLayout STime = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand };
            StackLayout SSDate = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, TranslationY = 12, Orientation = StackOrientation.Horizontal };
            StackLayout SSTime = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, TranslationY = 12, Orientation = StackOrientation.Horizontal };

           StackLayout SSState = new StackLayout() {TranslationY = 40, Orientation = StackOrientation.Horizontal };

            sDate.Children.Add(SDate);
            SDate.Children.Add(SSDate);
            sTime.Children.Add(STime);
            STime.Children.Add(SSTime);
           
            TapGestureRecognizer t1 = new TapGestureRecognizer();
            t1.Tapped += T1_Tapped; ;
            SDate.GestureRecognizers.Add(t1);
            TapGestureRecognizer t2 = new TapGestureRecognizer();
            t2.Tapped += T2_Tapped; ;
            STime.GestureRecognizers.Add(t2);
            TapGestureRecognizer t3 = new TapGestureRecognizer();
            t3.Tapped += T3_Tapped; 
            SSState.GestureRecognizers.Add(t3);
            
            SSDate.Children.Add(expDatePicker);
            SSDate.Children.Add(new Label() { Text = "              " });
            SSTime.Children.Add(expTimePicker);
            SSTime.Children.Add(new Label() { Text = "   " });
            SSState.Children.Add(labelState);

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
            SSState.Children.Add(new Image()
            {
                Source = "detailIcon.png",
                Scale = 1.5,
                TranslationY = -5,
                HorizontalOptions = LayoutOptions.EndAndExpand

            });


            s1 = new StackLayout() { BackgroundColor = Color.Gray, HeightRequest = 1 };
            s2 = new StackLayout() { HeightRequest = 1, BackgroundColor = Color.Gray };
            StackLayout s3 = new StackLayout() { TranslationY = 25,HeightRequest = 1, BackgroundColor = Color.Gray };

            sDate.Children.Add(s1);
            sTime.Children.Add(s2);

            otroStack.Children.Add(sDate);
            otroStack.Children.Add(sTime);

            stack.Children.Add(otroStack);
            stack.Children.Add(SSState);
            stack.Children.Add(s3);
            stack.Children.Add(error);


        }

        private void T3_Tapped(object sender, EventArgs e)
        {


            home.det = new ContentPage() { BackgroundColor = Color.White };
            home.aux.Children.RemoveAt(1);
            home.actualPage = new stateSelectPage(user, users, task, home);
            home.aux.Children.Add(home.actualPage);

            home.title.Text = "Seleccione un estado";
            home.actionIcon.Source = "";

            home.det.Content = home.aux;
            home.Detail = home.det;
            
        }

        private void T1_Tapped(object sender, EventArgs e)
        {
            expDatePicker = new ColorDatePicker();
            expDatePicker.Focus();
        }

        private void T2_Tapped(object sender, EventArgs e)
        {
            expTimePicker = new ColorTimePicker();
            expTimePicker.Focus();
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
            sep.BackgroundColor = Color.Gray;
            sep.HeightRequest = 1;
        }

        private void DescEntry_Focused(object sender, FocusEventArgs e)
        {
            sep.BackgroundColor = Color.FromRgb(0, 122, 255);
            sep.HeightRequest = 2;
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Equals(""))
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

            }
        }

        public void editTask()
        {
            if (!string.IsNullOrEmpty(nameEntry.Text))
                task.title = nameEntry.Text;

            DateTime d = expDatePicker.Date;
            d.Add(expTimePicker.Time);

            task.setExpDate(d);

            task.state = labelState.Text;
            
            if (!string.IsNullOrEmpty(descEntry.Text))
                task.desc = descEntry.Text;

        }
    }
}
