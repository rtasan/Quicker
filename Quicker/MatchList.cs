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
            
        }
        public bool append(string key, Match match){
            Matches.TryAdd(key, match);
            return true;
        }
        
    }
}
