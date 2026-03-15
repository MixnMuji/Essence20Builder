using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
using RenegadeCharacterBuilder.Models;
using RenegadeCharacterBuilder.Models.Transformers;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for ExpansionsTransfomersSelect.xaml
    /// </summary>
    public partial class ExpansionsTransfomersSelect : Page
    {
        public ExpansionsTransfomersSelect()
        {
            InitializeComponent();
            if(TFCharacterSession.CurrentTransfomer == null)
            {
                TFCharacterSession.CurrentTransfomer = new TransfomersCharacterModel();
             
            }

        }

        private void Countinue_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrignSelectTf());
        }
       
    }
}
