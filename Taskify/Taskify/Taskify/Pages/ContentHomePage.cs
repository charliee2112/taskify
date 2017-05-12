using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Taskify.Users;

namespace Taskify.Pages
{
    class ContentHomePage:StackLayout
    {
        private StackLayout listTasks;
        private StackLayout s;
        private User user;
        private List<User> users ;
        public Item.Item upd;
        private HomePage home;

        public ContentHomePage(User u, List<User> us, HomePage h)
        {
            home = h;
            VerticalOptions = LayoutOptions.FillAndExpand;
            users = us;
            user = u;
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
                TranslationX = 23,
                TranslationY = 15,
                Spacing = 25
            };

            s.Children.Add(listTasks);
            ScrollView sc = new ScrollView();
            sc.Content = s;
            
            loadTasks();
            this.Children.Add(sc);

        }

      
        private void THome_Tapped(object sender, EventArgs e)
        {
            listTasks.Children.Clear();
            loadTasks();
        }

        private void loadTasks()
        {
            bool p = true;
            bool noExpirated = true;
            List<Item.Item> l = new List<Item.Item>(user.getTasks());
            if (l.Count == 0)
            {
                listTasks.Children.Add(new Label() { Text = "No tienes tareas para mostrar" });
            }
            else
            {
                l.Sort();
                listTasks.TranslationY = -5;
               
                foreach (Item.Item i in l)
                {
                    if (i.expirated && p)
                    {
                        if (i.expirated)
                        {
                            Label a = new Label()
                            {
                                TranslationY = 10,
                                Text = "Atrasadas:",
                                TextColor = Color.Black,
                                FontSize = 17
                            };
                            listTasks.Children.Add(a);
                        }
                        p = false;
                    }
                    if (noExpirated && !i.expirated)
                    {
                        Label a = new Label()
                        {
                            Text = "A tiempo:",
                            TranslationY = 10,
                            TextColor = Color.Black,
                            FontSize = 17
                        };
                        listTasks.Children.Add(a);
                        noExpirated = false;

                    }

                    StackLayout descTask = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,

                    };
                    Image icon = new Image();
                    if (i.state.Equals("Pendiente"))
                    {
                        icon = new Image()
                        {
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                            Scale = 2,
                            Source = "clockIcon.png",
                        };
                        descTask.Children.Add(icon);
                        
                    }
                    else
                    {
                        if (i.state.Equals("En Proceso"))
                        {
                            icon = new Image()
                            {
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Start,
                                Source = "playIcon.png",
                                Scale = 2
                            };
                            descTask.Children.Add(icon);
                        }
                        else
                        {
                            icon = new Image()
                            {
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Start,
                                Scale = 2,
                                Source = "clockIcon.png",

                            };
                            descTask.Children.Add(icon);
                            
                        }
                    }
                    StackLayout detailTask = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        TranslationX = 18,
                    };
                    StackLayout task = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };
                    detailTask.Children.Add(task);
                    task.Children.Add(new Label()
                    {
                        Text = i.title,
                        TextColor = Color.Black,
                        FontSize = 18
                    });
                    task.Children.Add(new Label()
                    {
                        Text = " (" + i.state + ") ",
                        TextColor = Color.FromRgb(127, 127, 127),
                        FontSize = 18
                    });
                    detailTask.Children.Add(new Label()
                    {
                        Text = i.GetExpDate(),
                        FontSize = 16,
                        TextColor = Color.FromRgb(127, 127, 127),
                        TranslationY = -5
                    });
                    detailTask.Children.Add(new StackLayout()
                    {
                        HeightRequest = 1,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.Gray,
                        Opacity = 0.25,
                        TranslationY = 5

                    });
                    descTask.Children.Add(detailTask);
                    listTasks.Children.Add(descTask);

                    TapGestureRecognizer tItem = new TapGestureRecognizer();
                    tItem.Tapped += TItem_Tapped;
                    descTask.GestureRecognizers.Add(tItem);

                }
            }
            listTasks.Children.Add(new Label() { Text = " " });
        }

        private void TItem_Tapped(object sender, EventArgs e)
        {
            string taskName=((Label)((StackLayout)(((StackLayout)((StackLayout)sender).Children[1]).Children[0])).Children[0]).Text;
            upd = user.getTask(taskName);

            home.det = new ContentPage() { BackgroundColor = Color.White };
            home.aux.Children.RemoveAt(1);
            home.actualPage = new EditTask(user, users, upd, home);
            home.aux.Children.Add(home.actualPage);

            home.title.Text = "Editando "+taskName;
            home.actionIcon.Source = "addTask.png";

            home.det.Content = home.aux;
            home.Detail = home.det;

            home.actionIcon.Source = "tickIcon.png";

        }
    }
}
