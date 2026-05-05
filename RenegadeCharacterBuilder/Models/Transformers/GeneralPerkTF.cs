using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class GeneralPerkTF
    {
        public string Name;

        public PrequisiteTF? requirment { get; set; }

        public string Text { get; set; }

        public int timesSelectable { get; set; }

        // public abstract void ApplyEffects(TransfomersCharacterModel characterModel);
        
    }
}
