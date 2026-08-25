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
    /// Interaction logic for EquipmentPageTF.xaml
    /// </summary>
    public partial class EquipmentPageTF : Page
    {
        public EquimpmentPageVMTF vm { get; set; }
        public EquipmentPageTF()
        {
            InitializeComponent();
            vm = new EquimpmentPageVMTF();
            DataContext = vm;
           
        }
    }
}
