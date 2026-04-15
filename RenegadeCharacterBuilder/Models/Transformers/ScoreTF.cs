using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class ScoreTF : INotifyPropertyChanged
    {
        private string name { get; init; }
        private int currentRank {get; set;} = 1;
        public string Name { get; }
        private List<SkillTF> correspondingSkills { get; set; }

        public ICommand IncreaseSkllCommand { get; }
        public ICommand DecreaseSkllCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
      
        public ScoreTF(string name, List<SkillTF> CorrespondingSkills)
        {
            Name = name;
           correspondingSkills = CorrespondingSkills;


            IncreaseSkllCommand = new RelayCommand<SkillTF>(IncreaseSkill);
            DecreaseSkllCommand = new RelayCommand<SkillTF>(DecreaseSkill);
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
