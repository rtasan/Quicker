using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.Models
{
    internal class MatchList
    {
        private Dictionary<string, Match> Matches;
        public MatchList()
        {
            Matches = new Dictionary<string, Match>();
        }
        public MatchList(string path)
        {
            Matches = new Dictionary<string, Match>();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var keyword = values[0].Trim();
                    var snippet = values[1].Trim();
                    Match match = new Match(keyword, snippet);
                    append(values[0].Trim(), match);
                }
            }
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
