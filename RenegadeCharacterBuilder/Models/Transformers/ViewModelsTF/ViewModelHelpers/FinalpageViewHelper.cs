using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using RenegadeCharacterBuilder.CharacterModels.TransfomersCompaions;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers
{
    public class FinalpageViewHelper
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

        public int generalPointBank { get; set; }

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

        public List<Perk> miscellaneousPerks { get; set; } = new();

        public List<pet> companions { get; set; } = new();

        public FinalpageViewHelper()
        {
            Name = TFCharacterSession.CurrentTransfomer.Name;
            Pronouns = TFCharacterSession.CurrentTransfomer.Pronouns;
            Description = TFCharacterSession.CurrentTransfomer.Pronouns;
            Origns = TFCharacterSession.CurrentTransfomer.Origns;
            Altmodes = TFCharacterSession.CurrentTransfomer.Altmodes;
            Role = TFCharacterSession.CurrentTransfomer.Role;  // look over and think what need names vs what needs the actual values.
            sub = TFCharacterSession.CurrentTransfomer.sub;
            ChosenLinkedSkill = TFCharacterSession.CurrentTransfomer.ChosenLinkedSkill;
            Level = TFCharacterSession.CurrentTransfomer.Level;
            ActualPerksToSpend = TFCharacterSession.CurrentTransfomer.ActualPerksToSpend;
            CurrentLevel = TFCharacterSession.CurrentTransfomer.CurrentLevel;
            Faction = TFCharacterSession.CurrentTransfomer.Faction;
            Languages = TFCharacterSession.CurrentTransfomer.Languages;
            Influences = TFCharacterSession.CurrentTransfomer.Influences;
            Hang_Ups = TFCharacterSession.CurrentTransfomer.Hang_Ups;
            Health = TFCharacterSession.CurrentTransfomer.Health;
            generalPointBank = TFCharacterSession.CurrentTransfomer.generalPointBank;

            Strenght = TFCharacterSession.CurrentTransfomer.Strenght;
            Speed = TFCharacterSession.CurrentTransfomer.Speed;
            Smarts = TFCharacterSession.CurrentTransfomer.Smarts;
            Social = TFCharacterSession.CurrentTransfomer.Social;
            Toughness = TFCharacterSession.CurrentTransfomer.Toughness + 10 + Strenght.CurrentRank;
            Cleverness = TFCharacterSession.CurrentTransfomer.Cleverness + 10 + Smarts.CurrentRank;

            Willpower = TFCharacterSession.CurrentTransfomer.Willpower + 10 + Social.CurrentRank;
            Evasion = TFCharacterSession.CurrentTransfomer.Evasion + 10 + Speed.CurrentRank;




            Athletics = TFCharacterSession.CurrentTransfomer.Athletics;
            Brawn = TFCharacterSession.CurrentTransfomer.Brawn;
            Conditioning = TFCharacterSession.CurrentTransfomer.Conditioning;
            Intimidation = TFCharacterSession.CurrentTransfomer.Intimidation;
            Might = TFCharacterSession.CurrentTransfomer.Might;

            //Speed gets
            Acrobatics = TFCharacterSession.CurrentTransfomer.Acrobatics;
            Driving = TFCharacterSession.CurrentTransfomer.Driving;
            Finesse = TFCharacterSession.CurrentTransfomer.Finesse;
            Inflitration = TFCharacterSession.CurrentTransfomer.Inflitration;
            Inititave = TFCharacterSession.CurrentTransfomer.Inititave;
            Targeting = TFCharacterSession.CurrentTransfomer.Targeting;

            //smarts gets

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

            fullSkillList = TFCharacterSession.CurrentTransfomer.fullSkillList;
            fullScoreList = TFCharacterSession.CurrentTransfomer.fullScoreList;
            SubclassTextBlocks = TFCharacterSession.CurrentTransfomer.SubclassTextBlocks;
            GeneralPerkTextBlock = TFCharacterSession.CurrentTransfomer.GeneralPerkTextBlock;
            PickedPerks = TFCharacterSession.CurrentTransfomer.PickedPerks;

            miscellaneousPerks = TFCharacterSession.CurrentTransfomer.miscellaneousPerks;

            companions = TFCharacterSession.CurrentTransfomer.companions;
        }
   
    }



}


