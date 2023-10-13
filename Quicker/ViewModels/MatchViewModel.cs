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
using System.Windows;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace Quicker.ViewModels
{
    public class MatchViewModel : INotifyPropertyChanged, IClosing
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
        public AddNewMatchCommand AddNewMatchCommand { get; private set; }

        //Modelのインスタンスを保持するプロパティ
        public MatchList MatchList { get; set; }
        public CsvFile CsvFile { get; set; }

        public MatchViewModel()
        {
            BrowserCommand = new BrowserMatchCommand(this);
            ToggleCommand = new ToggleCommand(this);
            AddNewMatchCommand = new AddNewMatchCommand(this);
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
        public bool OnClosing()
        {
            bool close = true;

            //Ask whether to save changes och cancel etc
            //close = false; //If you want to cancel close
            string messageBoxText = "Do you want to save changes before closing?";
            string caption = "Quicker";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if(result == MessageBoxResult.Yes)
            {
                if (!CsvFile.WriteCsv(this.MatchList))
                {
                    MessageBox.Show("Unable to save CSV file, check the path and try again.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    close = false;
                }
                else
                {
                    close = true;
                }
            }
            else if(result == MessageBoxResult.No)
            {
                close = true;
            }
            else
            {
                close = false;
            }


            return close;
        }

    }
    public interface IClosing
    {
        /// <summary>
        /// Executes when window is closing
        /// </summary>
        /// <returns>Whether the windows should be closed by the caller</returns>
        bool OnClosing();
    }
}
