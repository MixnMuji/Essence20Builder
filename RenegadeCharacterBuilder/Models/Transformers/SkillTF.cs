using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class SkillTF: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string name { get; set; }


        private int _skillScore { get; set; } = 0;

        public SkillTF(string name)
        {
            this.name = name;
        }
        public int SkillScore{
            get => _skillScore;
            set
            {
                _skillScore = value;
                NotifyPropertyChanged(nameof(SkillScore));

            }
        }
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
       

        
    }
}
