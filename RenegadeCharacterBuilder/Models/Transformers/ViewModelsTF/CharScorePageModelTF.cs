using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using static RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.CharScorePageModelTF;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class CharScorePageModelTF:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _pointBank = 9;

        public string CharacterRoleForKeyScores { get; set; }
        public ICommand AddpointsToScore { get; }
        public ICommand RemovePointsFromScore { get; }
        
        public ICommand AddPointsToSkill { get; }

        public ICommand RemovePointsFromSkill { get; }

        public List <ScoreTF> Scores { get; }

        public ScoreTF Strength { get; }

        

      public ScoreTF Score { get; }
        public ScoreTF Speed { get; }
        public ScoreTF Smarts { get; }
        public ScoreTF Soical { get; }

        public CharScorePageModelTF()
        {
            AddpointsToScore = new RelayCommand<ScoreTF>(AddPointsToScore);
            RemovePointsFromScore = new RelayCommand<ScoreTF>(DecreasePontsFromScore);
            AddPointsToSkill = new RelayCommand<SkillTF>(AddPointsToSkil);
            RemovePointsFromSkill = new RelayCommand<SkillTF>(DecreasePointsFromSkill);
            CharacterRoleForKeyScores = TFCharacterSession.CurrentTransfomer.Role.Name;

            switch (CharacterRoleForKeyScores)
            {
                case "Analyst":
                    defineScoresAndSkillChoice(Speed, Smarts,{Alertness, finesse,infiltration, iniative, science, Technology})
                break;
            }
            // define skills with name and a get and set them equal like Athletics = new Skilltf thletics also switch must come later after everything is defined or it will be null
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



        public string defineScoresAndSkillChoice( ScoreTF IncreaseOne, ScoreTF IncreaseTwo, IEnumerable<SkillTF> Choices )
        {
            IncreaseOne.CurrentRank += 1;
            IncreaseOne.IsKeyScore = true;
            IncreaseTwo.CurrentRank += 1;
            IncreaseTwo.IsKeyScore = true;

            var skillList = new StringBuilder();
            int i = 1;

            foreach (var skill in Choices)
            {
                skillList.AppendLine($"{i}) {skill.Name}");
                i++;
            }
            return skillList.ToString();
            //take role scratch that it's in the switch case
            //add to scores and turn Keyscore into true
            // Create a list of List TF with options
            //pick a skill for loop listing options
            //after you pick remove the option from the list and pick again
            //changes boolian to SkillTF to true!

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


        public void AddPointsToSkil(SkillTF skill)
        {
            var score = Scores.First(s => s.CorrespondingSkills.Contains(skill));
            if (!score.TryIncreaseSKill(skill))
            {
                MessageBox.Show($"points allocated to skills can not exceed value of Essance score current value {score.Name}");
            }
            else
            {
                score.TryIncreaseSKill(skill);
            }

        }
        public void DecreasePointsFromSkill(SkillTF skill)
        {
            var score = Scores.First(s => s.CorrespondingSkills.Contains(skill));
            if (skill.SkillScore <= 0)
            {
                MessageBox.Show("can not lower score past 0. ");
            }
            else
            {
                score.DecreaseSkill(skill);
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
            if (targetScore.CurrentRank <=1 || targetScore.IsKeyScore == true && targetScore.CurrentRank<=2)
            {
                MessageBox.Show("You can not decrese this score Lower than one, or lower your key score");

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
