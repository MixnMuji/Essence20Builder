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
    /// Interaction logic for RolesTF.xaml
    /// </summary>
    public partial class RolesTF : Page
    {
        public List<Roles> tfRoles { get; set; }
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
            tfRoles = RoleRoot.Roles;
            
        }
    }
}
