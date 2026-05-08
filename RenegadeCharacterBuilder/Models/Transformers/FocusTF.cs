using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class FocusTF
    {
        public string subclassName { get; set; }

        public string statToBoost { get; set; }

        public List<string> skillsToboost { get; set;}
        public List<FocusPerk> ranks {get; set; } 
    }
    //get focus with switch stament
}
