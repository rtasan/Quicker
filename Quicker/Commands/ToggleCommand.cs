using Microsoft.Win32;
using Quicker.Models;
using Quicker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsInput.Events;
using WindowsInput.Events.Sources;

namespace Quicker.Commands
{
    public class ToggleCommand:ICommand
    {
        public IKeyboardEventSource? m_Keyboard;
        private string KeyList = "";
        /// <summary>
        /// コマンドを読み出す側のクラス（View Model）を保持するプロパティ
        /// </summary>
        private MatchViewModel _view { get; set; }

        /// <summary>
        /// コンストラクタ
        /// コマンドで処理したいクラス(View Modeo)をここで受け取る
        /// </summary>
        /// <param name="view"></param>
        public ToggleCommand(MatchViewModel view)
        {
            _view = view;
        }

        /// <summary>
        /// コマンドのルールとして必ず実装しておくイベントハンドラ
        /// 通常、このメソッドを丸ごとコピーすればOK
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// コマンドの有効／無効を判定するメソッド
        /// コマンドのルールとして必ず実装しておくメソッド
        /// 有効／無効を制御する必要が無ければ、無条件にTrueを返しておく
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        //Command実行時の処理
        public void Execute(object? parameter)
        {
            if((bool)parameter)
            {
                Subscribe();
            }
            else
            {
                Unsubscribe();
            }
        }

        public void Subscribe()
        {
            //var Keyboard = default(IKeyboardEventSource);
            var Keyboard = WindowsInput.Capture.Global.Keyboard();

            Subscribe(Keyboard);
        }

        public void Unsubscribe()
        {
            //TODO: Unsubscribe
            this.m_Keyboard?.Dispose();
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

        private static Dictionary<string, string> KeyPair = new Dictionary<string, string>()
        {
            {"),","),"},{").","). "},{",",","},{".",". "},{")",")"}
        };

        private void Keyboard_KeyEvent(object? sender, EventSourceEventArgs<KeyboardEvent> e)
        {
            //System.Diagnostics.Debug.WriteLine(e.Data);
            //System.Diagnostics.Debug.WriteLine(e.Data.TextClick);

            var key = e.Data.TextClick?.Text;
            if (key == " ")
            {
                var result = new Match("", ""); //findした結果が代入される変数
                string value;
                bool isPlural = false;
                int AdditionalDelete = 0;
                if (KeyList.Contains('/'))
                {
                    if (KeyList.EndsWith("/s"))
                    {
                        isPlural = true;
                        KeyList = KeyList.Remove(KeyList.Length - 1);
                    }
                    KeyList = KeyList.Remove(KeyList.Length - 1);
                    AdditionalDelete = 1;
                }
                else if (KeyList.EndsWith("s"))
                {
                    isPlural = true;
                    KeyList = KeyList.Remove(KeyList.Length - 1);
                }
                if (KeyPair.TryGetValue(((KeyList.Length >= 2) ? KeyList.Substring(KeyList.Length - 2) : ""), out value) || KeyPair.TryGetValue(KeyList.Substring(KeyList.Length - 1), out value))
                {
                    KeyList = KeyList.Remove(KeyList.Length - value.Trim().Length);
                    if (_view.MatchList.FindMatch(KeyList, ref result))
                    {
                        result.Perform(ref this.m_Keyboard, value, isPlural, 0);
                    }
                }
                else if (KeyList.EndsWith("#"))
                {
                    KeyList = KeyList.Remove(KeyList.Length - 1);
                    result.OnlyNumber(ref this.m_Keyboard, null, KeyList);
                }
                else if (_view.MatchList.FindMatch(KeyList, ref result))
                {
                    result.Perform(ref this.m_Keyboard, null, isPlural, AdditionalDelete);
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

       

    }
}
