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

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for LevelUpAfter1.xaml
    /// </summary>
    public partial class LevelUpAfter1 : Page
    {
        public LevelUpVM vm { get; set; }
        //we need the transformers current stats
        //we need a method to apply stat boost as well as add to a general perk score
        // can copy and past a lot of data from the skillscoreallocation VM
        // don't need to allocate scores only skills
        //xaml page will use 
        public LevelUpAfter1()
        {
            vm = new LevelUpVM();
            InitializeComponent();
            DataContext = vm;
        }

        private void MoveOn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FinalPageAndConfirmation());
        }
    }
}
