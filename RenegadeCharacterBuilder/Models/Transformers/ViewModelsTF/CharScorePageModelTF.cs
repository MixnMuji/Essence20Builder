using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
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

        public string oringName { get; set; }

        public List<ScoreTF> modeMasterEssences { get; set; } = new();

        public List<SkillTF> originBoostOptions { get; set; } = new();

        public ObservableCollection<SkillTF> modeMasterSkillOptions { get; set; } = new();

        private SkillTF _skillBoostFromOrigin;
        public SkillTF SkillBoostFromOrigin
        {
            get => _skillBoostFromOrigin;
            set
            {

                if(_skillBoostFromOrigin != null)
                {
                    SkillBoostFromOrigin.SkillScore -= 1; // if it already has a skill associated it's value is removed
                }
                _skillBoostFromOrigin = value;
                _skillBoostFromOrigin.SkillScore += 1;
                NotifyPropertyChanged(nameof(SkillBoostFromOrigin));
               
            }
        }

        private int _pointBank = 9;
        private int _skillsPointBank = 0;

        public List<SkillTF> SkillsToBoost { get; }

        private List<SkillTF> SelectedKeySkills { get;}
        public string CharacterRoleForKeyScores { get; set; }
        public ICommand AddpointsToScore { get; }
        public ICommand RemovePointsFromScore { get; }
        
        public ICommand AddPointsToSkill { get; }

        public ICommand RemovePointsFromSkill { get; }

        public ICommand GetRoleForAllocations { get; }

        
        public List <ScoreTF> Scores { get; }

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
        public SkillTF Culture {get;}
        public SkillTF Science { get; }
        public SkillTF Survival{ get;}
        public SkillTF Technology { get; }

        public SkillTF AnimalHandling { get; }
        public SkillTF Deception { get; }
        public SkillTF Preformance { get; }
        public SkillTF Persuasion { get; }
        public SkillTF Streetwise { get; }
      
       



        public CharScorePageModelTF()
        {
            oringName = TFCharacterSession.CurrentTransfomer.Origns[0].Name;
            SelectedKeySkills = new List<SkillTF>();
            AddpointsToScore = new RelayCommand<ScoreTF>(AddPointsToScore);
            RemovePointsFromScore = new RelayCommand<ScoreTF>(DecreasePontsFromScore);
            AddPointsToSkill = new RelayCommand<SkillTF>(AddPointsToSkil);
            RemovePointsFromSkill = new RelayCommand<SkillTF>(DecreasePointsFromSkill);
            CharacterRoleForKeyScores = TFCharacterSession.CurrentTransfomer.Role.Name;
            if (string.IsNullOrWhiteSpace(CharacterRoleForKeyScores))
            {
                CharacterRoleForKeyScores = "Analyst";
            }


            SkillsToBoost = new List<SkillTF>();
            
           
       


            //Strenght skills
            Athletics = new SkillTF("Athletics");
            Brawn = new SkillTF("Brawn");
            Conditioning = new SkillTF("Conditioning");
            Intimidation = new SkillTF("Intimidation");
            Might = new SkillTF("Might");


            Acrobatics = new SkillTF("Acrobatics");
            Driving = new SkillTF("Driving");
            Finesse = new SkillTF("Finesse");
            Inflitration = new SkillTF("Inflitration");
            Inititave = new SkillTF("Inititave");
            Targeting = new SkillTF("Targeting");


            Alertness = new SkillTF("Alertness");
            Culture = new SkillTF("Culture");
            Science= new SkillTF("Science");
            Survival = new SkillTF("Survival");
            Technology= new SkillTF("Technology");


            AnimalHandling = new SkillTF("Animal Handling");
            Deception = new SkillTF("Deception");
            Preformance = new SkillTF("Preformance");
            Persuasion = new SkillTF("Persuasion");
            Streetwise =new SkillTF("Streetwise");
               
           
            // define skills with name and a get and set them equal like Athletics = new Skilltf thletics also switch must come later after everything is defined or it will be null
            //Strength block
            Strength = new ScoreTF("Strength", new List<SkillTF>
            {
                Athletics,
                Brawn,
                Conditioning,
                Intimidation ,
                Might
            });

            Speed = new ScoreTF("Speed", new List<SkillTF>
            {
                Acrobatics,
                Driving,
                Finesse,
                Inflitration,
                Inititave,
                Targeting



            });

            Smarts = new ScoreTF("Smarts", new List<SkillTF>
            {
                Alertness,
                Culture,
                Science,
                Survival,
                Technology
               

             });

            Soical = new ScoreTF("Social", new List<SkillTF>
            {
                AnimalHandling,
                Deception,
                Preformance,
                Persuasion,
                Streetwise,

             });
            Scores = new List<ScoreTF>
            {
            Strength,
            Speed,
            Smarts,
            Soical
            };

            getLinkedSkillForScore();
            GetOptionsForOriginStats();



        }
        
        public void GetOptionsForOriginStats()
        {
            Dictionary<string, List<SkillTF>> OrignSkillPairs = new Dictionary<string, List<SkillTF>>
            {
                ["Support"] = [Technology, Science],
                ["Seeker"] = [Driving, Acrobatics],
                ["Rainmaker"] = [Targeting, Inititave],
                ["Outrider"] = [Alertness,Survival],
                ["Monolith"] = [Might, Brawn],
                ["Lookout"] = [Finesse, Deception, Streetwise, Inflitration],
                ["Cutter"] = [Athletics, Might],
                ["Champion"] = [Streetwise, Persuasion]
              
            };

            

            if(OrignSkillPairs.TryGetValue(oringName, out List<SkillTF> skills))
            {
                originBoostOptions = skills;
            }
            



        }
        public void findRoleForStats()
        {
            switch (CharacterRoleForKeyScores)
            {
                case "Analyst":
                    defineScoresAndSkillChoice(Speed, Smarts, new List<SkillTF> { Alertness, Finesse, Inflitration, Inititave, Science, Technology });
                    break;

                case "FieldCommander":
                    defineScoresAndSkillChoice(Strength, Soical, new List<SkillTF> { Brawn, Deception, Intimidation, Might, Preformance, Persuasion });
                    break;

                case "Gunner":
                    defineScoresAndSkillChoice(Speed, Smarts, new List<SkillTF> { Alertness, Inititave, Survival, Targeting });
                    break;

                case "ModeMaster": //this needs its own method unfortunately
                    defineScoresAndSkillChoice(Speed, Smarts, new List<SkillTF> { Alertness, Finesse, Inflitration, Inititave, Science, Technology });
                    break;

                case "Scientist":
                    defineScoresAndSkillChoice(Strength, Smarts, new List<SkillTF> { Brawn, Conditioning, Science, Technology });
                    break;

                case "Scout":
                    defineScoresAndSkillChoice(Speed, Soical, new List<SkillTF> { Alertness, Deception, Inflitration, Inititave, Streetwise });
                    break;

                case "Warrior":
                    defineScoresAndSkillChoice(Strength, Smarts, new List<SkillTF> { Alertness, Brawn, Conditioning, Culture, Might, Survival });
                    break;
                // the case from modemaster needs to take a skill and it needs to take return any list
                //esscens must have coresponding skills


            }
            

            
        }

        public ObservableCollection<SkillTF> ModemasterSelections(string choiceone, string choicetwo)
        {
            ObservableCollection<SkillTF> merged = new ObservableCollection<SkillTF>();
            var targetOne = TFCharacterSession.CurrentTransfomer.fullScoreList.FirstOrDefault(s => s.Name == choiceone);
            var targetTwo = TFCharacterSession.CurrentTransfomer.fullScoreList.FirstOrDefault(s => s.Name == choicetwo);
            foreach( var s in targetOne.CorrespondingSkills)
            {
                merged.Add(s);
            }
            foreach(var s in targetTwo.CorrespondingSkills)
            {
                merged.Add(s);
            }
            foreach(var i in merged) // when list is updated by new selection has to be observable collection because components change 
            {
                if(!targetOne.CorrespondingSkills.Contains(i) || !targetTwo.CorrespondingSkills.Contains(i))
                {
                    merged.Remove(i);
                }
            }
            return merged;
            
        }
        public List<SkillTF> defineScoresAndSkillChoice( ScoreTF IncreaseOne, ScoreTF IncreaseTwo, List<SkillTF> Choices )
        {
            IncreaseOne.CurrentRank += 1;
            IncreaseOne.IsKeyScore = true;
            IncreaseTwo.CurrentRank += 1;
            IncreaseTwo.IsKeyScore = true;

           

            foreach (var skill in Choices)
            {
                SkillsToBoost.Add(skill);
                skill.PropertyChanged += SkillChanged;
            }
            return SkillsToBoost;
            //take role scratch that it's in the switch case
            //add to scores and turn Keyscore into true
            // Create a list of List TF with options
            //pick a skill for loop listing options
            //after you pick remove the option from the list and pick again
            //changes boolian to SkillTF to true!

        }

        private void SkillChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName != nameof(SkillTF.IsSelected))
            {
                return;
            }
            var skill = (SkillTF)sender;
            if (skill.IsSelected)
            {
                if(SelectedKeySkills.Count >= 2)
                {
                    skill.IsSelected = false;
                    MessageBox.Show("You may only have two Key skills");
                    return;
                }
                SelectedKeySkills.Add(skill);
            }
            else
            {
                skill.IsKeySkill = false;
                skill.SkillScore -= 1;
                SelectedKeySkills.Remove(skill);
                return;
            }

            if(SelectedKeySkills.Count == 2)
            {
                defineKeySkills(SelectedKeySkills[0], SelectedKeySkills[1]);
            }

        }
       public void defineKeySkills(SkillTF skill1, SkillTF skill2)
        {
            skill1.SkillScore = 1;
            skill1.IsKeySkill = true;
            skill2.SkillScore = 1;
            skill2.IsKeySkill = true;

            // so because this fires twice it makes the skill add 2 to the role, lets see if equals fixes that


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

        public int SkillsPointBank
        {
            get => _skillsPointBank;
            set { _skillsPointBank = value;
                NotifyPropertyChanged(nameof(SkillsPointBank));
                    }
        }

        public void getLinkedSkillForScore()
        {
            //may need new object or boolean called linked skill
            if(TFCharacterSession.CurrentTransfomer.Role.Name == "Mode Master")
            {
                return;
            }
                ScoreTF target = Scores.First(s=> s.Name== TFCharacterSession.CurrentTransfomer.sub.statToBoost); // this searches array for score
            string skilltolink = TFCharacterSession.CurrentTransfomer.ChosenLinkedSkill;
            target.LinkedbyFocus = target.CorrespondingSkills.First(c => c.Name == skilltolink);
            // we got the linked skill sweet
            

        }
        public void AddPointsToSkil(SkillTF skill)
        {
            var score = Scores.First(s => s.CorrespondingSkills.Contains(skill));
            if(SkillsPointBank == 0)
            {
                MessageBox.Show("No more skill points to allocate");
                return;
            }
           
            if(skill.SkillScore == 6)
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
            if(skill.SkillScore == 1 && skill.IsKeySkill == true)
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

        public void AddPointsToScore(ScoreTF targetScore)
        {
            if (PointsBank > 0)
            {
                targetScore.AddToScore();
                PointsBank -= 1;
                SkillsPointBank += 1;
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
                SkillsPointBank -= 1;
            }
        }

        public void setLevelOneBoostForModeMaster(ScoreTF chosenScore)
        {
            if(originBoostOptions.Count() >= 2) // if we've picked 2 remvoe selection 1 and put in the new one
            {
                modeMasterEssences.Remove(modeMasterEssences[0]); 
                modeMasterEssences.Add(chosenScore);
            }
            else
            {
                modeMasterEssences.Add(chosenScore);
            }
        }

        public void UpdateModeMasterSelection()
        {
            int mostRecentData = originBoostOptions.Count() - 1;
            foreach(SkillTF x in modeMasterEssences[mostRecentData].CorrespondingSkills)
            {
                modeMasterSkillOptions.Add(x);
            }
            foreach(SkillTF x in modeMasterSkillOptions)
            {
                if (!modeMasterEssences[0].CorrespondingSkills.Contains(x) && !modeMasterEssences[1].CorrespondingSkills.Contains(x))
                {
                    modeMasterSkillOptions.Remove(x); //basically if neither list has them we no they're from an early pass.
                }
            }
            
        }
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
