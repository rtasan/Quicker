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

namespace Quicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task.Run(() => SetKey());
            System.Diagnostics.Debug.WriteLine("Im not stopped");    
        }

        public void SetKey()
        {
            using (var Keyboard = WindowsInput.Capture.Global.KeyboardAsync())
            {

                var Listener = new WindowsInput.Events.Sources.TextSequenceEventSource(Keyboard, new WindowsInput.Events.TextClick("aaa"));
                Listener.Triggered += (x, y) => Listener_Triggered(Keyboard, x, y); ;
                Listener.Enabled = true;
                while (true)
                {

                }
            }
            

        }

        

        private static async void Listener_Triggered(IKeyboardEventSource Keyboard, object sender, WindowsInput.Events.Sources.TextSequenceEventArgs e)
        {
            e.Input.Next_Hook_Enabled = false;

            var ToSend = WindowsInput.Simulate.Events();
            for (int i = 1; i < e.Sequence.Text.Length; i++)
            {
                ToSend.Click(WindowsInput.Events.KeyCode.Backspace);
            }

            ToSend.Click("Always ask albert!");

            //We suspend keyboard events because we don't want to accidently trigger a recursive loop if our
            //sending text actually had 'aaa' in it.
            using (Keyboard.Suspend())
            {
                await ToSend.Invoke();
            }
        }
    }
    
    
}
