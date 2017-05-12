using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Taskify.Cell;
using Xamarin.Forms;
using Taskify.Users;

namespace Taskify.Pages
{
    class HomePage : TabbedPage
    {
        private Grid grid;
        private User user;
        private List<User> users;
        private ContentPage home;
        private ContentPage contacts;

        public HomePage(User aUser, List<User> someUsers)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Title = "Taskify";
            user = aUser;
            users = someUsers;
            home = new ContentPage()
            {
                Title = "My Tasks"
            };
            contacts = new ContentPage()
            {
                Title = "contacts"
            };

            loadHome();
            loadContacts();

            this.Children.Add(home);
            this.Children.Add(contacts);
            this.Appearing += HomePage_Appearing;
            this.Disappearing += HomePage_Disappearing;

        }

        private void HomePage_Disappearing(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }

        private void loadContacts()
        {
            ScrollView scroll = new ScrollView();
            grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 300});
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 40 });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 40 });



            Label header = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                Text = "My contacts:",
                FontSize = 30
            };

            grid.Children.Add(header, 0, 0);
            addContacts(grid);
            addSentReq(grid);
            addRequests(grid);
            addOthers(grid);

            scroll.Content = grid;
            contacts.Content = scroll;
        }

        private void addSentReq(Grid grid)
        {
            if (user.requestedOut.Count != 0)
            {
                Label header = new Label()
                {
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    Text = "Sent Requests:",
                    FontSize = 30
                };

                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                grid.Children.Add((header), 0, grid.RowDefinitions.Count);

                foreach (User u in user.requestedOut)
                {

                    Image cancel = new Image()
                    {
                        Source = "error_icon.png",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        TranslationX = -20,
                        BindingContext = u
                    };

                    TapGestureRecognizer cancelTapGestureRecognizer = new TapGestureRecognizer();
                    cancelTapGestureRecognizer.Tapped += CancelTapGestureRecognizer_Tapped;
                    cancel.GestureRecognizers.Add(cancelTapGestureRecognizer);
                    

                    grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    grid.Children.Add(new HomeContactCell(u, user.requestedOut), 0, grid.RowDefinitions.Count);

                    grid.Children.Add(cancel, 2, grid.RowDefinitions.Count);
                }
            }
        }

        private void CancelTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            User tmpUser = (User)((Image)sender).BindingContext;
            tmpUser.deleteRequest(user);
            loadContacts();
        }
        
        private void addOthers(Grid grid)
        {
            List<User> tmp = new List<User>();
            foreach (User u in users)
            {
                if (!user.outView.Contains(u) && !user.requestedOut.Contains(u))
                {
                    tmp.Add(u);
                }
            }
            tmp.Remove(user);
            //Aqui tmp contiene todos los usuarios que no puedo ver ni todavia envie solicitud

            if (tmp.Count != 0)
            {
                Label header = new Label()
                {
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    Text = "Send Requests:",
                    FontSize = 30
                };

                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                grid.Children.Add((header), 0, grid.RowDefinitions.Count);

                foreach (User u in tmp)
                {
                    Image addNewContact = new Image()
                    {
                        Source = "plus_icon.png",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        TranslationX = -20,
                        BindingContext = u
                    };

                    TapGestureRecognizer addNewContactTapGestureRecognizer = new TapGestureRecognizer();
                    addNewContactTapGestureRecognizer.Tapped += AddNewContactTapGestureRecognizer_Tapped;
                    addNewContact.GestureRecognizers.Add(addNewContactTapGestureRecognizer);
                    
                    grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    grid.Children.Add(new HomeContactCell(u, tmp), 0, grid.RowDefinitions.Count);
                    grid.Children.Add(addNewContact, 2, grid.RowDefinitions.Count);

                }
            }

        }

        private void AddNewContactTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            User tmpUser = (User)((Image)sender).BindingContext;
            user.sendRequest(tmpUser);
            loadContacts();
        }

        private void addContacts(Grid grid)
        {
            if (user.outView.Count != 0)
            {
                foreach (User u in user.outView)
                {
                    Image accept = new Image()
                    {
                        Source = "doc_icon.png",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        TranslationX = -20,
                        BindingContext = u
                    };

                    TapGestureRecognizer acceptTapGestureRecognizer = new TapGestureRecognizer();
                    acceptTapGestureRecognizer.Tapped += AcceptTapGestureRecognizer_Tapped;
                    accept.GestureRecognizers.Add(acceptTapGestureRecognizer);

                    Image selfRevoke = new Image()
                    {
                        Source = "delete_icon.png",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        TranslationX = -20,
                        BindingContext = u
                    };

                    TapGestureRecognizer selfRevokeTapGestureRecognizer = new TapGestureRecognizer();
                    selfRevokeTapGestureRecognizer.Tapped += SelfRevokeTapGestureRecognizer_Tapped;
                    selfRevoke.GestureRecognizers.Add(selfRevokeTapGestureRecognizer);

                    grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    grid.Children.Add(new HomeContactCell(u, user.outView), 0, grid.RowDefinitions.Count);
                    grid.Children.Add(accept, 1, grid.RowDefinitions.Count);
                    grid.Children.Add(selfRevoke, 2, grid.RowDefinitions.Count);

                }
            }
            else
            {
                Label noContacts = new Label()
                {
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    Text = "No one yet",
                    FontSize = 20
                };
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                grid.Children.Add(noContacts, 0, grid.RowDefinitions.Count);

            }
            
        }

        private async void SelfRevokeTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            User tmpUser = (User)((Image)sender).BindingContext;
            bool answer = await DisplayAlert("Remove?", "Are you sure you want to stop viewing "+ tmpUser.name+"'s tasks?", "No", "Yes");
            if (!answer)
            {
                tmpUser.revokeUser(user);
                loadContacts();
            }
        }

        private void AcceptTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            User tmpUser = (User)((Image)sender).BindingContext;
            Navigation.PushAsync(new ViewContact(tmpUser,users));
        }     

        private void addRequests(Grid grid)
        {
            if (user.inView.Count != 0)
            {
                Label header = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    Text = "Sharing with:",
                    FontSize = 30
                };
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                grid.Children.Add((header), 0, grid.RowDefinitions.Count);

                foreach (User u in user.inView)
                {
                    Image delete = new Image()
                    {
                        Source = "delete_icon.png",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        TranslationX = -20,
                        BindingContext = u
                    };
                    TapGestureRecognizer deleteTapGestureRecognizer = new TapGestureRecognizer();
                    deleteTapGestureRecognizer.Tapped += DeleteTapGestureRecognizer_Tapped;
                    delete.GestureRecognizers.Add(deleteTapGestureRecognizer);

                    grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    grid.Children.Add(new HomeContactCell(u, user.inView), 0, grid.RowDefinitions.Count);
                    grid.Children.Add(delete, 2, grid.RowDefinitions.Count);

                }
            }
            if (user.requestedIn.Count != 0)
            {
                Label req = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    Text = "Requesting view:",
                    FontSize = 30
                };
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                grid.Children.Add((req), 0, grid.RowDefinitions.Count);

                foreach (User u in user.requestedIn)
                {
                    Image acceptReq = new Image()
                    {
                        Source = "tick_icon.png",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        TranslationX = -20,
                        BindingContext = u
                    };
                    TapGestureRecognizer acceptReqTapGestureRecognizer = new TapGestureRecognizer();
                    acceptReqTapGestureRecognizer.Tapped += AcceptReqTapGestureRecognizer_Tapped;
                    acceptReq.GestureRecognizers.Add(acceptReqTapGestureRecognizer);

                    grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    grid.Children.Add(new HomeContactCell(u, user.requestedIn), 0, grid.RowDefinitions.Count);
                    grid.Children.Add(acceptReq, 2, grid.RowDefinitions.Count);

                }
            }
        }

        private void AcceptReqTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            User tmpUser = (User)((Image)sender).BindingContext;
            user.acceptRequest(tmpUser);
            loadContacts();
        }

        private async void DeleteTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            User tmpUser = (User)((Image)sender).BindingContext;
            bool answer = await DisplayAlert("Revoke?", "Are you sure you want " + tmpUser.name + " to stop viewing your tasks?", "No", "Yes");
            if (!answer)
            {
                user.revokeUser(tmpUser);
                loadContacts();
            }
        }

        private void loadHome()
        {
            ScrollView scroll = new ScrollView();
            grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Label header = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                Text = "To do List:",
                FontSize = 30
            };

            Image newTask = new Image()
            {

                Source = "plus_icon.png",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TranslationX = -27
            };

            TapGestureRecognizer newTaskTapGestureRecognizer = new TapGestureRecognizer();
            newTaskTapGestureRecognizer.Tapped += NewTaskTapGestureRecognizer_Tapped;
            newTask.GestureRecognizers.Add(newTaskTapGestureRecognizer);
            
            grid.Children.Add(header, 0, 0);
            grid.Children.Add(newTask, 0, 0);
            
            List<Item.Item> list = new List<Item.Item>();
            
            foreach (Item.Item i in user.getTasks())
            {
                list.Add(i);
            }
            list.Sort();

            user.getTasks().Clear();

            foreach (Item.Item i in list)
            {
                user.getTasks().Add(i);
            }

            for (int i = 0; i < user.getTasks().Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                grid.Children.Add(new HomeItemCell(user.getTasks()[i], user, false,users), 0, i + 1);

            }
            if (user.getTasks().Count == 0)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                Label empty = new Label();
                empty.Text = "No task to display";
                grid.Children.Add(empty, 0, 1);

            }
            scroll.Content = grid;
            home.Content = scroll;
        }

        private void HomePage_Appearing(object sender, EventArgs e)
        {

            loadHome();
        }

        private void NewTaskTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTask(user,users));
        }
        
        private void addItems(Grid grid)
        {           
            for (int i = 0; i < user.getTasks().Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                grid.Children.Add(new HomeItemCell(user.getTasks()[i], user,false,users), 0, i + 1);
            }

        }
        
    }
}
