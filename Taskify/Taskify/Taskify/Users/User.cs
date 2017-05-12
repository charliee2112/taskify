using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Taskify.Users
{
    class User
    {
        public string name { get; set; }
        private int pin;
        public List<User> outView;
        public List<User> inView;
        public List<User> requestedIn;
        public List<User> requestedOut;
        public ObservableCollection<Item.Item> tasks;

        public User(string aName, int aPin)
        {
            this.name = aName;
            this.pin = aPin;
            tasks = new ObservableCollection<Item.Item>();
            inView = new List<User>();
            outView = new List<User>();
            requestedIn = new List<User>();
            requestedOut = new List<User>();

        }

        public string getPin()
        {
            return this.pin+"";
        }

        public ObservableCollection<Item.Item> getTasks()
        {
            return this.tasks;
        }

        public void sendRequest(User requestedUser)
        {
            requestedUser.requestedIn.Add(this);
            this.requestedOut.Add(requestedUser);
        }
        
        public void acceptRequest(User requestingUser)
        {
            this.requestedIn.Remove(requestingUser);
            this.inView.Add(requestingUser);
            requestingUser.requestedOut.Remove(this);
            requestingUser.outView.Add(this);

        }

        public void denyRequest(User requestingUser)
        {
            this.requestedIn.Remove(requestingUser);
            requestingUser.requestedOut.Remove(this);

        }

        public void revokeUser(User related)
        {
            this.inView.Remove(related);
            related.outView.Remove(this);

        }

        public void deleteRequest(User related)
        {
            this.requestedIn.Remove(related);
            related.requestedOut.Remove(this);
        }

        public Item.Item getTask(string name)
        {
            Item.Item retorno = new Item.Item(name);
            foreach(Item.Item i in tasks)
            {
                if (i.title==name)
                {
                    retorno = i;
                }
            }

            return retorno;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            User p = obj as User;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (name == p.name);
        }

        public bool Equals(User p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (name == p.name);
        }
    }
}
