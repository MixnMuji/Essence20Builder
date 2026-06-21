using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers
{
    public class CTHelperVM
    {
        public string RoleName { get; set; } = "";
        public ObservableCollection<Perk> legalPerks { get; set; } = new();

        
    }
}
