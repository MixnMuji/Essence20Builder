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
using System.Xml.Linq;

namespace RenegadeCharacterBuilder
{
    public enum GameSelected
    {
        Transfomers = 0,
        PowerRangers =1,
        GiJoe =2,
        Mlp = 3

        //remeber in check to cut whitspace from string
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ComboBox1.ItemsSource = Enum.GetValues(typeof(GameSelected));
        }

        private void proceed_Click(object sender, RoutedEventArgs e)
        {
            // run switch cases to determine slection and then route to next page!s
        }
    }
}