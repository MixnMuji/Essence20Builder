using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Diagnostics;
using System.Windows.Input;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class ScoreTF : INotifyPropertyChanged
    {
        private string name { get; init; }
        private int currentRank {get; set;} = 1;
        public string Name { get; }

        public bool IsKeyScore { get; set; } = false;
        private List<SkillTF> correspondingSkills { get; set; }

        public SkillTF LinkedbyFocus { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
      
        public ScoreTF(string name, List<SkillTF> CorrespondingSkills)
        {
            Name = name;
           correspondingSkills = CorrespondingSkills;


            //IncreaseSkllCommand = new RelayCommand<SkillTF>(TryIncreaseSKill);
           // DecreaseSkllCommand = new RelayCommand<SkillTF>(DecreaseSkill);
        }



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
        public List<SkillTF> CorrespondingSkills
        {
            get => correspondingSkills;
            set => correspondingSkills = value;
        }public bool CanIncreaseSkill()
        {
            int total = correspondingSkills.Sum(s => s.SkillScore);
            return total < CurrentRank;
        }

        public bool TryIncreaseSKill(SkillTF skill)
        {
            if (!CanIncreaseSkill())
            {
                return false;
            }
            else
            {
                skill.SkillScore += 1;
                LinkedbyFocus.SkillScore += 1;
                return true;
            }

           
        }
       
        public bool DecreaseSkill(SkillTF skill)
        {
            if(skill.SkillScore <= 0 || skill.IsKeySkill== true && skill.SkillScore <=1)
            {
                return false;
            }
            else { 
            skill.SkillScore -= 1;
            LinkedbyFocus.SkillScore -= 1;
                return true;

            }
            
        }
        public void AddToScore()
        {
            
            CurrentRank += 1;
        }
        public void SubtractFromScore()
        {
                CurrentRank -= 1;
          
        }
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
       
    }
}
