using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class GearTF
    {
        public string Name { get; set; }

       // public string Range { get; set; }

       // public string Attack { get; set; }

       // public string Effect { get; set; }

       // public string Notes { get; set; }
        
        public string availibility { get; set; }

      //  public string benefit { get; set; }
        public PrequisiteTF Requirements { get; set;}



    }
}
