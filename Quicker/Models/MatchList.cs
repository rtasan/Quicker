using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.Models
{
    public class MatchList
    {
        private Dictionary<string, Match> Matches;

        public MatchList()
        {
            Matches = new Dictionary<string, Match>();
        }
        public bool FromCsv(CsvFile csvFile)
        {
            Matches = csvFile.ReadCsv(); //Todo: 読み込みに失敗したときの処理でfalseを返す
            return true;
        }
        public bool append(string key, Match match)
        {
            return Matches.TryAdd(key, match);
        }
        public bool FindMatch(string key, ref Match result)
        {
            return Matches.TryGetValue(key, out result);
        }
    }
}
