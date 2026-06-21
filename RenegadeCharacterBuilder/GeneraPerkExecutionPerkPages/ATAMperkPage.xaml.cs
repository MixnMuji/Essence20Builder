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
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.Enums;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.TFServices;

using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for ATAMperkPage.xaml
    /// </summary>
    public partial class ATAMperkPage : Page
    {
        public ATAMVMTF vm { get; set; }
        public TFGeneralPerkService gpService { get; set; }
      
        public ATAMperkPage()
        {
            InitializeComponent();
             vm = new ATAMVMTF();
             DataContext = vm;
        }

        private void applyAndContinue(object sender, RoutedEventArgs e)
        {

            string movementchoice = moveSelected.SelectedItem as string;
            if (movementchoice == null)
            {
                MessageBox.Show("Please select a movement type.");
                return;
            }
            foreach (TransformersOrign mode in TFCharacterSession.CurrentTransfomer.Origns)
            {
                if (vm.SelectedAltMode == mode)
                {
                    mode.AltMode.Type2 = movementchoice;
                    mode.AltMode.Movement2 = 20;
                }
            }
            GernalPerkNavMethod.GoToNextPerk(NavigationService, PerkBeingApplied.ATAM);

        }
       
        
    }
}
