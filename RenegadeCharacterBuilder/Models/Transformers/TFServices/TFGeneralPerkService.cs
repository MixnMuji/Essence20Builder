using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Navigation;
using RenegadeCharacterBuilder.Models.Transformers.Roots;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

namespace RenegadeCharacterBuilder.Models.Transformers.TFServices
{
    public class TFGeneralPerkService
    {
        private readonly Dictionary<string, Action<TransfomersCharacterModel, GeneralPerkTF>> _effects;
        public TFGeneralPerkService()
        {
            _effects = new()
            {
             {"Dodgy", ApplyDodgy},
             {"Durabyllium Super Alloy", ApplyDSA },
           
         
         
             {"Razor Tongue", ApplyRT },
            

            };

        }
        public void ApplyPerk(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            if(perk.type == Enums.TfEnums.PerkType.AddText || perk.type == Enums.TfEnums.PerkType.AddBoth)
            {
                model.GeneralPerkTextBlock.Add(perk.Text);
            }
            if(_effects.TryGetValue(perk.Name, out var effect))
            {
                effect(model, perk);
            }
        }

        private void ApplyDodgy(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            model.Evasion += 2;
        }
        private void ApplyDSA(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            model.Toughness += 2;
        }
       
        private void ApplyRT(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            model.Cleverness += 2;
        }
      

    }
}
