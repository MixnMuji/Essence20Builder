using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
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
    /// Interaction logic for RolesTF.xaml
    /// </summary>
    public partial class RolesTF : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Roles> tfRoles { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private int _currentIndex;
        private Roles _currentRole;
        private Point _startPoint;
       
        public RolesTF()
        {
            InitializeComponent();
            LoadRoles();
            DataContext = this;
        }
        public void LoadRoles()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "Roles.json");
            var json = File.ReadAllText(path);
          
            var RoleRoot = JsonSerializer.Deserialize<TFRolesRoot>(json);
            tfRoles = new ObservableCollection<Roles>(RoleRoot?.Roles ?? new List<Roles>());
            CurrentIndex = 0;

            
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (tfRoles == null || tfRoles.Count == 0)
                {
                    return;
                }
                if (value < 0)
                {
                    _currentIndex = tfRoles.Count - 1;
                }
                else if (value >= tfRoles.Count)
                {
                    _currentIndex = 0;
                }
                else
                {
                    _currentIndex = value;
                }
                CurrentRole = tfRoles[_currentIndex];
                OnPropertyChanged();
            }
        }
        public Roles CurrentRole
        {
            get => _currentRole;
            set
            {
                _currentRole = value;
                OnPropertyChanged();
            }
        }
       
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void NextRoleDisplay(object sender, RoutedEventArgs e)
        {
            CurrentIndex++;
        }

        private void PerviousRoleDisplay(object sender, RoutedEventArgs e)
        {
            CurrentIndex--;
        }
    }
}
