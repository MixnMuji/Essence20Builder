using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RenegadeCharacterBuilder.Models.Transformers.Enums;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class GearTF
    {
        public string Name { get; set; }

        // public string Range { get; set; }

        // public string Attack { get; set; }

        // public string Effect { get; set; }

        // public string Notes { get; set; }
        
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EquimentType availibility { get; set; }

      //  public string benefit { get; set; }
        public PrequisiteTF Requirements { get; set;}



    }
}
