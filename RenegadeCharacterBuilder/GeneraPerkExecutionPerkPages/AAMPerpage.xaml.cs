using System;
using System.Collections.Generic;
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
using RenegadeCharacterBuilder.GlobalMethods;
namespace RenegadeCharacterBuilder.GeneraPerkExecutionPerkPages
{
    /// <summary>
    /// Interaction logic for AAMPerpage.xaml
    /// </summary>
    public partial class AAMPerpage : Page
    {
        
        public AAMPerpage()
        {
            var calldata = new GlobalCall();
            var firstlist =  calldata.LoadJson<TFOriginsRoot>("Origins.json", "TransformersJsons");
            var filterout = TFCharacterSession.CurrentTransfomer.Origns.Select(o=> o.Name);
            var filteredlist = firstlist.Origins.Where(o => !filterout.Contains(o.Name)).ToList();
            MessageBox.Show(filteredlist.Count.ToString());
            InitializeComponent();
            DataContext = filteredlist; // this should let our datacontext be the altmodes not taken
            
        }
    }
}
