using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ScoreSkillAllocationTF : Page, INotifyPropertyChanged
    {
       
        public ScoreSkillAllocationTF()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public  void AddToScore(object sender, RoutedEventArgs e)
        {
            // logic that binds button to skill
            var btn = sender as Button;

            string? stat = btn.Tag.ToString();
            if(stat == null || PointsBank <= 0)
            {
                MessageBox.Show("No more points to spend");
                return;
            }
            switch (stat)
            {
                case "Strength":
                    Strength += 1;
                    PointsBank -= 1;
                    break;
                case "Speed":
                    Speed += 1;
                    PointsBank -= 1;
                    break;
                case "Smarts":
                    Smarts += 1;
                    PointsBank -= 1;
                    break;
                case "Social":
                    Social += 1;
                    PointsBank -= 1;
                    break;
            }
            
        }
        public  void SubtracFromoScore(object sender, RoutedEventArgs e)
        {
            // logic that binds button to skill
            var btn = sender as Button;

            string? stat = btn.Tag.ToString();
            if (stat == null)
            {
                MessageBox.Show("Null exception");
                return;
            }
            switch (stat)
            {
                case "Strength":
                    if (Strength == 1)
                    {
                        MessageBox.Show("Stat must be equal to at least 1");
                        break;
                    }
                    else
                    {
                        Strength -= 1;
                        PointsBank += 1;
                        break;
                    }
                case "Speed":
                    if (Speed == 1)
                    {
                        MessageBox.Show("Stat must be equal to at least 1");
                        break;
                    }
                    else
                    {
                        Speed -= 1;
                        PointsBank += 1;
                        break;
                    }
                case "Smarts":
                    if (Smarts == 1)
                    {
                        MessageBox.Show("Stat must be equal to at least 1");
                        break;
                    }
                    else
                    {
                        Smarts -= 1;
                        PointsBank += 1;
                        break;
                    }
                case "Social":
                    if (Social == 1)
                    {
                        MessageBox.Show("Stat must be equal to at least 1");
                        break;
                    }
                    else
                    {
                        Social -= 1;
                        PointsBank += 1;
                        break;
                    }
            }

        }
   
    }
}
