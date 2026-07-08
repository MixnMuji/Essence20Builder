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
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for FinalPageAndConfirmation.xaml
    /// </summary>
    public partial class FinalPageAndConfirmation : Page
    {
        public FinalpageViewHelper vm { get; set; }
        public FinalPageAndConfirmation()
        {
            vm = new FinalpageViewHelper();
            InitializeComponent();
            DataContext = vm;
            RunvisbilityCheck();

        }
        public void RunvisbilityCheck() // fire this on button pushes
        {
            switch (vm.CurrentIndex) // copy data but instead make it so that it sets the visibilty to collapsiable
            {
                case 0:
                    MessageBox.Show(vm.CurrentIndex.ToString());
                    view1.Visibility = Visibility.Visible;
                    view2.Visibility = Visibility.Collapsed;
                    view3.Visibility = Visibility.Collapsed;
                return;
                case 1:
                    MessageBox.Show(vm.CurrentIndex.ToString());
                    view1.Visibility = Visibility.Collapsed;
                    view2.Visibility = Visibility.Visible;
                    view3.Visibility = Visibility.Collapsed;
                    return;
                case 2:
                    MessageBox.Show(vm.CurrentIndex.ToString());
                    view1.Visibility = Visibility.Collapsed;
                    view2.Visibility = Visibility.Collapsed;
                    view3.Visibility = Visibility.Visible;
                    return;

            }
        }

        private void Moveup(object sender, RoutedEventArgs e)
        {
            vm.Nextpage();
            RunvisbilityCheck();
        }

        private void MoveBack(object sender, RoutedEventArgs e)
        {
            vm.PreviousPage();
            RunvisbilityCheck();
        }
    }
}
