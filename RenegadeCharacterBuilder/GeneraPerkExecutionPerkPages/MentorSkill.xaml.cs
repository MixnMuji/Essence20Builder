using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.Enums;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for MentorSkill.xaml
    /// </summary>
    public partial class MentorSkill : Page
    {
        public string[] Skills { get; set; } =
            {
            "Athletics",
            "Brawn",
            "Conditioning",
            "Intimidation",
            "Might",
            "Acrobatics",
            "Driving",
            "Finesse",
            "Inflitration",
            "Inititave",
            "Targeting",
            "Alertness",
            "Culture",
            "Science",
            "Survival",
            "Technology",
            "Animal Handling",
            "Deception",
            "Preformance",
            "Persuasion",
            "Streetwise"
        };
        public string[] Scores { get; set; } = { "Strength", "Speed", "Smarts", "Social" };
        public MentorSkill()
        {
            InitializeComponent();
            DataContext = this;
            MessageBox.Show($"{Skills.Length}");
        }

        private void Confrim(object sender, RoutedEventArgs e)
        {
            if(selectedSkill.SelectedItem == null || selectedScore.SelectedItem == null)
            {
                MessageBox.Show("You must select a skill and score before continuing");
                return;
            }
            string findSkill = selectedSkill.SelectedItem.ToString();
            string findScore = selectedScore.SelectedItem.ToString();

            SkillTF targetSkill = TFCharacterSession.CurrentTransfomer.fullSkillList.FirstOrDefault(s => s.Name == findSkill);
            targetSkill.isMentorSkill = true;

            

            ScoreTF targetScore = TFCharacterSession.CurrentTransfomer.fullScoreList.FirstOrDefault(s => s.Name == findScore);
            targetScore.isMentorScore = true;

            //need a way to apply bonuses
            //need second advancement page for characters past level 1
            GernalPerkNavMethod.GoToNextPerk(NavigationService, PerkBeingApplied.Mentor);
        }
    }
}
