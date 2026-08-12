using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers.GearTFChildrenClasses
{
    public class UpgradeFilter
    {
        public List<string> NecessaryTraits { get; set; } = new(); //very simply things we need to exist in the weapon and logic can be put in the methods
        public List<string> ExcludedTraits { get; set; } = new (); // things we will take away if they exist

    }
}
