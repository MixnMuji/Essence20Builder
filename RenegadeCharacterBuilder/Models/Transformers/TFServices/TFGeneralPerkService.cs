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
             {"Additional Alt Mode", ApplyAAM},
             {"All Terrain Alt Mode", ApplyATAM },
             { "Cross-Training", ApplyCT },
             {"Human Companion", ApplyHC },
             {"Mentor", ApplyMentor },
             {"Object Alt Mode", ApplyOAM },
             {"Razor Tongue", ApplyRT },
             {"Size Change", ApplySC }

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
        private void ApplyAAM(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            
                 
                NavigationService.Navigate(new GenericPerkApplyerpage(PerkBeingApplied.ATAM));

             

                //actually need a page for this
                //actually needs a list for comparision
                //deseriilze model with chasis selection but remove existing chasis
                //have them pick

        }
        private void ApplyATAM(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            //make page with move types and add it to the alt mode
            //if multiple alt modes have them choose which one first
        }
        private void ApplyCT(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            //get characters level
            //make list of roles that aren't there own as well as the ranks under the level
            //grab the perks
            //display on page
        }
        private void ApplyHC(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            //give character a bool call human companion set it to true
            //have it go to a page that makes a human companion
        }
        private void ApplyMentor(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            //need score
            // needs skill
            //have them pick a score give it a second assioted skill
            //follow the same logic as the other associated skills
        }
        private void ApplyOAM(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            //List alt modes, have them pick one
            //change its movement to zero
            //Allow them to change the items name
        }
        private void ApplyRT(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            model.Cleverness += 2;
        }
        private void ApplySC(TransfomersCharacterModel model, GeneralPerkTF perk)
        {
            //get alt modes and pick one
            //change its size attribute
        }

    }
}
