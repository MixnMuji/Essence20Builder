using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class LevelTF
    {

        public int Level { get; set; }

        public List<Perk?> Perk { get; set; }

        public int? GeneralPerkCount { get; set; } // is logic for when character gets perks

        public int? FocusProgression { get; set; } //same thing for general perk
        public int? SpeedBoost { get; set; }
        public int? SmartsBoost { get; set; }
        public int? StrengthBoost { get; set; }
        public int? SocialBoost { get; set; }
    }
    
}
