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
            MainFrame.Navigate(new MainWindow());
        }

        private void proceed_Click(object sender, RoutedEventArgs e)
        {
            GameSelected selected = (GameSelected)ComboBox1.SelectedItem;
            // run switch cases to determine slection and then route to next page!s
            switch (selected)
            {
                case GameSelected.Transfomers:
                    MainFrame.Navigate(new ExpansionsTransfomersSelect());
                    break;
                case GameSelected.PowerRangers:
                    MessageBox.Show("Power Rangers not implented yet");
                    break;
                case GameSelected.Mlp:
                    MessageBox.Show("My Little Pony not implented yet");
                    break;
                case GameSelected.GiJoe:
                    MessageBox.Show("Gi Joe not Implimented yet");
                    break;
            }
        }
    }
}