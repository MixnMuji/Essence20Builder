using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class FocusPerk
    {
        public string ParentRole { get; set; }

        public string SubclassName { get; set; }

        public int LevelRequirment { get; set; }

        public string AbilityName { get; set; }

        public string AbilityEffect { get; set;}
    }
}
