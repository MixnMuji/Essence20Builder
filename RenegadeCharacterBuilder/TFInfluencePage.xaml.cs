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
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
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
            if (TFCharacterSession.CurrentTransfomer.Orign == null)
            {
                MessageBox.Show("Origin binding failed");
            }
            LoadTFInfluneces();
            LoadTFHangUps();
            DataContext = this;
        }
        public void LoadTFInfluneces()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "InfluencesTF.json");
            string influencesJson = File.ReadAllText(path);
            var influencesRoot = JsonSerializer.Deserialize<TFInfluencesRoot>(influencesJson);
            //MessageBox.Show(influencesRoot == null ? "Root NULL" : "Root OK");

        
               
            tfInfluences = influencesRoot.Influences;
       
        }
        public void LoadTFHangUps()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "HangUpsTF.json");
            string hangUpsJson = File.ReadAllText(path);
            var hangupsRoot = JsonSerializer.Deserialize<TFHangUpsRoot>(hangUpsJson);
            //MessageBox.Show(hangupsRoot == null ? "Hangups Null" : "hangups ok");

            tfHangups = hangupsRoot.Hang_ups;
        }

        private void ProceedToRoles(object sender, RoutedEventArgs e)
        {
            var slectedInfluences = tfInfluences.Where(i => i.isChecked).ToList();
            var slectedHangUps = tfHangups.Where(i => i.isChecked).ToList();

            int influenceCount = slectedInfluences.Count;
            int HangUpsCount = slectedHangUps.Count;

            if (influenceCount > 3)
            {
                MessageBox.Show("You may only have up to 3 influences");
                return;
            }
            if (HangUpsCount != influenceCount)
            {
                MessageBox.Show("Your Hang ups and influnece total must be equal");
                return;
            }
            TFCharacterSession.CurrentTransfomer.Influences = slectedInfluences;
            TFCharacterSession.CurrentTransfomer.Hang_Ups = slectedHangUps;

            if ((TFCharacterSession.CurrentTransfomer.Influences?.Count ?? 0) == 0 ||
                (TFCharacterSession.CurrentTransfomer.Hang_Ups?.Count ?? 0) == 0)
            {
                MessageBox.Show("You must select at least one influence and one hang up before proceeding");
                return;
            }

            NavigationService.Navigate(new RolesTF());
           
        }
    }
}
