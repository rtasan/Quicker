using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using WindowsInput;
using WindowsInput.Events.Sources;
using WindowsInput.Events;

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
        public void Perform(ref IKeyboardEventSource Keyboard)
        {
            var ToSend = WindowsInput.Simulate.Events();
            for (int i = 0; i < this.keyword.Length; i++)
            {
                ToSend.Click(WindowsInput.Events.KeyCode.Backspace);
            }

            ToSend.Click(snippet);

            using (Keyboard.Suspend())
            {
                ToSend.Invoke();
            }
        }
    }
}
