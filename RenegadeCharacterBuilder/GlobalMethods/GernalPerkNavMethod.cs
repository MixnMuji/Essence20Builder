using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages;
using RenegadeCharacterBuilder.Models.Transformers.Enums;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.TFServices;
using RenegadeCharacterBuilder.Models.Transformers.TFServices;

namespace RenegadeCharacterBuilder.GlobalMethods
{
    public static class GernalPerkNavMethod
    {

        public static void GoToNextPerk(NavigationService navigationService, PerkBeingApplied currentperk)
        {

            if (TFCharacterSession.CurrentTransfomer.PickedPerks.Count() == 1) // base case if we only have one general perk move on.
            {
                navigationService.Navigate(new LevelUpAfter1());
                return;
            }

            int i = 0;
            foreach (var perk in TFCharacterSession.CurrentTransfomer.PickedPerks)
            {

                if (perk.PerkBeingApplied == currentperk)
                {
                    break; // will boot us out with the index we need
                }
                i++; // if we aren't at the index keep going
            }
            if (i == TFCharacterSession.CurrentTransfomer.PickedPerks.Count() - 1)
            {
                //if the current index is equal to count minus one were at the end of the list
                navigationService.Navigate(new LevelUpAfter1());
            }
            if (i < TFCharacterSession.CurrentTransfomer.PickedPerks.Count() - 1)
            {
                for (int j = i + 1; j <= TFCharacterSession.CurrentTransfomer.PickedPerks.Count() - 1; j++)
                { //basically if we start at the next index over we can apply the perk if they're normal or navigate to another page
                    switch (TFCharacterSession.CurrentTransfomer.PickedPerks[j].PerkBeingApplied)
                    {
                        case PerkBeingApplied.AAM:
                            {
                                navigationService.Navigate(new AAMPerpage());
                                return;
                            }
                            

                        case PerkBeingApplied.ATAM:
                            navigationService.Navigate(new ATAMperkPage());
                            return;

                        case PerkBeingApplied.CT:
                            {

                                navigationService.Navigate(new CTperkpage());
                            }
                            return;
                        case PerkBeingApplied.HC:
                            {
                                //navigate to chracter builder page set new bools that are minicon, and human companion so that if they are sent with the data it will load a different page.
                                navigationService.Navigate(new HumanCompanion());
                            }
                            return;

                        case PerkBeingApplied.Mentor:
                            {
                                navigationService.Navigate(new MentorSkill());
                                // have it navigate to a page and if it's this populate the list of scores and skills and have them listed to pick them
                            }
                            return;

                        case PerkBeingApplied.OAM:
                            {
                                navigationService.Navigate(new OamPerkpage());

                            }
                            return; 
                        case PerkBeingApplied.SC:
                            {
                                navigationService.Navigate(new SCperkpage());
                                List<string> sizes = ["Common", "long", "Huge"];


                            }
                            return;
                        default:
                            {
                                TFGeneralPerkService gpService = new TFGeneralPerkService();
                                gpService.ApplyPerk(TFCharacterSession.CurrentTransfomer, TFCharacterSession.CurrentTransfomer.PickedPerks[j]);// should apply perks now

                            }
                            break;
                    }
                }
            }
        }
    }
}
