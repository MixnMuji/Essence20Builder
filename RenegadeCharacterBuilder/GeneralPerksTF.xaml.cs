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
    /// Interaction logic for GeneralPerksTF.xaml
    /// </summary>
    public partial class GeneralPerksTF : Page
    {
        public GeneralPerksVMTF viewmodel { get; set; }
        public GeneralPerksTF()
        {
            InitializeComponent();
            viewmodel = new GeneralPerksVMTF();
            viewmodel.GetApplicablePerks(TFCharacterSession.CurrentTransfomer.CurrentLevel, TFCharacterSession.CurrentTransfomer.fullSkillList, TFCharacterSession.CurrentTransfomer.fullScoreList);
            DataContext = viewmodel;
            
        }


    }
}
