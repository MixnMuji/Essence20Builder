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
        public HumanCompanion()
        {
            Viewmodel = new CharScorePageModelTF();
            InitializeComponent();
            DataContext = Viewmodel;
        }

        private void Countinue(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show(
                "Save Scores and Skills For Human Companion",
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
            GernalPerkNavMethod.GoToNextPerk(NavigationService, PerkBeingApplied.HC);
        }
    }
}
