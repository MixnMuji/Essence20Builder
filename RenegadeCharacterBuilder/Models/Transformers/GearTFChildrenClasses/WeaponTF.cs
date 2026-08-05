using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers.GearTFChildrenClasses
{
    public class WeaponTF: GearTF
    {
        

        public string Classification { get; set; }
        public int Hands { get; set; }
        public string Range { get; set; }
        public string Effect { get; set; }

        public string AlternateEffect { get; set; }

        public List<string> Traits { get; set; } = new();
    }
}
