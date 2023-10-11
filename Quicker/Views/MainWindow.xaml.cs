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
using System.IO;
using Quicker.Models;

namespace Quicker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public IKeyboardEventSource m_Keyboard;
        //private string KeyList = "";
        //private MatchList m_list;

        public MainWindow()
        {
            InitializeComponent();
            //Subscribe();
            //this.m_list = new MatchList("C:\\Users\\Reiko\\Desktop\\test.csv");
        }
        /*
        private void Subscribe(IKeyboardEventSource Keyboard)
        {
            this.m_Keyboard?.Dispose();
            this.m_Keyboard = Keyboard;

            if (Keyboard != default)
            {
                Keyboard.KeyEvent += this.Keyboard_KeyEvent;
            }
        }
        private static Dictionary<string, string> KeyPair = new Dictionary<string, string>()
        {
            {"),","),"},{").","). "},{",",","},{".",". "},{")",")"}
        };
        private void Keyboard_KeyEvent(object sender, EventSourceEventArgs<KeyboardEvent> e)
        {
            //System.Diagnostics.Debug.WriteLine(e.Data);
            //System.Diagnostics.Debug.WriteLine(e.Data.TextClick);

            var key = e.Data.TextClick?.Text;
            if (key == " ")
            {
                var result = new Match("", ""); //findした結果が代入される変数
                string value; 
                bool isPlural=false;
                if (KeyList.EndsWith("s"))
                {
                    isPlural= true;
                    KeyList = KeyList.Remove(KeyList.Length - 1);
                }
                if(KeyPair.TryGetValue(((KeyList.Length>=2)?KeyList.Substring(KeyList.Length - 2):""), out value)|| KeyPair.TryGetValue(KeyList.Substring(KeyList.Length - 1), out value))
                {
                    KeyList = KeyList.Remove(KeyList.Length - value.Trim().Length);
                    if (m_list.FindMatch(KeyList, ref result))
                    {
                        result.Perform(ref this.m_Keyboard, value, isPlural);
                    }
                }else if (KeyList.EndsWith("#"))
                {
                    KeyList = KeyList.Remove(KeyList.Length - 1);
                    result.OnlyNumber(ref this.m_Keyboard, null, KeyList);
                }
                else if(m_list.FindMatch(KeyList, ref result))
                {
                    result.Perform(ref this.m_Keyboard, null, isPlural);
                }                              
                KeyList = "";
            }
            else if (e.Data.KeyDown?.Key == KeyCode.Backspace)
            {
                if (KeyList.Length > 0)
                {
                    KeyList = KeyList.Remove(KeyList.Length - 1);
                }
            }
            else
            {
                KeyList += key;
            }
        }

        private void Subscribe()
        {
            //var Keyboard = default(IKeyboardEventSource);
            var Keyboard = WindowsInput.Capture.Global.Keyboard();

            Subscribe(Keyboard);
        }
        */

    }
    
    
}
