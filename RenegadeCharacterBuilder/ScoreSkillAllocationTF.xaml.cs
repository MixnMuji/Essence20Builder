using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF;
using static System.Net.Mime.MediaTypeNames;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for ScoreSkillAllocationTF.xaml
    /// </summary>
    public partial class ScoreSkillAllocationTF : Page
    {
        public CharScorePageModelTF Viewmodel { get; }
        public ScoreSkillAllocationTF()
        {
            InitializeComponent();
            Viewmodel = new CharScorePageModelTF();
            DataContext = Viewmodel;

        }

        /* template for later 
        <TextBlock Grid.Row="0"  Text= "Strength" HorizontalAlignment= "Center" />
            < Button Grid.Row= "0"  Content= "-" Click= "SubtracFromoScore" Height= "15" Width= "10" HorizontalAlignment= "Center" Margin= "0,0,50,0" VerticalAlignment= "Bottom" Tag= "Strength" />
            < TextBlock Text= "{Binding Strength}" HorizontalAlignment= "Center" Margin= "0,0,0,2" VerticalAlignment= "Bottom" ></ TextBlock >
            < Button Grid.Row= "0" Content= "+" Click= "AddToScore" Height= "15" Width= "10" HorizontalAlignment= "Center" Margin= "5,0,-50,0" VerticalAlignment= "Bottom" Tag= "Strength" />

        */





    }
}
