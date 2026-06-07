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
using RenegadeCharacterBuilder.Models.Transformers.Enums;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for GenericPerkApplyerpage.xaml
    /// </summary>
    public partial class GenericPerkApplyerpage : Page
    {
        
        public GenericPerkApplyerpage(PerkBeingApplied perktype)
        {
            InitializeComponent();
            var vm = new PerkApplierVMTF();
            vm.GetDataForPerkExecution(perktype);
            DataContext = vm;
        }
    }
}
