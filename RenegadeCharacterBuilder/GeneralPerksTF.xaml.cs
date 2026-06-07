using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using RenegadeCharacterBuilder.Models.Transformers.TFServices;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for GeneralPerksTF.xaml
    /// </summary>
    public partial class GeneralPerksTF : Page
    {
        public TFGeneralPerkService gpService { get; set; }
        public GeneralPerksVMTF viewmodel { get; set; }
        public GeneralPerksTF()
        {
            InitializeComponent();
            viewmodel = new GeneralPerksVMTF();
            viewmodel.GetApplicablePerks(TFCharacterSession.CurrentTransfomer.CurrentLevel, TFCharacterSession.CurrentTransfomer.fullSkillList, TFCharacterSession.CurrentTransfomer.fullScoreList);
            DataContext = viewmodel;
            
        }

        private void ContinueAndGetApplyPerk(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("clicked");
            gpService = new TFGeneralPerkService();
            var pickedPerks = viewmodel._qualifyingPerks.Where(p => p.isSelected == true);
            MessageBox.Show(
    viewmodel._qualifyingPerks.Count(p => p.isSelected).ToString()
);
            foreach (var perk in viewmodel._qualifyingPerks)
            {
                Debug.WriteLine($"{perk.Name} : {perk.isSelected}");
            }
            foreach (var perk in pickedPerks)
            {
                if (perk.abreviationForExecute!=null) // basically we're catching the perks with an executer
                {
                    TFCharacterSession.CurrentTransfomer.GeneralPerkTextBlock.Add(perk.Text); // we add the perk's text
                    NavigationService.Navigate(new GenericPerkApplyerpage(perk.abreviationForExecute?? 0));// then it will make a new page where
                }
                gpService.ApplyPerk(TFCharacterSession.CurrentTransfomer, perk);// shouldapply perks now
                NavigationService.Navigate(new FinalPageAndConfirmation());
            }
     
        }
    }
}
