using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Events.Sources;
using WindowsInput.Events;
using Quicker.Models;
using System.ComponentModel;
using Quicker.Commands;
using System.Windows.Interop;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

namespace Quicker.ViewModels
{
    public class MatchViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// View Modelのルールとして実装しておくイベントハンドラ
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //コマンドを格納するプロパティ
        public BrowserMatchCommand BrowserCommand { get; private set; }
        public ToggleCommand ToggleCommand { get; private set; }

        //Modelのインスタンスを保持するプロパティ
        public MatchList MatchList { get; set; }
        public CsvFile CsvFile { get; set;}

        public MatchViewModel()
        {
            BrowserCommand = new BrowserMatchCommand(this);
            ToggleCommand = new ToggleCommand(this);
            MatchList = new MatchList();
            CsvFile = new CsvFile();
        }

        /// <summary>
        /// 他のクラスから参照／設定が行えるようにするためのプロパティの定義
        /// View のXAMLに記述したバインドにより、View Modelからアクセスされる
        /// </summary>

        public string CsvFilePath
        {
            get
            {
                //カウンタークラスが保持するカウント値を返す
                return CsvFile.CsvPath;
            }
            set
            {
                //カウンタークラスが保持するカウント値を設定する
                CsvFile.SetCsvPath(value);
                //中身が変更されたことを View Modelに通知するためのイベントハンドラ呼び出し
                //引数として、プロパティ名を文字列として渡すことでVIEWからバインドされる
                NotifyPropertyChanged();
            }
        }
    }
}
