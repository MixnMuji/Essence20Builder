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

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for ScoreSkillAllocationTF.xaml
    /// </summary>
    public partial class ScoreSkillAllocationTF : Page
    {
        public int pointBank = 9;
        public int SocialSkilbank;
        public int StrengthSkilbank;
        public int SpeedlSkilbank;
        public int SmartslSkilbank;

        //for binding
        public int Strength { get; set;} = 0;
        public int Smarts { get; set; } = 0;
        public int Speed { get; set; } = 0;
        public int Social { get; set; }  = 0;

        public ScoreSkillAllocationTF()
        {
            InitializeComponent();
            DataContext = this;
        }
        public  void AddToScore(object sender, RoutedEventArgs e)
        {
            // logic that binds button to skill
            pointBank -= 1;
        }
        public  void SubtracFromoScore(object sender, RoutedEventArgs e)
        {
            // logic that binds button to skill
            pointBank += 1;

        }
        public  void AddToSkill(object sender, RoutedEventArgs e)
        {
            //logic which checks the skills corresponding stat
            // if current points in skill > score return message box show. "Cannot have more points in skill that base essance score"
        }
        public  void SubtracFromSkill(object sender, RoutedEventArgs e)
        {

        }
    }
}
