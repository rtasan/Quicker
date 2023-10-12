using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        private Dictionary<string, Match> _matches;
        public Dictionary<string, Match> Matches
        {
            get
            {
                return _matches;
            }
            set
            {
                _matches = value;
                NotifyPropertyChanged();
            }
        }

        public MatchList()
        {
            Matches = new Dictionary<string, Match>();
            Matches.TryAdd("105", new Match("105", "test"));
        }
        //public bool FromCsv(CsvFile csvFile)
        //{
        //    Matches = csvFile.ReadCsv(); //Todo: 読み込みに失敗したときの処理でfalseを返す
        //    //NotifyPropertyChanged();
        //    return true;
        //}
        public bool FindMatch(string key, ref Match result)
        {
            return _matches.TryGetValue(key, out result);
        }
    }
}
