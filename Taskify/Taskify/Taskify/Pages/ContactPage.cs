using System;
using System.Collections.Generic;
using System.Text;
using Taskify.Users;
using Xamarin.Forms;

namespace Taskify.Pages
{
    class ContactPage : StackLayout
    {
        private User user;
        private List<User> users;
        private StackLayout listTasks;
        private StackLayout s;
        private string tappedUser;
        private HomePage home;

        public ContactPage(User aUser, List<User> someUsers, HomePage h)
        {
            home = h;
            user = aUser;
            users = new List<User>(someUsers);
            users.Remove(user);
            s = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
            };
            

            listTasks = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                TranslationX = 15,
                TranslationY = 15,
                Spacing = 30
            };
            loadTasks();
            s.Children.Add(listTasks);
            ScrollView sc = new ScrollView();
            sc.Content = s;

            this.Children.Add(sc);
        }

        public void T_Tapped1(object sender, EventArgs e)
        {
            tappedUser = ((Label) (((StackLayout) sender).Children[0])).Text;
            home.det = new ContentPage() { BackgroundColor = Color.White };
            home.aux.Children.RemoveAt(1);

            home.actualPage = new DetailContactPage(user, users, home, tappedUser);
            home.aux.Children.Add(home.actualPage);

            home.title.Text = tappedUser;
            home.det.Content = home.aux;
            home.Detail = home.det;
            home.actionIcon.Source = "";

        }

        public void T_Tapped(object sender, EventArgs e)
        {
            listTasks.Children.Clear();
            loadTasks();
        }

        public void loadTasks()
        {
            listTasks.TranslationY = 15;
         
            foreach (User u in users)
            {
                StackLayout contact = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                contact.Children.Add(new Label()
                {
                    IsVisible = false,
                    Text = u.name
                });
                contact.Children.Add(new Image()
                {
                    Scale = 2.0,
                    Source = "blueCircle.png"
                });

                contact.Children.Add(new Label()
                {
                    Text = u.name[0] + "",
                    TextColor = Color.White,
                    FontSize = 22,
                    TranslationX = -30,
                    TranslationY = 15

                });

                StackLayout detailContact = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TranslationY = -6,
                };

                Label detailState = new Label()
                {
                    TextColor = Color.Gray,
                    FontSize = 14
                };
                

                if (user.inView.Contains(u))
                {
                    detailState.Text = "Tiene acceso";

                    if (user.outView.Contains(u))
                    {
                        detailState.Text += ", tienes acceso";
                    }
                    else
                    {
                        if (user.requestedOut.Contains(u))
                        {
                            detailState.Text += ", solicitud enviada";
                        }
                    }
                }
                else
                {
                   
                    if (user.outView.Contains(u))
                    {
                        detailState.Text = "No tiene acceso, tienes acceso";
                        
                    }
                    else
                    {
                        if (user.requestedIn.Contains(u) || user.requestedOut.Contains(u))
                        {
                            if (user.requestedIn.Contains(u))
                            {

                                detailState.Text += "Solicitud pendiente";
                            }
                            else
                            {
                                if (user.requestedOut.Contains(u))
                                {

                                    detailState.Text += "Solicitud enviada";
                                }
                            }

                            if (user.requestedOut.Contains(u))
                            {
                                if(!detailState.Text.Equals("Solicitud enviada"))
                                detailState.Text += ", solicitud enviada";
                            }
                        }
                        else
                        {
                            detailState.Text = "No hay accesos";
                        }

                    }
                }
                
                

                detailContact.Children.Add(new Label()
                {
                    Text = u.name,
                    FontSize = 22,
                    TranslationY = 10,
                    TextColor = Color.Black,
                });
                detailContact.Children.Add(detailState);

                detailContact.Children.Add(new StackLayout()
                {
                    HeightRequest = 1,
                    Opacity = 0.25,
                    TranslationY = 16,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Gray

                });
                contact.Children.Add(detailContact);
               TapGestureRecognizer t1 = new TapGestureRecognizer();
                
               t1.Tapped += T_Tapped1;
               contact.GestureRecognizers.Add(t1);
                  listTasks.Children.Add(contact);
            }
            
            listTasks.Children.Add(new Label() { Text = " " });

        }

    }
}
