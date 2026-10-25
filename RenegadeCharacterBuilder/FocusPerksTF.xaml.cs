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
                if (TFCharacterSession.CurrentTransfomer.Role.Name == "Mode Master")
                {
                    CheckForNullsBeforeMovingOn();
                    TFCharacterSession.CurrentTransfomer.scoreBoostFromSubclss = viewmodel.ChosenModeMasterEssence;
                    TFCharacterSession.CurrentTransfomer.skillBoostFromSubclass = viewmodel.ChosenModeMasterSkill;
                }
                NavigationService.Navigate(new ScoreSkillAllocationTF());
            }
            else
                return;
               
            }

            
               
            
       

        private void CheckForNullsBeforeMovingOn()
        {
            if(viewmodel.ChosenModeMasterEssence == null || viewmodel.ChosenModeMasterEssence == "") {
                MessageBox.Show("Can not proceed without picking an essacne score to boost");
                return;
            }
            if (viewmodel.ChosenModeMasterSkill == null || viewmodel.ChosenModeMasterSkill == "")
            {
                MessageBox.Show("Can not proceed without picking a skill score to boost");
                return;
            }
        }
        private void SkllChosen(object sender, RoutedEventArgs e)
        {
            RadioButton choice = (RadioButton)sender;
            TFCharacterSession.CurrentTransfomer.ChosenLinkedSkill = choice.Content?.ToString();
        }
    }
}
