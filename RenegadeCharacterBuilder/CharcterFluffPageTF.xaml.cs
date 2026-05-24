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
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for CharcterFluffPageTF.xaml
    /// </summary>
    public partial class CharcterFluffPageTF : Page
    {
        public CharcterFluffPageTF()
        {
            InitializeComponent();
            ComboBoxLevels.ItemsSource = GetLevels();
        }
        public List<int> GetLevels()
        {
            return Enumerable.Range(1, 20).ToList();
        }

        private void SaveChangesAndCountinue(object sender, RoutedEventArgs e)
        {

            TFCharacterSession.CurrentTransfomer.Name = CharactersName.Text;
            TFCharacterSession.CurrentTransfomer.Pronouns = CharacterPronouns.Text;
            TFCharacterSession.CurrentTransfomer.Description = CharacterDescription.Text;

            if (ComboBoxLevels.SelectedItem is int levelVal)
            {
                TFCharacterSession.CurrentTransfomer.CurrentLevel = levelVal;
            }
            NavigationService.Navigate(new OrignSelectTf());

        }

        private void FactionChoice(object sender, RoutedEventArgs e)
        {
            RadioButton choice = sender as RadioButton;
            if(choice.Name == "Decepticon")
            {
                TFCharacterSession.CurrentTransfomer.Faction = Alliegence.Descepticon;
            }
            if(choice.Name == "Autobot")
            {
                TFCharacterSession.CurrentTransfomer.Faction = Alliegence.Autobot;
            }
            
        }
    }
}
