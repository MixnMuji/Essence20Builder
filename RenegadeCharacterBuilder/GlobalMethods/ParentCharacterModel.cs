using System;
using System.Collections.Generic;
using System.Text;
using RenegadeCharacterBuilder.Models;
using RenegadeCharacterBuilder.Models.Transformers;

namespace RenegadeCharacterBuilder.GlobalMethods
{
    public class ParentCharacterModel
    {
        public string Name { get; set; }
        public string Pronouns { get; set; }

        public string Description { get; set; }
        public List<TransformersOrign> Origns { get; set; } = new(); // make an object called origin
        public Roles Role { get; set; }
        public FocusTF sub { get; set; }
        public string ChosenLinkedSkill { get; set; }
        public LevelTF Level { get; set; }
        public int ActualPerksToSpend { get; set; }
        public int CurrentLevel { get; set; }
        public List<string> Languages { get; set; }
        public List<InfluencesTF> Influences { get; set; }
        public List<HangUps> Hang_Ups { get; set; }
        public int Health { get; set; }

        public DirivedStat Evasion { get; set; }
        public DirivedStat Toughness { get; set; }
        public DirivedStat Cleverness { get; set; }
        public DirivedStat Willpower { get; set; }
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


        public void AssignScoresAndSkills(ScoreTF Str, ScoreTF Spd, ScoreTF Smt, ScoreTF Soc, SkillTF Ath, SkillTF Bra, SkillTF Con, SkillTF Int, SkillTF Mig,
            SkillTF Acro, SkillTF Dri, SkillTF Fin, SkillTF Inf, SkillTF Init, SkillTF Tar,
            SkillTF Alert, SkillTF Cul, SkillTF Sci, SkillTF Sur, SkillTF Tech,
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
    }
}
