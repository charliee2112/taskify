using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Taskify.Users;
using System.Collections.ObjectModel;
using Taskify.Cell;

namespace Taskify.Pages
{
    class ViewContact:ContentPage
    {
        private User user;
        private List<User> users;

        public ViewContact(User u,List<User> someUsers)
        {
            user = u;
            users = someUsers;
            ScrollView scroll = new ScrollView();
            Grid grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Label header = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                Text = u.name+"'s To do List:",
                FontSize = 30
            };
           
            grid.Children.Add(header, 0, 0);
           
            ObservableCollection<Item.Item> list = new ObservableCollection<Item.Item>();

            foreach (Item.Item task in u.getTasks())
            {
                list.Add(task);

            }
            List<Taskify.Item.Item> l = new List<Taskify.Item.Item>();
            l.AddRange(list);
            l.Sort();
            for (int i = 0; i < l.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                grid.Children.Add(new HomeItemCell(l[i], user, true,users), 0, i + 1);

            }
            scroll.Content = grid;
            this.Content = scroll;

        }
    }
}
