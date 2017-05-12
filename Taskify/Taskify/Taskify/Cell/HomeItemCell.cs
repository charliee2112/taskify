using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Taskify.Pages;
using Xamarin.Forms;
using Taskify.Users;

namespace Taskify.Cell
{
    class HomeItemCell : StackLayout
    {
        private StackLayout head;
        private Label titleLabel;
        private Label stateLabel;
        private Image prev;
        private Image next;
        private Item.Item item;
        private User user;
        private List<User> users;

        public HomeItemCell(Item.Item unItem, User u, bool readOnly, List<User> someUsers)
        {
            user = u;
            users = someUsers;
            this.item = unItem;          
            this.Orientation = StackOrientation.Horizontal;
            this.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.VerticalOptions= LayoutOptions.Start;


            titleLabel = new Label();
            titleLabel.Text = item.title;
            titleLabel.FontSize = 20;

            stateLabel = new Label();
            stateLabel.BindingContext = item;
            stateLabel.SetBinding(Label.TextProperty,"state");
            stateLabel.FontSize = 12;
            stateLabel.Text += "  to  " + item.GetExpDate();

            head = new StackLayout();
            head.Orientation = StackOrientation.Vertical;
            head.HorizontalOptions = LayoutOptions.FillAndExpand;
            head.VerticalOptions = LayoutOptions.Start;

            prev = new Image()
            {
                Source = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/8/8e/1leftarrow.png")),
                WidthRequest = 40,
                HeightRequest = 40,
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End

            };
            next = new Image()
            {
                Source = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/3/3c/1rightarrow.png")),
                WidthRequest = 40,
                HeightRequest = 40,
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End
            };


            head.Children.Add(titleLabel);
            head.Children.Add(stateLabel);
            this.Children.Add(head);

            if (item.GetExpDateTime() < DateTime.Today)
            {
                Image warning = new Image()
                {
                    Source = ImageSource.FromUri(new Uri("http://www.freeiconspng.com/uploads/warning-icon-png-26.png")),
                    WidthRequest = 40,
                    HeightRequest = 40,
                    Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.End
                };
                this.Children.Add(warning);
            }

            if (!readOnly)
            {
                this.Children.Add(prev);
                this.Children.Add(next);
                TapGestureRecognizer nextTapGestureRecognizer = new TapGestureRecognizer();
                nextTapGestureRecognizer.Tapped += NextTapGestureRecognizer_Tapped; ;
                next.GestureRecognizers.Add(nextTapGestureRecognizer);
                TapGestureRecognizer prevTapGestureRecognizer = new TapGestureRecognizer();
                prevTapGestureRecognizer.Tapped += PrevTapGestureRecognizer_Tapped; ;
                prev.GestureRecognizers.Add(prevTapGestureRecognizer);
                TapGestureRecognizer headTapGestureRecognizer = new TapGestureRecognizer();
                headTapGestureRecognizer.Tapped += HeadTapGestureRecognizer_Tapped; ;
                head.GestureRecognizers.Add(headTapGestureRecognizer);
            }

        }

        private void NextTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(item.next())
            Navigation.PushAsync(new ReasonLogChange(item, user, users));
            
            
        }
        private void PrevTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(item.prev())
            Navigation.PushAsync(new ReasonLogChange(item,user,users));
            
        }
        private void HeadTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdateTask(item,user,users));
            
        }
    }
}
