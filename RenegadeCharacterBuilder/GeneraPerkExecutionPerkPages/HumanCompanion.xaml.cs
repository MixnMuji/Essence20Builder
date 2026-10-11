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
using RenegadeCharacterBuilder.CharacterModels.TransfomersCompaions;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.Enums;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for HumanCompanion.xaml
    /// </summary>
    public partial class HumanCompanion : Page
    {
        public CharScorePageModelTF Viewmodel { get; set; }
        public pet human = new pet();

        public SkillTF MiniconPurpose1 { get; set; } 
       
        public HumanCompanion()
        {
            Viewmodel = new CharScorePageModelTF();
            InitializeComponent();
            Viewmodel.SetPuroseList();
            InitialPageSet();
            DataContext = Viewmodel;
        }

        private void Countinue(object sender, RoutedEventArgs e)
        {
           
            MessageBoxResult result = MessageBox.Show(
                "Save Scores and Skills For Companion",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );
            if (result == MessageBoxResult.Yes)
            {
                human.Name = CompanionsName.Text;
                human.humanOrCon = 0;
                human.AssignScoresAndSkills(Viewmodel.Strength, Viewmodel.Speed, Viewmodel.Smarts, Viewmodel.Soical,
                    Viewmodel.Athletics, Viewmodel.Brawn, Viewmodel.Conditioning, Viewmodel.Intimidation, Viewmodel.Might,
                    Viewmodel.Acrobatics, Viewmodel.Driving, Viewmodel.Finesse, Viewmodel.Inflitration, Viewmodel.Inititave, Viewmodel.Targeting,
                Viewmodel.Alertness, Viewmodel.Culture, Viewmodel.Science, Viewmodel.Survival, Viewmodel.Technology,
                Viewmodel.AnimalHandling, Viewmodel.Deception, Viewmodel.Preformance, Viewmodel.Persuasion, Viewmodel.Streetwise);
                
                TFCharacterSession.CurrentTransfomer.companions.Add(human);
            }
            if(TFCharacterSession.CurrentTransfomer.Role.Name == "Mode Master" && TFCharacterSession.CurrentTransfomer.hasSpentGeneralPerkPoints == false)
            {
                
                if (SecondPurpose.IsChecked == true) // basically checks to see if this should be purpose 1 or 2
                {

                    human.purposeTwo = MiniconPurpose1;
                    human.AssignPurpsoe(human.purposeTwo, human.Name);
                }
                else
                {
                    human.purpose = MiniconPurpose1;
                    human.AssignPurpsoe(human.purpose, human.Name);
                }
                
    
                if (TFCharacterSession.CurrentTransfomer.Role.Name == "Mode Master" && TFCharacterSession.CurrentTransfomer.companions[0] != null && 
                    TFCharacterSession.CurrentTransfomer.CurrentLevel >= 10 && TFCharacterSession.CurrentTransfomer.companions.Count  <=2)
                {
                    MessageBox.Show("Loading page again. At 10th level Mode Master, you may choose to make a new companion or add a purpose to your companion");
                    NavigationService.Navigate(new HumanCompanion());
                    return;
                }
                NavigationService.Navigate(new GeneralPerksTF());
            }
            GernalPerkNavMethod.GoToNextPerk(NavigationService, PerkBeingApplied.HC);
        }


        private void InitialPageSet()
        {
            if (TFCharacterSession.CurrentTransfomer.Role.Name == "Mode Master")
            {
                ModeMasterBit.Visibility = Visibility.Visible;
                if(TFCharacterSession.CurrentTransfomer.companions.Count() >= 1)
                {
                    MiniconSettingsAfter10.Visibility = Visibility.Visible;
                }
            }
            
        }

        private void SetModeMasterVisibitlity(object sender, RoutedEventArgs e)
        {
      
            if( SecondPurpose.IsChecked == true)
            {
                ModeMasterBit.Visibility = Visibility.Visible;
                ScoreView.Visibility = Visibility.Hidden;
                CompanionsName.Visibility = Visibility.Hidden;
                ExistingMinicon.Visibility = Visibility.Visible;


            }
            else if(SecondCon.IsChecked == true)
            {
                ModeMasterBit.Visibility = Visibility.Visible;
                ScoreView.Visibility = Visibility.Visible;
                ExistingMinicon.Visibility = Visibility.Hidden;
                CompanionsName.Visibility = Visibility.Visible;
            }
            
        }

        
        private void SetPurpose(object sender, RoutedEventArgs e)
        {
            var target = (RadioButton)sender;
            SkillTF selectedPurpose = (SkillTF)target.DataContext;
            pet minicon = TFCharacterSession.CurrentTransfomer.companions.FirstOrDefault(x => x.Name == CompanionsName.Text);
            MiniconPurpose1 = selectedPurpose;

           
            
       
            
        }

        
    }
}
