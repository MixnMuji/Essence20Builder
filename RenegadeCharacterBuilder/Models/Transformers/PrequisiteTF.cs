using System;
using System.Collections.Generic;
using System.Text;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class PrequisiteTF
    {
        public int? statRequirement { get; set; } 
        public List<string>? ApplicableStats { get; set; }
        public int? levelRequirement { get; set; }

        //basically our checker if characters can do a given stat

        public bool CheckIfMet(int? targetNumber,List<string>? ApplicableStats, int? levelReq, TransfomersCharacterModel CurrentChar)
        {
            // if theres stats compare them to the stats of the character
            // need to put skills in a list
            // List<string> toCheck =


            if (levelReq != null && CurrentChar.Level.Level >= targetNumber)
                return true;

            return false;
        }

    }
}
