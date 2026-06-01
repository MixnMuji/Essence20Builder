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

    }
}
