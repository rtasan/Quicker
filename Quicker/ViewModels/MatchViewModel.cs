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

namespace Quicker.ViewModels
{
    internal class MatchViewModel : INotifyPropertyChanged
    {
        private MatchList m_list;

        /// <summary>
        /// View Modelのルールとして実装しておくイベントハンドラ
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        //コマンドを格納するプロパティ
        public EnableMatchCommand EnableMatchCommand { get; private set; }
        public DisableMatchCommand DisableMatchCommand { get; private set; }

        //Modelのインスタンスを保持するプロパティ
        public Keyboard Keyboard { get; set; }
        //Todo:MatchListも多分ここ

        public MatchViewModel()
        {
            EnableMatchCommand = new EnableMatchCommand(this);
            DisableMatchCommand = new DisableMatchCommand(this);
            this.m_list = new MatchList("C:\\Users\\Reiko\\Desktop\\test.csv");
            Keyboard = new Keyboard(this.m_list);
        }


        
    }
}
