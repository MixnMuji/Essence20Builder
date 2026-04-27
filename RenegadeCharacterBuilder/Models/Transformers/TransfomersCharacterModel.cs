using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Numerics;
using System.Text;
using System.Windows.Media.Animation;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    // these fields will be used to make a new object and the stats will mirror what's on the sheet as well
    public class TransfomersCharacterModel
    {
        public string Name { get; set; }
        public string Pronouns { get; set; }
        public TransformersOrign Orign { get; set; }
        public Roles Role { get; set; }
        public int Level { get; set; }
        public Alliegence Faction { get; set; }
        public List<string> Languages { get; set; }
        public List<InfluencesTF> Influences { get; set; }
        public List<HangUps> Hang_Ups { get; set; }
        public int Health { get; set; }
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

        public TransfomersCharacterModel()
        
                // redo the constructor to make it so that the optional ones are at the end and have default values;
        {
            
        }
        
        public void AssignScoresAndSkills(ScoreTF Str, ScoreTF Spd, ScoreTF Smt, ScoreTF Soc, SkillTF Ath,SkillTF Bra, SkillTF Con, SkillTF Int, SkillTF Mig,
            SkillTF Acro, SkillTF Dri, SkillTF Fin, SkillTF Inf, SkillTF Init, SkillTF Tar, 
            SkillTF Alert, SkillTF Cul , SkillTF Sci, SkillTF Sur, SkillTF Tech,
            SkillTF Ani, SkillTF Dec, SkillTF Pre, SkillTF Pro, SkillTF Street)
        {
            Strenght = Str;
           Speed = Spd;
            Smarts = Smt;
            Social = Soc;
            Athletics = Ath;
            Brawn = Bra;
            Conditioning = Con;
            Intimidation = Int;
            Might = Mig;

            Acrobatics = Acro;
            Driving = Dri;
            Finesse = Fin;
            Inflitration = Inf;
            Inititave = Init;
            Targeting = Tar;

            Alertness = Alert;
            Culture = Cul;
            Science = Sci;
            Survival = Sur;
            Technology = Tech;

            AnimalHandling = Ani;
            Deception = Dec;
            Preformance = Pre;
            Persuasion = Pro;
            Streetwise = Street;
    }
    }

   
   public enum Alliegence
    {
        Autobot = 0,
        Descepticon = 1

    }
    
}
