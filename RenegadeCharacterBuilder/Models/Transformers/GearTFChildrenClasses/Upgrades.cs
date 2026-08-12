using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers.GearTFChildrenClasses
{
    public class Upgrades: GearTF
    {
        public string Benefit { get; set; }
        public string abbrivation { get; set; } // this will be used with a string method callinng pair. like for applying perks
        public UpgradeFilter Filter { get; set; }
    }
}
