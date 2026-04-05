using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class CharScorePageModelTF: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _pointBank = 9;
        public int SocialSkilbank;
        public int StrengthSkilbank;
        public int SpeedlSkilbank;
        public int SmartslSkilbank;

        private int _strength = 1;
        private int _speed = 1;
        private int _smarts = 1;
        private int _social = 1;


        /*perhaps make a dictionary to show key scores and assoiated stats? that way if statments can check skills int value to essance score (if skillsun(all scores for related score) > Score){Messsage.box.Show
        "Skills can not have more total ranks than Corresponding essence score)
         */

        //Strength skills
        private int _athletics;
        private int _brawn;
        private int _conditioning;
        private int _intimidation;
        private int _might;

        //speed skills
        private int _acrobatics;
        private int _driving;
        private int _finnese;
        private int _infiltration;
        private int _initative;
        private int _targeting;

        //smarts skills
        private int _alertness;
        private int _culture;
        private int _science;
        private int _surviaval;
        private int _Technology;


        //socail skills
        private int _animalHandling;
        private int _deception;
        private int _preformance;
        private int _persuasion;
        private int _streetwise;
        public int PointsBank
        {
            get => _pointBank;
            set
            {
                _pointBank = value;
                OnPropertyChanged(nameof(PointsBank));
            }
        }
        public int Strength
        {
            get => _strength;
            set
            {
                _strength = value;
                OnPropertyChanged(nameof(Strength));
            }
        }
        public int Smarts
        {
            get => _smarts;
            set
            {
                _smarts = value;
                OnPropertyChanged(nameof(Smarts));

            }
        }
        public int Speed
        {
            get => _speed;
            set
            {
                _speed = value;
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
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void AddToScore(object sender, RoutedEventArgs e)
        {
            // logic that binds button to skill
            var btn = sender as Button;

            string? stat = btn.Tag.ToString();
            if (stat == null || PointsBank <= 0)
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
        public void SubtracFromoScore(object sender, RoutedEventArgs e)
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
        public void AddToSkill(object sender, RoutedEventArgs e)
        {
            //logic which checks the skills corresponding stat
            // if current points in skill > score return message box show. "Cannot have more points in skill that base essance score"
        }
        public void SubtracFromSkill(object sender, RoutedEventArgs e)
        {

        }
    }
}
