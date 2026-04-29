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
    /// Interaction logic for CharcterFluffPageTF.xaml
    /// </summary>
    public partial class CharcterFluffPageTF : Page
    {
        public CharcterFluffPageTF()
        {
            InitializeComponent();
            ComboBoxLevels.ItemsSource = GetLevels();
        }
        public List<int> GetLevels()
        {
            return Enumerable.Range(1, 20).ToList();
        }
    }
}
