using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    internal class ScoreTF: INotifyPropertyChanged
    {
        private string name { get; init; }
        private int currentRank;

        public event PropertyChangedEventHandler PropertyChanged;
        public int CurrentRank
        {
            get => currentRank;
            set
            {
                if (currentRank != value)
                {
                    currentRank = value;
                    NotifyPropertyChanged(nameof(CurrentRank));
                }
            }
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
