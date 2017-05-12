using System;
using System.Collections.Generic;
using System.Text;
using Taskify.Cell;
using Taskify.Users;
using Xamarin.Forms;

namespace Taskify.Pages
{
    class TasksDetail : ContentPage
    {
        private StackLayout s;
        private User user;
        private List<User> users;

        public TasksDetail(User u, List<User> us) {
            user = u;
            users = us; 
            ScrollView scroll = new ScrollView();
             s = new StackLayout()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

        Label header = new Label
        {
            HorizontalTextAlignment = TextAlignment.Start,
            VerticalTextAlignment = TextAlignment.Start,
            Text = "Atrasadas",
            FontSize = 30
        };
            
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

            for (int i = 0; i<user.getTasks().Count; i++)
            {
                s.Children.Add(new HomeItemCell(user.getTasks()[i], user, false, users));

            }
            if (user.getTasks().Count == 0)
            {
                Label empty = new Label();
                empty.Text = "No task to display";
                s.Children.Add(empty);
            }
            scroll.Content = s;
            this.Content = scroll;
        }
    }
}
