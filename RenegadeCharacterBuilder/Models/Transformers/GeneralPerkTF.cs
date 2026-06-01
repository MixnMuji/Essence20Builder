using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class GeneralPerkTF
    {
        public string Name { get; set; }

        public PrequisiteTF? requirment { get; set; }

        public string Text { get; set; }

        public int timesSelectable { get; set; }

        public PerkType type { get; set; }
        // public abstract void ApplyEffects(TransfomersCharacterModel characterModel);
        
    }
    public enum PerkType
    {
        AddText = 0,
        AddObject =1,
        AddBoth = 2
    }
}
