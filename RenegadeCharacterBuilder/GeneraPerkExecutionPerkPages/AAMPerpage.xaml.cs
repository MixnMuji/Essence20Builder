using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers.Enums;
using RenegadeCharacterBuilder.Models.Transformers.TFServices;

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for AAMPerpage.xaml
    /// </summary>
    public partial class AAMPerpage : Page
    {
  
        public Altmode newAltModeSelection { get; set; }
        public AAMPerpage()
        {
            var calldata = new GlobalCall();
            var firstlist = calldata.LoadJson<TFOriginsRoot>("Origins.json", "TransformersJsons");
            var filterout = TFCharacterSession.CurrentTransfomer.Origns.Select(o => o.Name);
            var filteredlist = firstlist.Origins.Where(o => !filterout.Contains(o.Name)).ToList();
            MessageBox.Show(filteredlist.Count.ToString());
            InitializeComponent();
            DataContext = filteredlist; // this should let our datacontext be the altmodes not taken
            newAltModeSelection = new Altmode();

        }

        private void setnewAltmode(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            var choice = rb.DataContext as TransformersOrign;
            newAltModeSelection = choice.AltMode;
        }

        private void figureOutWhatToDo(object sender, RoutedEventArgs e)
        {

            if (newAltModeSelection == null)
            {
                MessageBox.Show("You need to make a selection for your new altmode");
                return;
            }
            if (TFCharacterSession.CurrentTransfomer.Role.Name == "Mode Master" && TFCharacterSession.CurrentTransfomer.hasSpentGeneralPerkPoints == false)
            {
                
                TFCharacterSession.CurrentTransfomer.Altmodes.Add(newAltModeSelection);
                NavigationService.Navigate(new GeneralPerksTF());
                return;
            }
            
            // GernalPerkNavMethod.GoToNextPerk(NavigationService, PerkBeingApplied.AAM);
            NavigationService.Navigate(new LevelUpAfter1());


        }

        
    }
}
