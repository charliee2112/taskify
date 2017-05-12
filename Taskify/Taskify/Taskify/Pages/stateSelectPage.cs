using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Taskify.Users;

namespace Taskify.Pages
{
    class stateSelectPage : StackLayout
    {
        private User user;
        private List<User> users;
        private Item.Item item;
        private ListView list;
        private HomePage home;
        public stateSelectPage(User u, List<User> us, Item.Item i, HomePage h)
        {
            home = h;
            item = i;
            user = u;
            users = us;
            list = new ListView();
            list.ItemsSource = Item.Item.states;
            this.Children.Add(list);
            list.ItemSelected += List_ItemSelected;
        }

        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            item.state = e.SelectedItem.ToString();
            home.det = new ContentPage() { BackgroundColor = Color.White };
            home.aux.Children.RemoveAt(1);
            home.actualPage = new EditTask(user, users, item, home);
            home.aux.Children.Add(home.actualPage);

            home.title.Text = "Editando " + item.title;
            home.det.Content = home.aux;
            home.Detail = home.det;

            home.actionIcon.Source = "tickIcon.png";
        }
    }
}
