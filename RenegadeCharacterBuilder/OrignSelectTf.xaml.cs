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
    /// Interaction logic for OrignSelectTf.xaml
    /// </summary>
    public partial class OrignSelectTf : Page, INotifyPropertyChanged
    {
        public List<TransformersOrign> tfOrgins { get; set; }
        public List<string> OriginNames { get; set; } = new();
        // we need the names of each orign,
        // need to say that if the selected item is chosen display the origin with its name so we can say tforigns is or pointer
        public event PropertyChangedEventHandler PropertyChanged;
        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged(nameof(SelectedItem));
                UpdateViewToMatchOrigin();
            }
                    
        }
        private TransformersOrign _currentView;
        public TransformersOrign CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                NotifyPropertyChanged(nameof(CurrentView));
            }
        }
        public OrignSelectTf()
        {
            InitializeComponent();
            LoadOrigins();
            GetNames();
            DataContext = this;
   
        }

        public void GetNames()
        {
            foreach( var x in tfOrgins)
            {
                OriginNames.Add(x.Name);
            }
        }

        public void UpdateViewToMatchOrigin()
        {
            CurrentView = tfOrgins.FirstOrDefault(x => x.Name == SelectedItem);

        }
        public void LoadOrigins()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "Origins.json");
            string json = File.ReadAllText(path);
            var originRoot = JsonSerializer.Deserialize<TFOriginsRoot>(json);
            tfOrgins = originRoot.Origins;
        }
       

        private void ToInfluences(object sender, RoutedEventArgs e)
        {

            if (TFCharacterSession.CurrentTransfomer.Origns == null)
            {
                MessageBox.Show("You must pick an Origin Before continuing");

            }
            else
                TFCharacterSession.CurrentTransfomer.Origns[0] = CurrentView;
            NavigationService.Navigate(new TFInfluencePage());
        }
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
