using System;
using System.Collections.Generic;
using System.Text;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.CharacterModels.TransfomersCompaions
{
    public class pet: ParentCharacterModel
    {
       public type humanOrCon { get; set; }

        public SkillTF purpose { get; set; }

        public SkillTF purposeTwo { get; set; }

        public pet()
        {
           
        }
        public void AssignPurpsoe(SkillTF Purpose, string CompanionName)
        {
            //this will make it so that the character will get the bonus from the mode master perk
            int bonus = TFCharacterSession.CurrentTransfomer.fullSkillList.FirstOrDefault(x => x.Name == Purpose.Name).SkillScore / 2;
            pet target = TFCharacterSession.CurrentTransfomer.companions.FirstOrDefault(x => x.Name == CompanionName);
            foreach(ScoreTF s in target.fullScoreList)
            {
                if (s.CorrespondingSkills.Contains(purpose))
                {
                    s.CurrentRank += bonus;
                }
            }
        }

        public void SetDeiveredStatValues()
        {
            Toughness.Value += 10 + Strenght.CurrentRank;
            Willpower.Value += 10 + Smarts.CurrentRank;
            Cleverness.Value += 10 + Social.CurrentRank;
            Evasion.Value = +10 + Speed.CurrentRank;
        }

    }

    public enum type
    {
        Human = 0,
        Minicon =1
    }
}
