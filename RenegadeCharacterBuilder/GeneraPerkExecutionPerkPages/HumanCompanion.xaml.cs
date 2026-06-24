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

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for HumanCompanion.xaml
    /// </summary>
    public partial class HumanCompanion : Page
    {
        public CharScorePageModelTF viewmodel { get; set; }
        public HumanCompanion()
        {
            viewmodel = new CharScorePageModelTF();
            InitializeComponent();
            DataContext = viewmodel;
        }

        private void Countinue(object sender, RoutedEventArgs e)
        {

        }
    }
}
