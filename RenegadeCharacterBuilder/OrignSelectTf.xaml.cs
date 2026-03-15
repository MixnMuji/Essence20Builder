using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
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
using RenegadeCharacterBuilder.Models.Transformers.Roots;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for OrignSelectTf.xaml
    /// </summary>
    public partial class OrignSelectTf : Page
    {
        public List<TransformersOrign> tfOrgins { get; set; }
        public OrignSelectTf()
        {
            InitializeComponent();
            LoadOrigins();
            MessageBox.Show(tfOrgins.Count.ToString());
            DataContext = this;
        }
        void LoadOrigins()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "Origins.json");
            string json = File.ReadAllText(path);
            var originRoot = JsonSerializer.Deserialize<TFOriginsRoot>(json);
            tfOrgins = originRoot.Origins;
        }
    }
}
