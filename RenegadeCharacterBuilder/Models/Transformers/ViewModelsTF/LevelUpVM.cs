using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class LevelUpVM
    {
        
    
            public event PropertyChangedEventHandler PropertyChanged;
            private int _skillsPointBank = 0;

            public List<SkillTF> SkillsToBoost { get; }

           

            public int CurrentLevel { get; } // will be the limit for our current level
            public ICommand AddpointsToScore { get; }
            public ICommand RemovePointsFromScore { get; }

            public ICommand AddPointsToSkill { get; }

            public ICommand RemovePointsFromSkill { get; }

            public ICommand GetRoleForAllocations { get; }


            public List<ScoreTF> Scores { get; }

            public ScoreTF Strength { get; }

            public ScoreTF Speed { get; }
            public ScoreTF Smarts { get; }
            public ScoreTF Soical { get; }


            public SkillTF Athletics { get; }
            public SkillTF Brawn { get; }
            public SkillTF Conditioning { get; }
            public SkillTF Intimidation { get; }
            public SkillTF Might { get; }


            //Speed gets
            public SkillTF Acrobatics { get; }
            public SkillTF Driving { get; }
            public SkillTF Finesse { get; }
            public SkillTF Inflitration { get; }
            public SkillTF Inititave { get; }
            public SkillTF Targeting { get; }

            //smarts gets

            public SkillTF Alertness { get; }
            public SkillTF Culture { get; }
            public SkillTF Science { get; }
            public SkillTF Survival { get; }
            public SkillTF Technology { get; }

            public SkillTF AnimalHandling { get; }
            public SkillTF Deception { get; }
            public SkillTF Preformance { get; }
            public SkillTF Persuasion { get; }
            public SkillTF Streetwise { get; }





            public LevelUpVM()
            {
                TFCharacterSession.CurrentTransfomer.ApplylevlesAfter1();
                AddPointsToSkill = new RelayCommand<SkillTF>(AddPointsToSkil);
                RemovePointsFromSkill = new RelayCommand<SkillTF>(DecreasePointsFromSkill);
            SkillsPointBank = TFCharacterSession.CurrentTransfomer.generalPointBank;


                SkillsToBoost = TFCharacterSession.CurrentTransfomer.fullSkillList;

                Strength = TFCharacterSession.CurrentTransfomer.Strenght;

                Speed = TFCharacterSession.CurrentTransfomer.Speed;
                Smarts = TFCharacterSession.CurrentTransfomer.Smarts;
                Soical = TFCharacterSession.CurrentTransfomer.Social;



            //Skills
                 Athletics = TFCharacterSession.CurrentTransfomer.Athletics;
                Brawn = TFCharacterSession.CurrentTransfomer.Brawn;
                Conditioning = TFCharacterSession.CurrentTransfomer.Conditioning;
                Intimidation = TFCharacterSession.CurrentTransfomer.Intimidation;
                Might = TFCharacterSession.CurrentTransfomer.Might;


                Acrobatics = TFCharacterSession.CurrentTransfomer.Acrobatics;
                Driving = TFCharacterSession.CurrentTransfomer.Driving;
                Finesse = TFCharacterSession.CurrentTransfomer.Finesse;
                Inflitration = TFCharacterSession.CurrentTransfomer.Inflitration;
                Inititave = TFCharacterSession.CurrentTransfomer.Inititave;
                Targeting = TFCharacterSession.CurrentTransfomer.Targeting;


                Alertness = TFCharacterSession.CurrentTransfomer.Alertness;
                Culture = TFCharacterSession.CurrentTransfomer.Culture;
                Science = TFCharacterSession.CurrentTransfomer.Science;
                Survival = TFCharacterSession.CurrentTransfomer.Survival;
                Technology = TFCharacterSession.CurrentTransfomer.Technology;


                AnimalHandling = TFCharacterSession.CurrentTransfomer.AnimalHandling;
                Deception = TFCharacterSession.CurrentTransfomer.Deception;
                Preformance = TFCharacterSession.CurrentTransfomer.Preformance;
                Persuasion = TFCharacterSession.CurrentTransfomer.Persuasion;
                Streetwise = TFCharacterSession.CurrentTransfomer.Streetwise;


                Scores = TFCharacterSession.CurrentTransfomer.fullScoreList;

                getLinkedSkillForScore();


            }

          
            
         

         

            public int SkillsPointBank
            {
                get => _skillsPointBank;
                set
                {
                    _skillsPointBank = value;
                    NotifyPropertyChanged(nameof(SkillsPointBank));
                }
            }

            public void getLinkedSkillForScore()
            {
                //may need new object or boolean called linked skill
                ScoreTF target = Scores.First(s => s.Name == TFCharacterSession.CurrentTransfomer.sub.statToBoost); // this searches array for score
                string skilltolink = TFCharacterSession.CurrentTransfomer.ChosenLinkedSkill;
                target.LinkedbyFocus = target.CorrespondingSkills.First(c => c.Name == skilltolink);
                // we got the linked skill sweet


            }
            public void AddPointsToSkil(SkillTF skill)
            {
                var score = Scores.First(s => s.CorrespondingSkills.Contains(skill));
                if (SkillsPointBank == 0)
                {
                    MessageBox.Show("No more skill points to allocate");
                    return;
                }

                if (skill.SkillScore == 6)
                {
                    MessageBox.Show("Skill is at max rank");
                    return;
                }


                if (!score.TryIncreaseSKill(skill))
                {
                    MessageBox.Show($"points allocated to skills can not exceed value of Essance score current value {score.Name}");
                }

                SkillsPointBank -= 1;

            }
            public void DecreasePointsFromSkill(SkillTF skill)
            {
                var score = Scores.First(s => s.CorrespondingSkills.Contains(skill));
                if (skill.SkillScore <= 0)
                {
                    MessageBox.Show("can not lower score past 0.");
                    return;
                }
                if (skill.SkillScore == 1 && skill.IsKeySkill == true)
                {
                    MessageBox.Show("This score is raised as one of your Key skills uncheck to lower");
                    return;
                }
                else
                {
                    score.DecreaseSkill(skill);
                    SkillsPointBank += 1;
                }
            }

            
       
            private void NotifyPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }



