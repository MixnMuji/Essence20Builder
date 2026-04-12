using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class ScoreTF : INotifyPropertyChanged
    {
        private string name { get; init; }
        private int currentRank {get; set;} = 1;

        private List<SkillTF> correspondingSkills = new();

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

        public ScoreTF(string name, List<SkillTF> correspondingSkills)
        {
            this.name = name;
            this.correspondingSkills = correspondingSkills;
        }
        public bool CanIncreaseSkill()
        {
            int total = correspondingSkills.Sum(s => s.SkillScore);
            return total < CurrentRank;
        }
        public void IncreaseSkill(SkillTF skill)
        {
            if (!CanIncreaseSkill())
            {
                throw new InvalidOperationException("Skill allocation exceeds essance score");
            }
            skill.SkillScore += 1;
        }
        public void DecreaseSkill(SkillTF skill)
        {
            if(skill.SkillScore <= 0){
                throw new InvalidOperationException("Skill Scores can not be less than zero");
            }
            skill.SkillScore -= 1;
        }
        public void AddToScore()
        {
            
            CurrentRank += 1;
        }
        public void SubtractFromScore()
        {
            if (CurrentRank <=1)
            {
                throw new InvalidOperationException("Scores can not be have a lower value than 1");
            }
            CurrentRank -= 1;
           
        }
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
       
    }
}
