using System;
using System.Collections.Generic;
using System.Text;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers;

namespace RenegadeCharacterBuilder.CharacterModels.TransfomersCompaions
{
    public class pet: ParentCharacterModel
    {
       public type humanOrCon { get; set; }

        public SkillTF purpose { get; set; }

        public SkillTF purposeTwo { get; set; }         
    }

    }

    public enum type
    {
        Human = 0,
        Minicon =1
    }
}
