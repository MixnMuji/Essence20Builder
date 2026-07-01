using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Security;
using System.Runtime.CompilerServices;
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

namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for SCperkpage.xaml
    /// </summary>
    public partial class SCperkpage : Page 
    {
       public SCVM vm { get; set; }
        
        
        
        public SCperkpage()
        {
            vm = new SCVM();
            InitializeComponent();
            DataContext = vm;
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                MessageBoxResult.Yes:
                
                MessageBoxResult.No)

        }
    }
}
