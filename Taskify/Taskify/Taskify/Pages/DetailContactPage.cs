using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Taskify.Users;

namespace Taskify.Pages
{
    class DetailContactPage : StackLayout
    {
        private HomePage home;
        private User user;
        private List<User> users;
        private StackLayout s;
        private StackLayout tasks;
        private User other;
        private Image state1;
        private Image state2;
        //private StackLayout auxStack;

        public DetailContactPage(User u, List<User> us, HomePage h, string viewing)
        {
            users = us;
            user = u;
            User auxUser = new User(viewing, 0000);
            auxUser = users[users.IndexOf(auxUser)];
            home = h;
            other = auxUser;
            VerticalOptions = LayoutOptions.FillAndExpand;
           
           load();
        }

        private void load()
        {
            this.Children.Clear();
            s = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
            };

            tasks = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                TranslationX = 23,
                Spacing = 22
            };

            s.Children.Add(tasks);
            ScrollView sc = new ScrollView();
            sc.Content = s;

            loadInfo();
            tasks.Children.Add(new StackLayout() { HeightRequest = 1, BackgroundColor = Color.Gray, Opacity = 0.25 });

            loadState();

            if (other.inView.Contains(user))
            {
                tasks.Children.Add(new StackLayout() {  HeightRequest = 1, BackgroundColor = Color.Gray, Opacity = 0.25 });
                loadTasks();
            }
            this.Children.Add(sc);

        }

        private void loadState()
        {
            StackLayout state = new StackLayout() {};
            if (user.requestedIn.Contains(other))
            {
                StackLayout sAux = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    Padding = new Thickness(0, 0, 40, 0)
                };

                state.Children.Add(new Label()
                {
                    Text = other.name + " te ha enviado una solicitud de permiso:",
                    TextColor = Color.Black,
                    FontSize = 14.5,

                });
                Button acceptRequestButton = new Button()
                {
                    Text = "Aceptar",
                    BorderRadius = 2,
                    WidthRequest = 100,
                    BackgroundColor = Color.FromRgb(0, 122, 255),
                    TextColor = Color.White,
                };
                acceptRequestButton.Clicked += AcceptRequestButton_Clicked;
                Button denyRequestButton = new Button()
                {
                    Text = "Rechazar",
                    BorderRadius = 2,
                    WidthRequest = 105,
                    BackgroundColor = Color.FromRgb(0, 122, 255),
                    TextColor = Color.White,

                };
                denyRequestButton.Clicked += DenyRequestButton_Clicked;
                sAux.Children.Add(acceptRequestButton);
                sAux.Children.Add(denyRequestButton);
                state.Children.Add(sAux);
            }
            else
            {
                if (user.inView.Contains(other))
                {
                    StackLayout sAux = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Padding = new Thickness(0, 0, 40, 0)
                    };

                    state.Children.Add(new Label()
                    {
                        Text = other.name + " tiene permiso para ver tus tareas",
                        TextColor = Color.Black,
                        FontSize = 14.5,

                    });
                    Button revoke = new Button()
                    {
                        Text = "Revocar",
                        BorderRadius = 2,
                        WidthRequest = 100,
                        BackgroundColor = Color.FromRgb(0, 122, 255),
                        TextColor = Color.White,
                    };
                    revoke.Clicked += Revoke_Clicked;
                    sAux.Children.Add(revoke);
                    state.Children.Add(sAux);
                }

            }

            if (user.requestedOut.Contains(other))
            {
                StackLayout sAux = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    Padding = new Thickness(0, 0, 40, 0)
                };

                state.Children.Add(new Label()
                {
                    Text = "Has enviado una solicitud de permiso a " + other.name,
                    TextColor = Color.Black,
                    FontSize = 14.5,

                });
                Button cancelRequestButton = new Button()
                {
                    Text = "Cancelar",
                    BorderRadius = 2,
                    WidthRequest = 100,
                    BackgroundColor = Color.FromRgb(0, 122, 255),
                    TextColor = Color.White,
                };
                cancelRequestButton.Clicked += CancelRequestButton_Clicked;
                sAux.Children.Add(cancelRequestButton);
                state.Children.Add(sAux);
            }
            else
            {
                if (user.outView.Contains(other))
                {
                    StackLayout sAux = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Padding = new Thickness(0, 0, 40, 0)
                    };

                    state.Children.Add(new Label()
                    {
                        Text = "Tienes permiso para ver las tareas de " + other.name,
                        TextColor = Color.Black,
                        FontSize = 14.5,

                    });
                    Button stopFollow = new Button()
                    {
                        Text = "Olvidar",
                        BorderRadius = 2,
                        WidthRequest = 100,
                        BackgroundColor = Color.FromRgb(0, 122, 255),
                        TextColor = Color.White,
                    };
                    stopFollow.Clicked += StopFollow_Clicked;
                    sAux.Children.Add(stopFollow);
                    state.Children.Add(sAux);
                }
                else
                {
                    StackLayout sAux = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Padding = new Thickness(0, 0, 40, 0)
                    };

                    state.Children.Add(new Label()
                    {
                        Text = "Puedes enviarle solicitud a " + other.name,
                        TextColor = Color.Black,
                        FontSize = 14.5,

                    });
                    Button sendRequest = new Button()
                    {
                        Text = "Solicitar",
                        BorderRadius = 2,
                        WidthRequest = 100,
                        BackgroundColor = Color.FromRgb(0, 122, 255),
                        TextColor = Color.White,
                    };
                    sendRequest.Clicked += SendRequest_Clicked;
                    sAux.Children.Add(sendRequest);
                    state.Children.Add(sAux);

                }
            }
            tasks.Children.Add(state);

        }

        private void SendRequest_Clicked(object sender, EventArgs e)
        {
            user.sendRequest(other);
            load();
        }

        private async void StopFollow_Clicked(object sender, EventArgs e)
        {
            //Device.BeginInvokeOnMainThread();
            var aux = await home.DisplayAlert("Olvidar", "Esta seguro que desea dejar de ver las tareas de " + other.name + "?",
                    "No", "Si");
            if (!aux) {
                user.outView.Remove(other);
                other.inView.Remove(user);
            }
            load();
        }

        private void CancelRequestButton_Clicked(object sender, EventArgs e)
        {
            other.deleteRequest(user);
            load();
        }

        private async void Revoke_Clicked(object sender, EventArgs e)
        {
            var aux = await home.DisplayAlert("Revocar Permiso", "Esta seguro que desea revocar el permiso de " + other.name + "?",
                    "No", "Si");
            if (!aux)
                user.revokeUser(other);
            load();
        }

        private void DenyRequestButton_Clicked(object sender, EventArgs e)
        {
            user.denyRequest(other);
            load();
        }

        private void AcceptRequestButton_Clicked(object sender, EventArgs e)
        {
            user.acceptRequest(other);
            load();
        }

        private void loadInfo()
        {

            StackLayout info = new StackLayout() {  Orientation = StackOrientation.Horizontal };
           
            StackLayout infoMe = new StackLayout()
            {
                WidthRequest = 100,
                TranslationX = -20,
               Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.StartAndExpand };
            StackLayout infoRel = new StackLayout()
            {
                TranslationX = -12,
                WidthRequest = 200, Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.Center
            };
            StackLayout infoHim = new StackLayout()
            {
                WidthRequest = 100,
                TranslationX = -30, Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.EndAndExpand };
            info.Children.Add(infoMe);
            info.Children.Add(infoRel);
            info.Children.Add(infoHim);

            Image circleMe = new Image()
            {
                Source = "blueCircle.png",
                Scale = 2.5,
                TranslationY = 16
            };
            Label letterMe = new Label()
            {
                Text = user.name[0] + "",
                TextColor = Color.White,
                FontSize = 25,
                TranslationY = -25,
                TranslationX = 30,

            };
            Label nameMe = new Label()
            {
                TranslationY = -8,
                Text = user.name,
                TranslationX = 13,
                TextColor = Color.FromRgb(40, 40, 40)
            };

            infoMe.Children.Add(circleMe);
            infoMe.Children.Add(letterMe);
            infoMe.Children.Add(nameMe);

            loadRel(infoRel);

            Image circleHim = new Image()
            {
                Source = "blueCircle.png",
                TranslationY = 16,
                Scale = 2.5
            };
            Label letterHim = new Label()
            {
                Text = other.name[0] + "",
                TextColor = Color.White,
                FontSize = 25,
                TranslationY = -24,
                TranslationX = 30
                
            };
            if (letterMe.Text.Equals("M"))
            {
                letterMe.TranslationX = 26.5;
            }
            else
            {
                letterMe.TranslationX = 30;
            }
            if (letterHim.Text.Equals("M"))
            {
                letterHim.TranslationX = 26.5;
            }
            else
            {
                letterHim.TranslationX = 30;
            }
            Label nameHim = new Label()
            {
                TranslationY = -8,
                Text = other.name,
                HorizontalOptions = LayoutOptions.End,
                TranslationX = -10,
                TextColor = Color.FromRgb(40,40,40)
            };
            infoHim.Children.Add(circleHim);
            infoHim.Children.Add(letterHim);
            infoHim.Children.Add(nameHim);


            tasks.Children.Add(new Label()
            {
                Text = " ",
                TextColor = Color.Black,
                FontSize = 17,
                TranslationX = -3,
            });
            tasks.Children.Add(info);

        }

        private void loadRel(StackLayout stackRel)
        {

            
            stackRel.Children.Add(new Image()
            {
                Source = "arrowRight.png",
                TranslationY = -12,
                TranslationX = -15
            });

            state1 = new Image()
            {
                Scale = 2.5,
                TranslationY = -44,
                TranslationX = -14,
            };
           
            stackRel.Children.Add(state1);

            stackRel.Children.Add(new Image()
            {
                Source = "arrowLeft.png",
                TranslationX = -15,
                TranslationY= -20,
            });
            state2 = new Image()
            {
                Scale = 2.5,
                TranslationY = -48,
                TranslationX = -14,
            };
            stackRel.Children.Add(state2);
            updateIcons();
            
        }

        private void updateIcons()
        {
            if (user.outView.Contains(other))
            {
                state1.Source = "eye.png";
            }
            else
            {
                if (user.requestedOut.Contains(other))
                {
                    state1.Source = "clock.png";
                }
                else
                {
                    state1.Source = "person.png";
                }
            }

            if (user.inView.Contains(other))
            {
                state2.Source = "eye.png";
            }
            else
            {
                if (user.requestedIn.Contains(other))
                {
                    state2.Source = "clock.png";
                }
                else
                {
                    state2.Source = "person.png";
                }
            }
            
        }

        private void loadTasks()
        {
            StackLayout listTasks = new StackLayout()
            {
                //BackgroundColor = Color.Green
            };
            bool p = true;
            bool noExpirated = true;
            List<Item.Item> l = new List<Item.Item>(other.getTasks());
            if (l.Count == 0)
            {
                listTasks.Children.Add(new Label() { Text = "No tienes tareas para mostrar" });
            }
            else
            {
                l.Sort();

                if (noExpirated && !l[0].expirated)
                {
                    Label a = new Label()
                    {
                        TranslationY = 10,
                        Text = "Atrasadas:",
                        TextColor = Color.Black,
                        FontSize = 17
                    };
                    listTasks.Children.Add(a);
                    noExpirated = false;

                }
                foreach (Item.Item i in l)
                {
                    if (i.expirated && p)
                    {
                        if (i.expirated)
                        {
                            Label a = new Label()
                            {
                                TranslationY = 0,
                                Text = "A tiempo:",
                                TextColor = Color.Black,
                                FontSize = 17
                            };
                            /*listTasks.Children.Add(new Label()
                            {
                                Text = " ",
                                TextColor = Color.Black,
                                FontSize = 17
                            });*/
                            listTasks.Children.Add(a);
                        }
                        p = false;
                    }
                    Image icon = new Image();
                    if (i.state.Equals("Pendiente"))
                    {
                        icon = new Image()
                        {
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                            Source = "clockIcon.png",
                            Scale = 1.666
                        };
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
                                Scale = 1.666
                            };
                        }
                        else
                        {
                            icon = new Image()
                            {
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Start,
                                Source = "clockIcon.png",
                                Scale = 1.666

                            };
                        }
                    }
                    StackLayout descTask = new StackLayout()
                    {
                        //TranslationY = 30,
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        
                    };
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
                        
                    });
                    descTask.Children.Add(icon);
                    descTask.Children.Add(detailTask);
                    listTasks.Children.Add(descTask);
                    
                    tasks.Children.Add(listTasks);
                }
            }
        }
    }
}