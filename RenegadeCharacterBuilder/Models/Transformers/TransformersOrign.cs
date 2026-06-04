using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class TransformersOrign
    {
        public string Name { get; set; }
        public int StartingHealth { get; set; }
        public string OriginSkill { get; set; }
        public int? StrengthIncrease { get; set; }
        public int? SmartsIncrease { get; set;}
        public int? SocialIncrease { get; set; }
        public int? SpeedIncrease{ get; set; }

        public Botmode BotMode { get; set;}
        public Altmode AltMode { get; set; }


        public TransformersOrign()
        {
            
        }

    }
}

