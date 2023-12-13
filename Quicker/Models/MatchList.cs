using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Humanizer;

namespace Quicker.Models
{
    public class MatchList:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ObservableCollection<Match> matchesList;
        public ObservableCollection<Match> MatchesList
        {
            get
            {
                return matchesList;
            }
            set
            {
                matchesList = value;
                NotifyPropertyChanged();
            }
        }

        public MatchList()
        {
            MatchesList = new ObservableCollection<Match>();
            string __= "".Pluralize(); //Humanizerの初回呼び出しが遅れるので、一度呼び出しておく
        }

        public void AddMatch(Match match)
        {
            this.MatchesList.Add(match);
        }

        public bool FindMatch(string key, ref Match result)
        {
            result=matchesList.FirstOrDefault(x => x.keyword == key);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
