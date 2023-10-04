﻿using System;
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
        private IKeyboardEventSource m_Keyboard;
        public MainWindow()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("Im not stopped");
            Subscribe();
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
            Log(e);
        }

        private void Log<T>(EventSourceEventArgs<T> e, string Notes = "") where T : InputEvent
        {
            var NewContent = "";
            NewContent += $"{e.Timestamp}: {Notes}\r\n";
            foreach (var item in e.Data.Events)
            {
                NewContent += $"  {item}\r\n";
            }
            System.Diagnostics.Debug.WriteLine(NewContent);

        }

        private void Subscribe()
        {
            var Keyboard = default(IKeyboardEventSource);
            Keyboard = WindowsInput.Capture.Global.Keyboard();

            Subscribe(Keyboard);
        }

    }
    
    
}
