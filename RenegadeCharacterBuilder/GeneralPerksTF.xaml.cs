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
using  RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.Enums;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;
using RenegadeCharacterBuilder.Models.Transformers.TFServices;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

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
            var pickedPerks = viewmodel._qualifyingPerks.Where(p => p.isSelected == true).ToList();
            TFCharacterSession.CurrentTransfomer.PickedPerks = pickedPerks;
            TFCharacterSession.CurrentTransfomer.hasSpentGeneralPerkPoints = true; //bool will actually do the problem of sorting

       
            
          
         
            foreach (var perk in pickedPerks)
            {
                switch (perk.PerkBeingApplied) 
                {
                    case PerkBeingApplied.AAM:

                        NavigationService.Navigate(new AAMPerpage());
                        return;
                    

                    case PerkBeingApplied.ATAM:
                        NavigationService.Navigate(new ATAMperkPage());
                        return;



                    

                    case PerkBeingApplied.CT:
                        {
                            //get characters level
                            //make list of roles that aren't there own as well as the ranks under the level
                            //grab the perks
                            //display on page
                            /*
                            var filter = TFCharacterSession.CurrentTransfomer.Role;
                            int lvfilter = TFCharacterSession.CurrentTransfomer.CurrentLevel;
                            var Roles = LoadJson<TFRolesRoot>("Roles.json");
                            PerkExectuionData = Roles.Roles.Where(r => r.Name != filter.Name)
                            .SelectMany(r => r.Levels.Where(l => l.Level <= lvfilter && l.Perk != null)).SelectMany(l => l.Perk).ToList();
                            */
                            NavigationService.Navigate(new CTperkpage());
                        }
                        return;
                    case PerkBeingApplied.HC:
                        {
                            //navigate to chracter builder page set new bools that are minicon, and human companion so that if they are sent with the data it will load a different page.
                            NavigationService.Navigate(new HumanCompanion());
                        }
                        return;

                    case PerkBeingApplied.Mentor:
                        {
                            NavigationService.Navigate(new MentorSkill());
                          
                        }
                        return;

                    case PerkBeingApplied.OAM:
                        {
                            NavigationService.Navigate(new OamPerkpage());

                        }
                        return;
                    case PerkBeingApplied.SC:
                        {
                            NavigationService.Navigate(new SCperkpage());
                   
                         

                        }
                        return;
                    default:
                        {
                            gpService.ApplyPerk(TFCharacterSession.CurrentTransfomer, perk);// shouldapply perks now
                       
                        }
                    break;
                }
                
            }
                 NavigationService.Navigate(new LevelUpAfter1());
        }

        private void DecreaseValue(object sender, RoutedEventArgs e)
        {
            int limit = viewmodel.GeneralPerksPointBank;
            var target = sender as CheckBox;
            if(target.IsChecked == true)
            {
                if (viewmodel.GeneralPerksPointBank > 0)
                {
                    viewmodel.GeneralPerksPointBank -= 1;
                }
                else
                {
                    MessageBox.Show("You can't take anymore Perks");
                    target.IsChecked = false;
                    
                }
            }
            else
            {
                // It was unchecked, so return the point
                viewmodel.GeneralPerksPointBank += 1;
            }
        }
    }
}
