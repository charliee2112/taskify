using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Taskify.Cell;
using Xamarin.Forms;
using Taskify.Users;

namespace Taskify.Pages
{
    class HomePage : MasterDetailPage
    {
        private User user;
        private List<User> users;
        public StackLayout header;
        public StackLayout aux;
        public ContentPage det;
        public StackLayout actualPage;
        public Label title;
        public Image actionIcon;
        private ListView listView;

        public HomePage(User aUser, List<User> someUsers)
        {
            buildHeader();
            NavigationPage.SetHasNavigationBar(this, false);
            Title = "Taskify";
            user = aUser;
            users = someUsers;
           
            List<Label> items = new List<Label>();
            items.Add(new Label() {Text = "Mis Tareas" });
            items.Add(new Label() { Text = "Mis Contactos" });
            items.Add(new Label() { Text = "Cerrar Sesion" });

            listView = new ListView();
            listView.ItemsSource = items;
            
            listView.ItemTemplate = new DataTemplate(() => {
                var sAaux = new StackLayout();
     
                var nameLabel = new Label {
                    //FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.Black,
                    TranslationY = 10,
                    TranslationX = 25,
                };
                
                nameLabel.SetBinding(Label.TextProperty, "Text");

                sAaux.Children.Add(nameLabel);
                
                return new ViewCell { View = sAaux };
            });

            listView.ItemTapped += ListView_ItemTapped;
            

            StackLayout info = new StackLayout() { 
                HeightRequest = 175,
                BackgroundColor = Color.FromRgb(0,122,255),
            };
            Image c = new Image()
            {
                Scale = 2.0,
                TranslationX = 30,
                Source = "whiteCircle.png",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TranslationY = 66,
                
            };
            
            info.Children.Add(c);

            info.Children.Add(new Label()
            {
                Text = user.name[0] + "",
                //TextColor = Color.Red,
                TextColor = Color.FromRgb(0,122,255),
                FontSize = 22,
                TranslationX = 37,
                TranslationY = 30,

                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,

            });

            /*info.Children.Add(new Image()
            {
                Source = "logo.jpg",
                Scale = 0.3
            });*/

            info.Children.Add(new Label() {
                Text = user.name,

                TextColor = Color.White,
                TranslationX = 26,
                TranslationY = -10,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 17
            });

            this.Master = new ContentPage
            {
                Title = "Mis Tareas",
                BackgroundColor = Color.White,
                Content = new StackLayout
                {
                    Children =
                    {
                        info,
                        listView
                    }
                }
            };

            // listView.SelectedItem = items[0];
            // ((Label)listView.SelectedItem).BackgroundColor = Color.Red;

            //ContentPage det = new ContentHomePage(user,users);
            det = new ContentPage() { BackgroundColor = Color.White, };
//            det = new ContentPage() { BackgroundColor = Color.FromRgb(255, 87, 34), };
            aux = new StackLayout();
            aux.Children.Add(header);
            actualPage = new ContentHomePage(user, users,this);
            aux.Children.Add(actualPage);

            det.Content = aux;
            Detail = det;

            listView.ItemSelected += (sender, args) =>
            {
                // Set the BindingContext of the detail page.
                this.Detail.BindingContext = args.SelectedItem;
                
                // Show the detail page.
                this.IsPresented = false;
            };

           
            //Navigation.InsertPageBefore(pageAux,this);

           // this.Disappearing += HomePage_Disappearing;
        
        }

        protected override bool OnBackButtonPressed()
        {
            Type t = actualPage.GetType();
            if (t == typeof(AddTask))
            {

                loadHomeDetail();
            }
            else
            {
                if (t == typeof(ContentHomePage))
                {
                    //Navigation.PopAsync();
                }
                else
                {
                    if (t == typeof(EditTask))
                    {
                        loadHomeDetail();
                    }
                    else
                    {
                        if (t == typeof(ContactPage))
                        {
                            loadHomeDetail();
                        }
                        else
                        {
                            if (t == typeof(DetailContactPage))
                            {
                                loadContactsDetail();
                            }
                            else
                            {
                                
                            }
                        }
                    }
                }
            }
            return true;
        }
        

        private void HomePage_Disappearing(object sender, EventArgs e)
        {


           
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (((Label)(e.Item)).Text == ("Mis Tareas"))
            {
                if (actualPage.GetType() == typeof(ContentHomePage))
                {
                    IsPresented = false;
                }
                else
                {
                    loadHomeDetail();
                    IsPresented = false;
                }
            }
            else
            {
                if (((Label) (e.Item)).Text == ("Mis Contactos"))
                {
                    if (actualPage.GetType() == typeof(ContactPage))
                    {
                        IsPresented = false;
                    }
                    else
                    {
                        loadContactsDetail();
                        IsPresented = false;
                    }
                }
                else
                {
                    Navigation.PopAsync();
                }
            }
        }

        private void HTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
          
            
        }
        public void buildHeader()
        {
            header = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                //VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.FromRgb(255, 87, 34),
                Padding = new Thickness(0,10,0,10),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            title = new Label()
            {
                Text = "Mis Tareas",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 20,
                TextColor = Color.White,
                TranslationX = 40,
            };

            Image h = new Image()
            {
                Source = "hamburguerIcon.png",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                TranslationX = 20

            };

            TapGestureRecognizer hTapGestureRecognizer = new TapGestureRecognizer();
            hTapGestureRecognizer.Tapped += HTapGestureRecognizer_Tapped; ;
            h.GestureRecognizers.Add(hTapGestureRecognizer);
            
            Image addTask = new Image()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Source = "addTask.png",
                Scale = 1.8,
                TranslationX = -10

            };
            TapGestureRecognizer t = new TapGestureRecognizer();
            t.Tapped += T_Tapped; ;
            addTask.GestureRecognizers.Add(t);
            actionIcon = addTask;

            header.Children.Add(h);
            header.Children.Add(title);
            header.Children.Add(actionIcon);
        }

        private void T_Tapped(object sender, EventArgs e)
        {
            Type t = actualPage.GetType();
            if (t == typeof(AddTask))
            {
                string str = ((AddTask)actualPage).addTask();
                if (string.IsNullOrEmpty(str))
                {
                    loadHomeDetail();
                }
                else
                {
                    DisplayAlert("Tarea no creada", str, "Ok");
                }
            }
            else
            {
                if (t == typeof(ContentHomePage))
                {
                    det = new ContentPage() {BackgroundColor = Color.White};
                    aux.Children.RemoveAt(1);
                    actualPage = new AddTask(user, users,this);
                    aux.Children.Add(actualPage);

                    title.Text = "Nueva Tarea";
                    actionIcon.Source = "tickIcon.png";
                    
                    det.Content = aux;
                    Detail = det;
                }
                else
                {
                    if (t == typeof(EditTask))
                    {
                        ((EditTask)actualPage).editTask();
                        loadHomeDetail();
                    }
                    else
                    {
                        
                    }
                }
            }
            
        }

        public void loadStateDetail(Item.Item i)
        {
            det = new ContentPage() { BackgroundColor = Color.White };
            aux.Children.RemoveAt(1);
            actualPage = new stateSelectPage(user, users,i, this);
            aux.Children.Add(actualPage);

            title.Text = "Seleccione un estado";
            actionIcon.Source = "";

            det.Content = aux;
            Detail = det;
        }

        public void loadHomeDetail()
        {
            det = new ContentPage() { BackgroundColor = Color.White };
            aux.Children.RemoveAt(1);
            actualPage = new ContentHomePage(user, users,this);
            aux.Children.Add(actualPage);

            title.Text = "Mis Tareas";
            actionIcon.Source = "addTask.png";
            
            det.Content = aux;
            Detail = det;
        }

       

        private void loadContactsDetail()
        {
            det = new ContentPage() { BackgroundColor = Color.White };
            aux.Children.RemoveAt(1);
            actualPage = new ContactPage(user, users,this);
            aux.Children.Add(actualPage);

            actionIcon.Source = "";
            title.Text = "Contactos";

            det.Content = aux;
            Detail = det;
        }

        private void TapAux_Tapped(object sender, EventArgs e)
        {
           loadHomeDetail();
        }



    }
}
