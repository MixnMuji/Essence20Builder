using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using RenegadeCharacterBuilder.Models.Transformers.Enums;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;
using RenegadeCharacterBuilder.Models.Transformers.TFServices;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;
using  RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for GeneralPerksTF.xaml
    /// </summary>
    public partial class GeneralPerksTF : Page
    {
        public TFGeneralPerkService gpService { get; set; }
        public GeneralPerksVMTF viewmodel { get; set; }
        public GeneralPerksTF()
        {
            InitializeComponent();
            viewmodel = new GeneralPerksVMTF();
            viewmodel.GetApplicablePerks(TFCharacterSession.CurrentTransfomer.CurrentLevel, TFCharacterSession.CurrentTransfomer.fullSkillList, TFCharacterSession.CurrentTransfomer.fullScoreList);
            DataContext = viewmodel;
            
        }

        private void ContinueAndGetApplyPerk(object sender, RoutedEventArgs e)
        {

            gpService = new TFGeneralPerkService();
            var pickedPerks = viewmodel._qualifyingPerks.Where(p => p.isSelected == true);
          
         
            foreach (var perk in pickedPerks)
            {
                switch (perk.abreviationForExecute) 
                {
                    case PerkBeingApplied.AAM:

                        NavigationService.Navigate( new AAMPerpage());
                    break;

                    case PerkBeingApplied.ATAM:
                        NavigationService.Navigate(new ATAMperkPage());




                     break;

                    case PerkBeingApplied.CT:
                        {
                            //get characters level
                            //make list of roles that aren't there own as well as the ranks under the level
                            //grab the perks
                            //display on page
                            var filter = TFCharacterSession.CurrentTransfomer.Role;
                            int lvfilter = TFCharacterSession.CurrentTransfomer.CurrentLevel;
                            var Roles = LoadJson<TFRolesRoot>("Roles.json");
                            PerkExectuionData = Roles.Roles.Where(r => r.Name != filter.Name)
                            .SelectMany(r => r.Levels.Where(l => l.Level <= lvfilter && l.Perk != null)).SelectMany(l => l.Perk).ToList();

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
                            List<string> sizes = ["Common", "long", "Huge"];
                         

                        }
                        break;
                    default:
                        {
                            gpService.ApplyPerk(TFCharacterSession.CurrentTransfomer, perk);// shouldapply perks now
                            
                        }
                    break;
                }
                NavigationService.Navigate(new FinalPageAndConfirmation());
            }
     
        }
    }
}
