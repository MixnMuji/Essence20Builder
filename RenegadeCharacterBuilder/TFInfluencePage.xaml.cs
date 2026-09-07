using System;
using System.Collections.Generic;
using System.ComponentModel;
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
   
    public partial class TFInfluencePage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<InfluencesTF> tfInfluences{get; set;}
        public List<string> InfluenceNames { get; set; } = new();
        public List<HangUps> tfHangups { get; set; }
        public List<string> HangUpNames { get; set; } = new();

        private string _viewSetter;
        public string ViewSetter
        {
            get => _viewSetter;
            set
            {
                _viewSetter = value;
                NotifyPropertyChanged(nameof(ViewSetter));
                FindNewDisplay();
            }
        }

        private HangUps _displayedHangup;
        public HangUps DisplayedHangup
        {
            get => _displayedHangup;
            set
            {
                _displayedHangup = value;
                NotifyPropertyChanged(nameof(DisplayedHangup));
            }
        }

        private InfluencesTF _displayedInfluence;
        private InfluencesTF DisplayedInfluence
        {
            get => _displayedInfluence;
            set
            {
                _displayedInfluence = value;
                NotifyPropertyChanged(nameof(DisplayedInfluence));
            }
        }
        public TFInfluencePage()
        {
            InitializeComponent();
            if (TFCharacterSession.CurrentTransfomer.Origns == null)
            {
                MessageBox.Show("Origin binding failed");
            }
            LoadTFInfluneces();
            LoadTFHangUps();
            setnameData();
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
        public void setnameData()
        {
            foreach(var x in tfInfluences)
            {
                InfluenceNames.Add(x.Name);
            }
            foreach(var y in tfHangups)
            {
                HangUpNames.Add(y.Name);
            }
           
        }
        public void FindNewDisplay()
        {
            if (InfluenceNames.Contains(ViewSetter))
            {
                DisplayedInfluence = tfInfluences.FirstOrDefault(x => x.Name == ViewSetter);
            }
            else if (HangUpNames.Contains(ViewSetter))
            {
                DisplayedHangup = tfHangups.FirstOrDefault(x => x.Name == ViewSetter);
            }
            else
                MessageBox.Show("Invalid data, contact dev with error");
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
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
