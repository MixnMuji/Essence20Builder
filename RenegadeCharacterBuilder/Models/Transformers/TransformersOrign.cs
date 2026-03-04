using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    internal class TransformersOrign
    {
        int health { get; set; }
        List<string> LanguagesSpoken { get; set; }
        string OrignBenefit { get; set; }
        int statIncrease { get; set; }

        Chassis chassis { get; set; } // object for chasis is not yet made

        public TransformersOrign(string name, int basehealth, string orignBenefit, int statInscrease, List<string> languages, Chassis basecassis)
        {
            this.health = basehealth;
            this.LanguagesSpoken = languages;
            this.statIncrease = statInscrease;
            this.chassis = basecassis;
        }

    }
}
