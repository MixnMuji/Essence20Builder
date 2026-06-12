using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Windows.Media.Animation;
using RenegadeCharacterBuilder.Models.Transformers.Roots;
using RenegadeCharacterBuilder.Models.Transformers.Enums;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    // these fields will be used to make a new object and the stats will mirror what's on the sheet as well
    public class TransfomersCharacterModel
    {
        public string Name { get; set; }
        public string Pronouns { get; set; }

        public string Description { get; set; }
        public List<TransformersOrign> Origns { get; set; } = new();
        public List<Altmode> Altmodes { get; set; } = new List<Altmode>();
        public Roles Role { get; set; }
        public FocusTF sub { get; set; }
        public string ChosenLinkedSkill { get; set; }
        public LevelTF Level { get; set; }
        public int ActualPerksToSpend { get; set; }
        public int CurrentLevel { get; set; }
        public Alliegence Faction { get; set; }
        public List<string> Languages { get; set; }
        public List<InfluencesTF> Influences { get; set; }
        public List<HangUps> Hang_Ups { get; set; }
        public int Health { get; set; }

        public int Evasion { get; set; }
        public int Toughness { get; set; }
        public int Cleverness { get; set; }
        public int Willpower { get; set; }
        public ScoreTF Strenght { get; set; }
        public ScoreTF Speed { get; set; }
        public ScoreTF Smarts { get; set; }
        public ScoreTF Social { get; set; }


        public SkillTF Athletics { get; set; }
        public SkillTF Brawn { get; set; }
        public SkillTF Conditioning { get; set; }
        public SkillTF Intimidation { get; set; }
        public SkillTF Might { get; set; }


        //Speed gets
        public SkillTF Acrobatics { get; set; }
        public SkillTF Driving { get; set; }
        public SkillTF Finesse { get; set; }
        public SkillTF Inflitration { get; set; }
        public SkillTF Inititave { get; set; }
        public SkillTF Targeting { get; set; }

        //smarts gets

        public SkillTF Alertness { get; set; }
        public SkillTF Culture { get; set; }
        public SkillTF Science { get; set; }
        public SkillTF Survival { get; set; }
        public SkillTF Technology { get; set; }

        public SkillTF AnimalHandling { get; set; }
        public SkillTF Deception { get; set; }
        public SkillTF Preformance { get; set; }
        public SkillTF Persuasion { get; set; }
        public SkillTF Streetwise { get; set; }

        public List<SkillTF> fullSkillList { get; set; }
        public List<ScoreTF> fullScoreList { get; set; }

        public List<FocusPerk> SubclassTextBlocks { get; set; } = new();
        public List<string> GeneralPerkTextBlock { get; set; } = new();
        public List<GeneralPerkTF> PickedPerks { get; set; } = new();
        public TransfomersCharacterModel()
        
        {
            
            
        }
        
        public void AssignScoresAndSkills(ScoreTF Str, ScoreTF Spd, ScoreTF Smt, ScoreTF Soc, SkillTF Ath,SkillTF Bra, SkillTF Con, SkillTF Int, SkillTF Mig,
            SkillTF Acro, SkillTF Dri, SkillTF Fin, SkillTF Inf, SkillTF Init, SkillTF Tar, 
            SkillTF Alert, SkillTF Cul , SkillTF Sci, SkillTF Sur, SkillTF Tech,
            SkillTF Ani, SkillTF Dec, SkillTF Pre, SkillTF Pro, SkillTF Street)
        {
            fullScoreList = new List<ScoreTF>([ 
                Strenght = Str,
                Speed = Spd,
                Smarts = Smt,
                Social = Soc
                
            ]);
            
            fullSkillList = new List<SkillTF>([
                
                Athletics = Ath,
                Brawn = Bra,
                Conditioning = Con,
                Intimidation = Int,
                Might = Mig,

                Acrobatics = Acro,
                Driving = Dri,
                Finesse = Fin,
                Inflitration = Inf,
                Inititave = Init,
                Targeting = Tar,

                Alertness = Alert,
                Culture = Cul,
                Science = Sci,
                Survival = Sur,
                Technology = Tech,

                AnimalHandling = Ani,
                Deception = Dec,
                Preformance = Pre,
                Persuasion = Pro,
                Streetwise = Street
                ]);


    }

       
        
        public void GetGeneralPerkPonts()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "Roles.json");
            var json = File.ReadAllText(path);
            var RoleRoot = JsonSerializer.Deserialize<TFRolesRoot>(json);
            var role = RoleRoot.Roles.First(r => r.Name == Role.Name);
            int i = 0;
            while(i< CurrentLevel)
            {
                if (role.Levels[i].GeneralPerkCount!= null)
                {
                    ActualPerksToSpend++;
                }
                i++;
            }
            
            // find current level
            // go to the json and look through it until we hit our level ie while Json.index >= level
            // then we say if the level contains general perk, general perk count ++; which means we need a forloop
        }
    }

   
   public enum Alliegence
    {
        Autobot = 0,
        Descepticon = 1

    }
   
}
