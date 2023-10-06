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
        public IKeyboardEventSource m_Keyboard;
        private string KeyList = "";
        private MatchList m_list;

        public MainWindow()
        {
            InitializeComponent();           
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
        private void Keyboard_KeyEvent(object sender, EventSourceEventArgs<KeyboardEvent> e)
        {
            System.Diagnostics.Debug.WriteLine(e.Data);

            var key = e.Data.TextClick?.Text;
            if (key == " ")
            {
                var result = new Match("", ""); //findした結果が代入される変数

                if (m_list.FindMatch(KeyList, ref result))
                {
                    result.Perform(ref this.m_Keyboard);
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
            //System.Diagnostics.Debug.WriteLine(KeyList);
        }

        private void Subscribe()
        {
            //var Keyboard = default(IKeyboardEventSource);
            var Keyboard = WindowsInput.Capture.Global.Keyboard();

            Subscribe(Keyboard);
        }

    }
    
    
}
