using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Taskify.Item
{
    class Item : INotifyPropertyChanged,IComparable
    {
        private string titleValue = String.Empty;
        private string descValue = String.Empty;
        public static string[] states = { "Pendiente" , "En Proceso", "Terminada"};
        private List<Tuple<DateTime,string>> log;
        private DateTime expiration;
        public bool expirated;

        public DateTime GetExpDateTime()
        {
            return this.expiration;
        }
        
        public void setExpDate(DateTime exp)
        {
            this.expiration = exp;
            NotifyPropertyChanged();
        }

        public string GetExpDate()
        {
            return ""+expiration.Day+" / "+expiration.Month+" / "+expiration.Year;
        }

        private int stateValue;

        public void addLogLine(string reason)
        {
            if (reason == "")
            {
                log.Add(new Tuple<DateTime, string>(DateTime.Now," <NoReason> "));
            }
            else
            {
                log.Add(new Tuple<DateTime, string>(DateTime.Now, reason));
            }
            NotifyPropertyChanged();
        }

        public List<Tuple<DateTime, string>> getLogList()
        {
           
             return log.OrderByDescending(x => x.Item1).ToList();
        }

        public string title
        {
            get
            {
                return this.titleValue;

            }
            set
            {
                if (value != this.titleValue)
                {
                    this.titleValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string state
        {
            get
            {
                if (stateValue < states.Length)
                {
                    return states[this.stateValue];
                }
                else
                {
                    stateValue = states.Length-1;
                    return states[stateValue];
                }

            }
            set
            {
                int index=0;
                int i = 2;
                while (i > -1)
                {
                    if (value.Equals(states[i]))
                        index = i;
                    i--;
                }
                this.stateValue = index;
                NotifyPropertyChanged();
            }
        }

        public string desc
        {
            get
            {
                return this.descValue; 
                
            }
            set
            {
                if (value != this.descValue)
                {
                    this.descValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Item(string aTitle, DateTime exp)
        {
            this.title = aTitle;
            this.titleValue = aTitle;
            this.state = "Pendiente";
            this.stateValue = 0;
            this.expiration = exp;
            this.log = new List<Tuple<DateTime, string>>();
            log.Add(new Tuple<DateTime, string>(DateTime.Now, "Created"));
            expirated = exp < DateTime.Now;
        }

        public Item(string aTitle)
        {
            this.title = aTitle;
            this.titleValue = aTitle;
            this.state = "Pendiente";
            this.stateValue = 0;
            this.expiration = DateTime.Now;
            this.log = new List<Tuple<DateTime, string>>();
            log.Add(new Tuple<DateTime, string>(DateTime.Now, "Created"));
            expirated = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged([CallerMemberName] string name="")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            checkExpiration();
        }

        private void checkExpiration()
        {
            expirated = expiration < DateTime.Now;
        }

        public bool next()
        {
            if (this.stateValue < states.Length - 1)
            {
                this.stateValue++;
                state = states[stateValue];
                return true;
            }
            else
            {
                this.stateValue = 0;
                state = states[0];
                return false;
            }
        }
        public bool prev()
        {
            if (this.stateValue > 0)
            {
                this.stateValue--;
                state = states[stateValue];
                return true;
            }
            else
            {
                this.stateValue = 0;
                state = states[0];
                return false;
            }
        }


        public int CompareTo(object obj)
        {
            Item i = (Item) obj;
            int thisState = this.stateValue;
            int toState = i.stateValue;
            DateTime lastUpd = log[log.Count - 1].Item1;
            DateTime toLastUpd = i.log[i.log.Count - 1].Item1;

            if (this.expiration.Date.CompareTo(i.expiration.Date) == 0)
            {
                if (thisState - toState != 0)
                {
                    return thisState - toState;
                }
                else
                {
                    return lastUpd.CompareTo(toLastUpd);

                }
            }
            else
            {
                return this.expiration.Date.CompareTo(i.expiration.Date);
            }
        }
        
        public bool Equals(Item i)
        {
            return i.title.Equals(this.title);
        }

    }
}
