using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Taskify.Users;
using Taskify.Controls;
namespace Taskify.Pages
{
    class Login : ContentPage
    {
        private Button logIn;
        private Entry nameEntry;
        private Entry pinEntry;
        private List<User> users;
        private User logging;
        private Label error;
        private StackLayout s1;
        private StackLayout s2;

        public Login(List<User> someUsers)
        {
            this.Padding= new Thickness(20,0,20,20);
            this.BackgroundColor = Color.White;
            users = someUsers;
            NavigationPage.SetHasNavigationBar(this,false);
            error = new Label()
            {
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            
            nameEntry = new NoLineEntry()
            {
                Placeholder = "Usuario",
                PlaceholderColor= Color.Gray,
                TranslationY = 20,
                TextColor = Color.Black,
                FontSize = 20
            };
            pinEntry = new NoLineEntry()
            {
                Placeholder = "Contraseña",
                PlaceholderColor = Color.Gray,
                TranslationY = 20,
                TextColor = Color.Black,
                IsPassword =true,
                FontSize = 20
            };
            var stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = 15
            };
            Label h = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                Text = "Por favor, ingresa para continuar:",
                TextColor = Color.Black,
                FontSize = 20
            };
            Image logo = new Image()
            {
                Source = "logo.png",
                TranslationY = 30,
                HorizontalOptions = LayoutOptions.Center,
                Scale = 0.42,
                
            };
            
            stack.Children.Add(logo);
            stack.Children.Add(h);
            logIn = new Button()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,

                VerticalOptions = LayoutOptions.End,
                Text = "Continuar",
                BackgroundColor = Color.FromRgb(0, 122, 255),
                TextColor = Color.White,
                HeightRequest = 65,
                FontSize = 22,
                BorderRadius = 0,
                IsEnabled = false
            };
            logIn.Clicked += LogIn_Clicked;
            nameEntry.TextChanged += NameEntry_TextChanged;
            pinEntry.TextChanged += PinEntry_TextChanged;
            s2 = new StackLayout() { BackgroundColor = Color.Gray, HeightRequest = 1 };
            s1 = new StackLayout() { BackgroundColor = Color.Gray, HeightRequest = 1 };
            nameEntry.Focused += NameEntry_Focused;
            nameEntry.Unfocused += NameEntry_Unfocused;
            pinEntry.Focused += DescEntry_Focused;
            pinEntry.Unfocused += DescEntry_Unfocused;

            stack.Children.Add(nameEntry);
            stack.Children.Add(s1);
            stack.Children.Add(pinEntry);
            stack.Children.Add(s2);
            stack.Children.Add(error);
            stack.Children.Add(logIn);

            this.Content = new ScrollView() {Content = stack };
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            pinEntry.Text = "";
        }

        private void NameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            s1.BackgroundColor = Color.Gray;
            s1.HeightRequest = 1;
        }

        private void NameEntry_Focused(object sender, FocusEventArgs e)
        {
            s1.BackgroundColor = Color.FromRgb(0, 122, 255);
            s1.HeightRequest = 2;
            pinEntry.Text = "";
        }

        private void DescEntry_Unfocused(object sender, FocusEventArgs e)
        {
            s2.BackgroundColor = Color.Gray;
            s2.HeightRequest = 1;
        }

        private void DescEntry_Focused(object sender, FocusEventArgs e)
        {
            s2.BackgroundColor = Color.FromRgb(0,122,255);
            s2.HeightRequest = 2;
        }

        private void PinEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 4)
            {
                logIn.IsEnabled = true;
            }
            else
            {
                logIn.IsEnabled = false;
            }
        }

        private void LogIn_Clicked(object sender, EventArgs e)
        {
            error.Text = "";
            logging = null;
            foreach(User u in users)
            {
                if (u.name.Equals(nameEntry.Text)){
                    logging = u;
                }
            }


            if(logging == null){
                error.Text = "Este nombre de usuario no esta registrado";
            }
            else
            {
                //chequeo el pin
                if (logging.getPin().Equals(pinEntry.Text))
                {
                    Navigation.PushAsync(new HomePage(logging, users));
                }
                else
                {
                    error.Text = "El pin ingresado no corresponde al usuario de nombre " + nameEntry.Text;
                }
            }
        }
    }
}
