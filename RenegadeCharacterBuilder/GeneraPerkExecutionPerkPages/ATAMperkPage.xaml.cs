using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.Enums;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.TFServices;

using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for ATAMperkPage.xaml
    /// </summary>
    public partial class ATAMperkPage : Page
    {
        public ATAMVMTF vm { get; set; }
        public TFGeneralPerkService gpService { get; set; }
      
        public ATAMperkPage()
        {
            InitializeComponent();
             vm = new ATAMVMTF();
             DataContext = vm;
        }

        private void applyAndContinue(object sender, RoutedEventArgs e)
        {

            string movementchoice = moveSelected.SelectedItem as string;
            if (movementchoice == null)
            {
                MessageBox.Show("Please select a movement type.");
                return;
            }
            foreach (TransformersOrign mode in TFCharacterSession.CurrentTransfomer.Origns)
            {
                if (vm.SelectedAltMode == mode)
                {
                    mode.AltMode.Type2 = movementchoice;
                    mode.AltMode.Movement2 = 20;
                }
            }
            figureOutWhatToDo();

        }
       private void figureOutWhatToDo() { //make a method taht can be called across the board nut methods can't navigate huh
        
            gpService = new TFGeneralPerkService();
            if (TFCharacterSession.CurrentTransfomer.PickedPerks.Count() == 1) // base case if we only have one general perk move on.
            {
                NavigationService.Navigate(new FinalPageAndConfirmation());
            }

            int i = 0;
            foreach (var perk in TFCharacterSession.CurrentTransfomer.PickedPerks)
            {

                if (perk.PerkBeingApplied == PerkBeingApplied.ATAM)
                {
                    break; 
                }
                i++;
            }
            if (i == TFCharacterSession.CurrentTransfomer.PickedPerks.Count() - 1)
            {
                //if the current index is equal to count minus one were at the end of the list
                NavigationService.Navigate(new FinalPageAndConfirmation());
            }
            if (i < TFCharacterSession.CurrentTransfomer.PickedPerks.Count() - 1)
            {
                for (int j = i + 1; j <= TFCharacterSession.CurrentTransfomer.PickedPerks.Count() - 1; j++)
                { //basically if we start at the next index over we can apply the perk if they're normal or navigate to another page
                    switch (TFCharacterSession.CurrentTransfomer.PickedPerks[j].PerkBeingApplied)
                    {


                        case PerkBeingApplied.AAM:
                            NavigationService.Navigate(new AAMPerpage());
                            break;

                        case PerkBeingApplied.CT:
                            {

                                NavigationService.Navigate(new CTperkpage());
                            }
                            break;
                        case PerkBeingApplied.HC:
                            {
                                //navigate to chracter builder page set new bools that are minicon, and human companion so that if they are sent with the data it will load a different page.
                                NavigationService.Navigate(new HumanCompanion());
                            }
                            break;

                        case PerkBeingApplied.Mentor:
                            {
                                NavigationService.Navigate(new MentorSkill());
                                // have it navigate to a page and if it's this populate the list of scores and skills and have them listed to pick them
                            }
                            break;

                        case PerkBeingApplied.OAM:
                            {
                                NavigationService.Navigate(new OamPerkpage());

                            }
                            break;
                        case PerkBeingApplied.SC:
                            {
                                NavigationService.Navigate(new SCperkpage());
                          
                            }
                            break;
                        default:
                            {
                                gpService.ApplyPerk(TFCharacterSession.CurrentTransfomer, TFCharacterSession.CurrentTransfomer.PickedPerks[j]);// should apply perks now

                            }
                            break;
                    }
                }
                //go through PerksPicked and find where perkbeing applied equals AAM
                // if the index of that spot is eqal to the count-1 return, if its less than, move the index and use the switch case again.
            }
        }
    }
}
