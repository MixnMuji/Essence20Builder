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
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers;

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for CTperkpage.xaml
    /// </summary>
    public partial class CTperkpage : Page
    {
        public CTPerkATVM viewmodel { get; set; }
        public CTperkpage()
        {
            viewmodel = new CTPerkATVM();
            InitializeComponent();
            DataContext = viewmodel;

        }

        private void MoveOn(object sender, RoutedEventArgs e)
        {
            if(viewmodel.SelectedPerk == null)
            {
                MessageBox.Show("Choose a Perk Before moving on");
                return;
            }
            TFCharacterSession.CurrentTransfomer.miscellaneousPerks.Add(viewmodel.SelectedPerk);


        }
    }
}
