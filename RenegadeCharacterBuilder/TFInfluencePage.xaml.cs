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
    /// Interaction logic for TFInfluencePage.xaml
    /// </summary>
   
    public partial class TFInfluencePage : Page
    {
        public List<InfluencesTF> tfInfluences{get; set;}
        public List<HangUps> tfHangups { get; set; }
        public TFInfluencePage()
        {
            InitializeComponent();
            LoadTFInfluneces();
            LoadTFHangUps();
            DataContext = this;
        }
        void LoadTFInfluneces()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "InfluencesTF.json");
            string influencesJson = File.ReadAllText(path);
            var influencesRoot = JsonSerializer.Deserialize<TFInfluencesRoot>(influencesJson);
            //MessageBox.Show(influencesRoot == null ? "Root NULL" : "Root OK");

        
               
            tfInfluences = influencesRoot.Influences;
       
        }
        void LoadTFHangUps()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "HangUpsTF.json");
            string hangUpsJson = File.ReadAllText(path);
            var hangupsRoot = JsonSerializer.Deserialize<TFHangUpsRoot>(hangUpsJson);
            //MessageBox.Show(hangupsRoot == null ? "Hangups Null" : "hangups ok");

            tfHangups = hangupsRoot.Hang_ups;
        }

    }
}
