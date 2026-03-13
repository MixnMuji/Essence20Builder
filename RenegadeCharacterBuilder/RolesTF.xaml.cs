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

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for RolesTF.xaml
    /// </summary>
    public partial class RolesTF : Page
    {
        public Role CurrentRole { get; set; }
        public RolesTF()
        {
            InitializeComponent();
            LoadRole();
            DataContext = CurrentRole;
        }
        public void LoadRole()
        {
            string json = File.ReadAllText("AnalystTF.json");
            CurrentRole = JsonSerializer.Deserialize<Role>(json) ?? new Role();
        }
    }
}
