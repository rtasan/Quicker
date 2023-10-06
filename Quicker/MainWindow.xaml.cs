using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsInput;
using WindowsInput.Events.Sources;
using WindowsInput.Events;
using Quicker;

namespace Quicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardEventSource m_Keyboard;
        private string KeyList = "";
        private MatchList m_list;

        public MainWindow()
        {
            InitializeComponent();
            //Create a instance of MatchList for the test
            
            Subscribe();
            Match test1 = new Match("11", "engine 11");
            this.m_list= new MatchList();
            m_list.append("11", test1);
        }

        private void Subscribe(IKeyboardEventSource Keyboard)
        {
            this.m_Keyboard?.Dispose();
            this.m_Keyboard = Keyboard;

            if (Keyboard != default)
            {
                Keyboard.KeyEvent += this.Keyboard_KeyEvent;
            }
        }
        List<string> match_list = new List<string> { "11" ,"12"};
        private void Keyboard_KeyEvent(object sender, EventSourceEventArgs<KeyboardEvent> e)
        {
            //System.Diagnostics.Debug.WriteLine(e.Data);

            var key = e.Data.TextClick?.Text;
            if (key==" ")
            {
                //TODO: Dictをキーで検索 ヒットしたマッチインスタンスの置き換えメソッドを実行
                var result = new Match(); //1findした結果が代入される変数

                if (m_list.FindMatch(KeyList, ref result))
                {
                    //System.Diagnostics.Debug.WriteLine("Matched!!");
                    
                    result.Perform(this.m_Keyboard);
                    //Thread.Sleep(1000);
                }
                KeyList = "";                
            }
            else
            {
                KeyList += key;
            }
            //System.Diagnostics.Debug.WriteLine(KeyList);
            //Log(e);
        }

        private void Log<T>(EventSourceEventArgs<T> e, string Notes = "") where T : InputEvent
        {
            var NewContent = "";
            NewContent += $"{e.Timestamp}: {Notes}\r\n";
            foreach (var item in e.Data.Events)
            {
                NewContent += $"  {item}\r\n";
            }
            //System.Diagnostics.Debug.WriteLine(NewContent);

        }

        private void Subscribe()
        {
            var Keyboard = default(IKeyboardEventSource);
            Keyboard = WindowsInput.Capture.Global.Keyboard();

            Subscribe(Keyboard);
        }

    }
    
    
}
