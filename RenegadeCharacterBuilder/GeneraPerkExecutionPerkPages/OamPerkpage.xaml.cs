using System;
using System.Collections.Generic;
using System.Net.Mail;
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

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for OamPerkpage.xaml
    /// </summary>
    public partial class OamPerkpage : Page
    {
        public List<Altmode> choics { get; set; }

        public Altmode selection { get; set; }
        public OamPerkpage()
        {
            ReturnLeagalAltmodes();
            InitializeComponent();
            DataContext = this;
        }

        public void ReturnLeagalAltmodes()
        {
            string[] botSizes = ["Common", "Long", "Huge"];
            string[] altSizes = ["Common", "Large", "Extended"];

            int limitIndex = 0;
            
            foreach(string size in botSizes)
            {
                if (TFCharacterSession.CurrentTransfomer.Origns[0].BotMode.Size == size)
                {
                    break;
                }
                else
                    limitIndex++;
                
            }
            Array.Resize<string>(ref altSizes, limitIndex); // so now that we have our limit number we can just check our new array to see if it contains the altmode size

            choics = TFCharacterSession.CurrentTransfomer.Altmodes.Where(a => altSizes.Contains(a.Size)).ToList();
            // needs to be within two size classes of your bot mode
            // botmodes and altmodes have different size classes but basically the same tiers
            // get size of altmode, and if it is =< array[botmode.size], include it as our list
            // List<Altmode> legalchoices = TFCharacterSession.CurrentTransfomer.Origns.
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            if(selection == null)
            {
                MessageBox.Show("You must choose an altmode to change into an object");
                return;
            }
            Altmode target = TFCharacterSession.CurrentTransfomer.Altmodes.FirstOrDefault(a => a == selection);
            target.Movement = 0;
            target.Attack = "gain an edge on skill test including, hiding, blending in, or easdropping";
            GernalPerkNavMethod.GoToNextPerk(NavigationService, PerkBeingApplied.OAM);
        }
    }
}
