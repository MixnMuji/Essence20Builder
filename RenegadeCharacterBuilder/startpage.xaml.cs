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

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for startpage.xaml
    /// </summary>
    public partial class startpage : Page
    {
        public startpage()
        {
            InitializeComponent();
            ComboBox1.ItemsSource = Enum.GetValues(typeof(GameSelected));
           
            
        }
        private void proceed_Click(object sender, RoutedEventArgs e)
        {
            if(ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("you must make a selection first");
                return;
            }
            GameSelected selected = (GameSelected)ComboBox1.SelectedItem;
            // run switch cases to determine slection and then route to next page!s
            
            switch (selected)
            {
                case GameSelected.Transfomers:
                    NavigationService.Navigate(new ExpansionsTransfomersSelect());
                    break;
                case GameSelected.PowerRangers:
                    MessageBox.Show("Power Rangers not implented yet");
                    break;
                case GameSelected.Mlp:
                    MessageBox.Show("My Little Pony not implented yet");
                    break;
                case GameSelected.GiJoe:
                    MessageBox.Show("Gi Joe not Implimented yet");
                    break;
                 
            }
        }
    }
}
