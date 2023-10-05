using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker
{
    internal class Match
    {
        private string keyword { get; set; }
        private string snippet { get; set; }
        public Match(string keyword, string snippet)
        {
            this.keyword = keyword;
            this.snippet = snippet;
        }
        public bool isMatch(ref string input)
        {
            return input==keyword;
        }
        public void Perform() { 
        //TODO: 置き換えメソッドをインプリメント
        }
    }
}
