using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Security;
using System.Runtime.CompilerServices;
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
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for SCperkpage.xaml
    /// </summary>
    public partial class SCperkpage : Page 
    {
       public SCVM vm { get; set; }
        
        
        
        public SCperkpage()
        {
            vm = new SCVM();
            InitializeComponent();
            DataContext = vm;
        }

        private void MoveOn(object sender, RoutedEventArgs e)
        {
            if(vm.Selection == null || vm.ChosenSize == null)
            {
                MessageBox.Show("You have to select an almode and new size before moving on");
            }
            var target = TFCharacterSession.CurrentTransfomer.Origns.FirstOrDefault(o => o.AltMode == vm.Selection);
            target.AltMode.Size = vm.ChosenSize;
            GernalPerkNavMethod.GoToNextPerk(NavigationService, PerkBeingApplied.SC);
        }
    }
}
