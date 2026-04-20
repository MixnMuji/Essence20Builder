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
        public string Name { get; }

        private int _skillScore { get; set; } = 0;
        public bool IsKeySkill { get; set; } = false;
        public SkillTF(string name)
        {
            Name = name;
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
