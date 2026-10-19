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

using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;
using static RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.EquimpmentPageVMTF;
using RenegadeCharacterBuilder.Models.Transformers.GearTFChildrenClasses;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers;
using System.Collections.ObjectModel;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for EquipmentPageTF.xaml
    /// </summary>
    public partial class EquipmentPageTF : Page
    {
        public EquimpmentPageVMTF vm { get; set; }
     
        public EquipmentPageTF()
        {
            InitializeComponent();
            CheckOrigin();
            vm = new EquimpmentPageVMTF();
            DataContext = vm;
            
        }

        private void EquipmentTabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is not TabControl tabControl)
                return;

            if (tabControl.SelectedItem is TabItem selectedTab)
            {
                vm.CurrentTab = selectedTab.Tag switch
                {
                    "Trained" => EquipmentTab.Trained,
                    "Untrained" => EquipmentTab.Untrained,
                    "Taken" => EquipmentTab.Taken,
                    _ => vm.CurrentTab
                };
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            if(sender is FrameworkElement element)
            {
                vm.AddEquuipment(element.DataContext);
            }
           
            
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                vm.removeitem(element.DataContext);
            }
        }

        private void MoveOn(object sender, RoutedEventArgs e)
        {
            TFCharacterSession.CurrentTransfomer.ArmorLoadout = vm.TakenArmorUpgrade;
           TFCharacterSession.CurrentTransfomer.KitsLoadout = vm.TakenKits.ToList();
           TFCharacterSession.CurrentTransfomer.WeaponsLoadOut = vm.TakenWeapons.ToList();
           TFCharacterSession.CurrentTransfomer.SupportEquipmentLoadOut= vm.TakenSupportEquipment.ToList();
            NavigationService.Navigate(new FinalPageAndConfirmation());
        }

        private void CheckOrigin()
        {
            if (TFCharacterSession.CurrentTransfomer.Origns[0].Name == "Support")
            {
                ifSupportOrign.Visibility = Visibility.Visible;
                return;
            }
        }

        private void SetRquestionPonts(object sender, RoutedEventArgs e)
        {
            string passedData = UsersRecVal.Text;
            int updatedReqValue;
            if(int.TryParse(passedData, out updatedReqValue))
            {
                vm.requistionPoints = updatedReqValue;
            }
            else
            {
                MessageBox.Show("Value in requisition box is not a number. Input a number or integer value");
                UsersRecVal.Text = "     ";
                return;
            }
        }
    }
}
