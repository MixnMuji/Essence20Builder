using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RenegadeCharacterBuilder.Models.Transformers.Enums;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class GeneralPerkTF
    {
        public string Name { get; set; }

        public PrequisiteTF? requirment { get; set; }

        public string Text { get; set; }

        public bool isSelected { get; set; }
        public int timesSelectable { get; set; }

        public TfEnums.PerkType type { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PerkBeingApplied? abreviationForExecute { get; set; }
        // public abstract void ApplyEffects(TransfomersCharacterModel characterModel);

    }
   
}
