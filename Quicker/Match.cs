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
        private string? keyword { get; set; }
        private string? snippet { get; set; }
        public Match(string keyword, string snippet)
        {
            this.keyword = keyword;
            this.snippet = snippet;
        }
        public Match()
        {
            this.keyword = null;
            this.snippet = null;
        }
        public bool isMatch(ref string input)
        {
            return input==keyword;
        }
        public void Perform(IKeyboardEventSource Keyboard)
        {
            //TODO: 置き換えメソッドをインプリメント
            //var ToSend=WindowsInput.Simulate.Events();
            //for (int i=1; i<keyword.Length; i++)
            //{
            //    ToSend = ToSend.Click(WindowsInput.Events.KeyCode.Backspace);
            //}
            //ToSend.Click(snippet);
            //using (Keyboard.Suspend())
            //{
            //    ToSend.Invoke().Wait();
            //}
            var kc = new WindowsInput.Events.KeyClick(KeyCode.A);
            IEnumerable<IEvent> events = new List<IEvent>();
            events.Append(kc);
            var Options = new InvokeOptions();
            var sim = Simulate.Events( Options,events);
            sim.Wait();
            System.Diagnostics.Debug.WriteLine("Performed!!");
        }
    }
}
