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
    /// Interaction logic for FinalPageAndConfirmation.xaml
    /// </summary>
    public partial class FinalPageAndConfirmation : Page
    {
        public FinalPageVM vm { get; set; }
        public FinalPageAndConfirmation()
        {
            InitializeComponent();
            vm = new FinalPageVM();
        }
        public void RunvisbilityCheck() // fire this on button pushes
        {
            switch (vm.CurrentIndex) // copy data but instead make it so that it sets the visibilty to collapsiable
            {
                case 0:
                return;
                case 1:
                    return;
                case 2:
                    return;

            }
        }
    }
}
