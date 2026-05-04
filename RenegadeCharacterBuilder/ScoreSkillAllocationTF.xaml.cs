using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;
using static System.Net.Mime.MediaTypeNames;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    public partial class ScoreSkillAllocationTF : Page
    {
        public CharScorePageModelTF Viewmodel { get; }
        public ScoreSkillAllocationTF()
        {
            InitializeComponent();
            Viewmodel = new CharScorePageModelTF();
            Viewmodel.findRoleForStats();
           DataContext = Viewmodel;
            MessageBox.Show(TFCharacterSession.CurrentTransfomer.Role.Name);

        }

        private void SaveScoresAndSkillsAndProcced(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Save Scores and Skills",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );
            if( result == MessageBoxResult.Yes)
            {
                TFCharacterSession.CurrentTransfomer.AssignScoresAndSkills(Viewmodel.Strength, Viewmodel.Speed, Viewmodel.Smarts, Viewmodel.Soical,
                    Viewmodel.Athletics, Viewmodel.Brawn, Viewmodel.Conditioning, Viewmodel.Intimidation, Viewmodel.Might,
                    Viewmodel.Acrobatics, Viewmodel.Driving, Viewmodel.Finesse, Viewmodel.Inflitration, Viewmodel.Inititave, Viewmodel.Targeting,
                Viewmodel.Alertness, Viewmodel.Culture, Viewmodel.Science, Viewmodel.Survival, Viewmodel.Technology,
                Viewmodel.AnimalHandling,Viewmodel.Deception,Viewmodel.Preformance, Viewmodel.Persuasion, Viewmodel.Streetwise);

                if(TFCharacterSession.CurrentTransfomer.Level.Level > 1)
                {
                    NavigationService.Navigate(new GeneralPerksTF());
                }
                else
                {
                    NavigationService.Navigate(new FinalPageAndConfirmation());
                }
                //go back to character start and have level set if it's greater than 3 have them go to general perks, also if they have Roles, let them go to focus
                //otherwise take to finalization page. Make finalization page!
            }

        }
    }
}
