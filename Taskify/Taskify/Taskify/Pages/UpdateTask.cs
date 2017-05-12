using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Taskify.Users;
using System.Collections.ObjectModel;

namespace Taskify.Pages
{
    class UpdateTask : ContentPage
    {
        private Entry nameEntry = new Entry();
        private Entry descEntry = new Entry();
        private Item.Item updatingItem;
        private Button delete = new Button();
        private DatePicker expDatePicker;
        private ListView logItem;
        private User user;
        private List<User> users;

        public UpdateTask(Item.Item task, User u,List<User> someUsers)
        {
            user = u;
            users = someUsers;
            updatingItem = task;
            var stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        Text = "Updating: "+task.title,
                        FontSize = 20
                    }

                }
                
            };

            Label nameLabel = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "Name: ",
                FontSize = 20
            };
            Label descLabel = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "Description: ",
                FontSize = 20
            };
            
            Label expLabel = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "Expiration date: ",
                FontSize = 20
            };
            expDatePicker = new DatePicker();
            expDatePicker.Date = updatingItem.GetExpDateTime();
            nameEntry.Placeholder = task.title;
            descEntry.Placeholder = task.desc;
            armarLog();
            if (updatingItem.desc == "") descEntry.Placeholder = "No description";
            armarLog();
            delete.Text = "Delete Task";
            delete.Clicked += Delete_Clicked;

            stack.Children.Add(nameLabel);
            stack.Children.Add(nameEntry);
            stack.Children.Add(descLabel);
            stack.Children.Add(descEntry);
            stack.Children.Add(expLabel);
            stack.Children.Add(expDatePicker);
            stack.Children.Add(delete);
            stack.Children.Add(logItem);
            Content = stack;
            
            this.Disappearing += UpdateTask_Disappearing;
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Task", "Are you sure of deleting all of this task's data? This action cannot be undone.", "No", "Yes");
            if (!answer)
            {
                user.getTasks().Remove(updatingItem);
                Navigation.RemovePage(this);
            }
        }

        private void armarLog()
        {
            logItem = new ListView();
            List<Tuple<DateTime, string>> log = updatingItem.getLogList();
            logItem.ItemsSource = log;
        }

        private void UpdateTask_Disappearing(object sender, EventArgs e)
        {
            if (nameEntry.Text != null)
                updatingItem.title = nameEntry.Text;
            else
                updatingItem.title = nameEntry.Placeholder;
            if (descEntry.Text != null)
                updatingItem.desc = descEntry.Text;
            else
                updatingItem.desc = descEntry.Placeholder;
                updatingItem.setExpDate(expDatePicker.Date);

            
            //Solucion chancha para cuando demoran en actualizarse los items
            //Navigation.PopAsync();
            //Navigation.PushAsync(new HomePage(user, users));
            
        }
        
    }
}
