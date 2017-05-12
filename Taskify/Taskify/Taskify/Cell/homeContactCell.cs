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
    class HomeContactCell : StackLayout
    {
        private StackLayout head;
        private Label titleLabel;
        private User item;
        private List<User> list;

        public HomeContactCell(User unItem, List<User> aLlist)
        {
            list = aLlist;
            this.item = unItem;
            this.Orientation = StackOrientation.Horizontal;
            this.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.VerticalOptions = LayoutOptions.Start;

            titleLabel = new Label();
            titleLabel.FontSize = 20;
            titleLabel.Text = unItem.name;
           
            head = new StackLayout();
            head.Orientation = StackOrientation.Vertical;
            head.HorizontalOptions = LayoutOptions.FillAndExpand;
            head.VerticalOptions = LayoutOptions.Start;
           
            head.Children.Add(titleLabel);
            this.Children.Add(head);
            
        }
        
    }
}
