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
        public event PropertyChangedEventHandler PropertyChanged;
        private int _pointBank = 9;
        public int SocialSkilbank;
        public int StrengthSkilbank;
        public int SpeedlSkilbank;
        public int SmartslSkilbank;

        //for binding

        
        private int _strength =1;
        private int _speed = 1;
        private int _smarts= 1;
        private int _social= 1;

        public int PointsBank
        {
            get => _pointBank;
            set
            {
                _pointBank = value;
                OnPropertyChanged(nameof(PointsBank));
            }
        }
        public int Strength {
            get => _strength; 
                set { _strength = value;
                OnPropertyChanged(nameof(Strength));
                    } 
        } 
        public int Smarts
        {
            get => _smarts;
            set { _smarts = value;
                OnPropertyChanged(nameof(Smarts));

            }
        }
        public int Speed 
                { get => _speed;
            set { _speed = value;
                OnPropertyChanged(nameof(Speed));
            } 
        }
        public int Social
        {
            get => _social;
            set
            {
                _social = value;
                OnPropertyChanged(nameof(Social));
            }
        }

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
