using Microsoft.Win32;
using Quicker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Quicker.Models;

namespace Quicker.Commands
{
    public class AddNewMatchCommand:ICommand
    {
        /// <summary>
        /// コマンドを読み出す側のクラス（View Model）を保持するプロパティ
        /// </summary>
        private MatchViewModel _view { get; set; }

        /// <summary>
        /// コンストラクタ
        /// コマンドで処理したいクラス(View Modeo)をここで受け取る
        /// </summary>
        /// <param name="view"></param>
        public AddNewMatchCommand(MatchViewModel view)
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
            var EmptyMatch=new Match("Key","Value");
            _view.MatchList.AddMatch(EmptyMatch);
        }
    }
}
