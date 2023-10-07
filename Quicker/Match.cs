using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using WindowsInput;
using WindowsInput.Events.Sources;
using WindowsInput.Events;
using Humanizer;

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
        public void Perform(ref IKeyboardEventSource Keyboard, string? OptionalText, bool isPlural)
        {
            var ToSend = WindowsInput.Simulate.Events();
            System.Diagnostics.Debug.WriteLine(this.keyword.Length + (String.IsNullOrEmpty(OptionalText)?0:OptionalText.Length));

            for (int i = 0; i < this.keyword.Length + (String.IsNullOrEmpty(OptionalText) ? 0 : OptionalText.Trim().Length)+(isPlural?1:0); i++)
            {
                ToSend.Click(WindowsInput.Events.KeyCode.Backspace);
            }

            ToSend.Click((isPlural?snippet.Pluralize():snippet)+" "+keyword+OptionalText);

            using (Keyboard.Suspend())
            {
                ToSend.Invoke();
            }
        }
    }
}
