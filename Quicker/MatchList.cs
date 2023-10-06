using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker
{
    internal class MatchList
    {
        private Dictionary<string ,Match> Matches;
        public MatchList()
        {
            this.Matches = new Dictionary<string, Match>();
        }
        public bool append(string key, Match match){
            Matches.TryAdd(key, match);
            return true;
        }
        public bool FindMatch(string key, ref Match result)
        {
            return Matches.TryGetValue(key, out result);
        }
        
        
    }
}
