using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using static RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.CharScorePageModelTF;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class CharScorePageModelTF:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _pointBank = 9;

        
        public ICommand AddpointsToScore { get; }
        public ICommand RemovePointsFromScore { get; }
        
        public ICommand AddPointsToSkill { get; }

        public ICommand RemovePointsFromSkill { get; }

        public List <ScoreTF> Scores { get; }

        public ScoreTF Strength { get; }

        //Strength skills
       
        public class SkillCommandParameter
        {
            public ScoreTF score { get; }
            public SkillTF skill { get; }
        }
        public ScoreTF Speed { get; }
        public ScoreTF Smarts { get; }
        public ScoreTF Soical { get; }

        public CharScorePageModelTF()
        {
            AddpointsToScore = new RelayCommand<ScoreTF>(AddPointsToScore);
            RemovePointsFromScore = new RelayCommand<ScoreTF>(DecreasePontsFromScore);
            AddPointsToSkill = new RelayCommand<SkillCommandParameter>(param => AddPointsToSkil(param.score, param.skill));
            RemovePointsFromSkill = new RelayCommand<SkillCommandParameter>(param=>DecreasePointsFromSkill(param.score,param.skill));

            //Strength block
            Strength = new ScoreTF("Strength", new List<SkillTF>
            {
                new SkillTF("Athletics"),
                new SkillTF("Brawn"),
                new SkillTF("Conditioning"),
                new SkillTF("Intimidation"),
                new SkillTF("Might"),
            });

            Speed = new ScoreTF("Speed", new List<SkillTF>
            {
                new SkillTF("Acrobatics"),
                new SkillTF("Driving"),
                new SkillTF("Finesse"),
                new SkillTF("Inflitration"),
                new SkillTF("Inititave"),
                new SkillTF("Targeting"),



            });

            Smarts = new ScoreTF("Smarts", new List<SkillTF>
            {
                new SkillTF("Alertness"),
                new SkillTF("Culture"),
                new SkillTF("Science"),
                new SkillTF("Survival"),
                new SkillTF("Technology"),
                new SkillTF("Targeting"),

             });

            Soical = new ScoreTF("Soical", new List<SkillTF>
            {
                new SkillTF("Animal Handling"),
                new SkillTF("Deception"),
                new SkillTF("Preformance"),
                new SkillTF("Preformance"),
                new SkillTF("Persuasion"),
                new SkillTF("Streetwise"),

             });
            Scores = new List<ScoreTF>
            {
            Strength,
            Speed,
            Smarts,
            Soical
            };



        }





        public int PointsBank
        {
            get => _pointBank;
            set
            {
                _pointBank = value;
                NotifyPropertyChanged(nameof(PointsBank));
            }
        }


        public void AddPointsToSkil(ScoreTF scoreTF,SkillTF skillTF)
        {
            if (!scoreTF.CanIncreaseSkill())
            {
                MessageBox.Show($"Total rank of skills in group {scoreTF} can not be greater than scores current value");
            }
            else
            {
                scoreTF.IncreaseSkill(skillTF);
            }
        }
        public void DecreasePointsFromSkill(ScoreTF scoretf,SkillTF skill)
        {
            if (skill.SkillScore <= 0)
            {
                MessageBox.Show("can not lower score past 0. ");
            }
            else
            {
                scoretf.DecreaseSkill(skill);
            }
        }

        public void AddPointsToScore(ScoreTF targetScore)
        {
            if (PointsBank > 0)
            {
                targetScore.AddToScore();
                PointsBank -= 1;
            }else
            {
                MessageBox.Show("You have no more points to allocate");
            }
        }
        public void DecreasePontsFromScore(ScoreTF targetScore)
        {
            if (targetScore.CurrentRank < 1)
            {
                MessageBox.Show("You can not decrese this score Lower than one");

            }
            else
            {
                targetScore.SubtractFromScore();
                PointsBank += 1;
            }
        }
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
