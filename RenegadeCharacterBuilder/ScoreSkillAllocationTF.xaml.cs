using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers;
using static System.Net.Mime.MediaTypeNames;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    public partial class ScoreSkillAllocationTF : Page
    {

        private Popup? dragPopup;
        private bool feedbackMessageShown = false;

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

                if(TFCharacterSession.CurrentTransfomer.CurrentLevel >= 4)
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

      
        private void DragStat(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            FrameworkElement element = (FrameworkElement)sender;
            ScoreTF score = (ScoreTF)element.DataContext;
            
                DragDrop.DoDragDrop(
                    element,
                    score,
                    DragDropEffects.Move
                );
           

        }

   
        private void SetScore(object sender, DragEventArgs e)
        {
            ScoreTF score = (ScoreTF)e.Data.GetData(typeof(ScoreTF));
            TextBlock target = (TextBlock)sender;
            if(target == Stat1)
            {
                Stat1.Text = score.Name;
                CheckforDubplicateStats(target, Stat2, Stat3, Stat4);
            }
            else if(target == Stat2)
            {
                Stat2.Text = score.Name;
                CheckforDubplicateStats(target, Stat1, Stat3, Stat4);
            }
            else if(target == Stat3)
            {
                Stat3.Text = score.Name;
                CheckforDubplicateStats(target, Stat1, Stat2, Stat4);
            }
            else if(target == Stat4)
            {
                Stat4.Text = score.Name;
                CheckforDubplicateStats(target, Stat1, Stat2, Stat3);
            }
        }

        private void stat_dragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ScoreTF)))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        public void CheckforDubplicateStats(TextBlock Current, TextBlock one, TextBlock two, TextBlock three)
        {
            if(Current.Text == one.Text)
            {
                one.Text = string.Empty;
            }
            if(Current.Text == two.Text)
            {
                two.Text = string.Empty;
            }
            if (Current.Text == three.Text)
            {
                three.Text = string.Empty;
            }
        }
        private void Doupdate(object sender, RoutedEventArgs e)
        {
            RadioButton choice = (RadioButton)sender;
            //SkillTF toupdate = choice.Content;

            Viewmodel.SkillBoostFromOrigin = (SkillTF)choice.DataContext;
        }
    }
}
