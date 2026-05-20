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
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;

namespace RenegadeCharacterBuilder
{

    /// <summary>
    /// Interaction logic for FocusPerks.xaml
    /// </summary>
    public partial class FocusPerks : Page
    {
        public FocusPageVMTF viewmodel{ get; }
        public FocusPerks()
        {
            InitializeComponent();
            viewmodel = new FocusPageVMTF();
            //viewmodel.GetSubClass(TFCharacterSession.CurrentTransfomer.Role.Name);
            viewmodel.GetSubClass();
            DataContext = viewmodel;
            
        }
    }
}
