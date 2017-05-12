using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Taskify.Users;

namespace Taskify.Pages
{
    class ReasonLogChange : ContentPage
    {
        private Item.Item item;
        private Entry reasonEntry = new Entry();
        private Button done = new Button();
        private bool answer;
        private User user;
        private List<User> users;

        public ReasonLogChange(Item.Item anItem, User u, List<User> us)
        {
            user = u;
            users = us;
            this.item = anItem;
            //Primero pregunto si el usuario quiere agregar una razon por la cual actualiza el estado de la tarea
            this.Appearing += ReasonLogChange_Appearing;
            this.Disappearing += ReasonLogChange_Disappearing;
        }

        private void ReasonLogChange_Disappearing(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage(user, users));
        }

        private async void ReasonLogChange_Appearing(object sender, EventArgs e)
        {
            answer = await DisplayAlert("Explanation", "Should you explain any reason for this state change?", "No", "Yes");
            if (!answer)
            {
                init();
            }
            else
            {
                Navigation.RemovePage(this);
                item.addLogLine("");
            }
        }

        public void init()
        {
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
                        Text = "Reason of State Update:",
                        FontSize = 20
                    }

                }
            };
            done.Text = "Done";
            done.Clicked += Done_Clicked;

            stack.Children.Add(reasonEntry);
            stack.Children.Add(done);
            this.Content = stack;
        }

        private void Done_Clicked(object sender, EventArgs e)
        {

            item.addLogLine(reasonEntry.Text);
            Navigation.RemovePage(this);

        }
    }
}
