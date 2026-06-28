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
        private bool _isSelected;
        private bool _isKeySkill;

        public bool isMentorSkill;
        public bool IsKeySkill
        {
            get => _isKeySkill;
            set
            {
                    _isKeySkill = value;
                     NotifyPropertyChanged(nameof(IsKeySkill));
                    
                
            }
        }
     
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                   
                        NotifyPropertyChanged(nameof(SkillTF.IsSelected));
                   
                }
            }
        }
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
