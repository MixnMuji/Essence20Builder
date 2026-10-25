using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for HybridizationPage.xaml
    /// </summary>
    public partial class HybridizationPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> HybridOptions { get; set;}
        public List<string> ChosenOptions { get; set; }

        private int _hybridPointBank { get; set; }
        public int HybridPointBank
        {
            get => _hybridPointBank;
            set
            {
                _hybridPointBank = value;
                NotifyPropertyChanged(nameof(HybridPointBank));
            }
        }
        public HybridizationPage()
        {
            InitializeComponent();
            SetPointBankValue();
            DataContext = this;
            ChosenOptions = new List<string>();
            HybridOptions = new ObservableCollection<string>
            {
                { "Change Sie: Temporariliy increase or decrease your Size by one full Size Class" },
                {"Evasive Conversion: When an enemy attacks you, you can imediately Convert to give the enemy a Snag on their attack." },
                {"Extra Shift: You Gain an additonal use of Mass Shift per day. You can take this Hybridization mutiple times, gaining an additonal use of Mass Shift per day each time" },
                { "Fast shift, You use Mass Shift as a Free action instead of a move action"},
                {"Half Track: Gain the Movement of your Alt Mode when in Bot mode" },
                {"Helping Hand: ignore your Origin's Limited Articulation drawback" },
                {"Hold that Shape: you gain the benefits of Mass shift until you use Mass Shift again or until you rest, instead of until the end of the scene." },
                {"Reinforce Shell: Gain the benefits of an Armor Upgrade you are trained in and currently do not have equipped" },
                {"Replacement Part: Repair 1 Damage" },
                {"Steady Hands: You do not suffer a Snag on unskilled Skill Tests" },
                {"Weaponize:Add A weapon which you are trained in to an Exteranal HardPoint" }
                
            };
            if(HybridPointBank == 0)
            {
                HybridPointBank +=1;
            }
        }
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void SetPointBankValue()
        {
            int cL = TFCharacterSession.CurrentTransfomer.CurrentLevel;

            switch (cL)
            {
                case   >2 and <7:
                    HybridPointBank = 1;
                    break;
                case >7 and  <12 :
                    HybridPointBank = 2;
                    break;
                case >12 and <14:
                    HybridPointBank = 3;
                break;
                case >14 and <18:
                    HybridPointBank = 4;
                    break;
                case >= 18:
                    HybridPointBank = 5;
                    break;
            }
        }

  
        private void ChecklegalityOfchoice(object sender, RoutedEventArgs e)
        {
            CheckBox currentbox = sender as CheckBox;
            string pulledData = currentbox.DataContext as string;

            if(currentbox.IsChecked == false && ChosenOptions.Contains(pulledData)) // basically if the checked item is in chosen remove it and refund the point
            {
                HybridPointBank += 1;
                ChosenOptions.Remove(pulledData);
                return;
            }

            if(HybridPointBank == 0 && currentbox.IsChecked== true)
            {
                MessageBox.Show("You have no More points to spend");
                currentbox.IsChecked = false;
                return;
            }
            else
            {
                HybridPointBank -= 1;
                ChosenOptions.Add(pulledData);
                return;
            }

        }

        private void SetdataAndProceed(object sender, RoutedEventArgs e)
        {
            foreach(string d in ChosenOptions)
            {
                TFCharacterSession.CurrentTransfomer.MiscilaniousData.Add(d);
                NavigationService.Navigate(new EquipmentPageTF());
            }
        }
    }
}
