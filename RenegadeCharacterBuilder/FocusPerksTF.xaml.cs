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
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

namespace RenegadeCharacterBuilder
{

    /// <summary>
    /// Interaction logic for FocusPerks.xaml
    /// </summary>
    public partial class FocusPerks : Page
    {
        public FocusPageVMTF viewmodel{ get; }
        public FocusPerks()
        {
            InitializeComponent();
            viewmodel = new FocusPageVMTF();
            //viewmodel.GetSubClass(TFCharacterSession.CurrentTransfomer.Role.Name);
            viewmodel.GetSubClass();
            DataContext = viewmodel;
            
        }

        private void SetSubclassandContinue(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TFCharacterSession.CurrentTransfomer.ChosenLinkedSkill))
            {
                MessageBox.Show("Select a skill to raise every time you raise score outlined in your Focus.");
                return;
            } 
            
            
            MessageBoxResult result = MessageBox.Show(
                "Select Subclasss and Continue",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question

                );
            if (result == MessageBoxResult.Yes)
            {
                
                TFCharacterSession.CurrentTransfomer.sub = viewmodel.CurrentSubclass;
                if (TFCharacterSession.CurrentTransfomer.CurrentLevel >= 4)
                {
                    NavigationService.Navigate(new GeneralPerksTF());
                }
                NavigationService.Navigate(new ScoreSkillAllocationTF());
            }
            else
                return;
        }

        private void SkllChosen(object sender, RoutedEventArgs e)
        {
            RadioButton choice = (RadioButton)sender;
            TFCharacterSession.CurrentTransfomer.ChosenLinkedSkill = choice.Content?.ToString();
        }
    }
}
