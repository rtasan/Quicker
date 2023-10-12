using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private List<Match> matchesList;
        public List<Match> MatchesList
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
            MatchesList = new List<Match>();
            string __= "".Pluralize(); //Humanizerの初回呼び出しが遅れるので、一度呼び出しておく
        }

        public bool FindMatch(string key, ref Match result)
        {
            result = matchesList.Find(x => x.keyword == key);
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
