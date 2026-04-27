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
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
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
            Viewmodel.findRoleForStats();
           DataContext = Viewmodel;

        }



        /*
         *<ItemsControl ItemsSource="{Binding CharacterRoleForKeyScores}">
                        <ItemsControl.ItemTemplate>
                            <DataTemplate>
                                <StackPanel>
                                    <TextBlock Text="{Binding Name}"/>
                                </StackPanel>
                            </DataTemplate>
                        </ItemsControl.ItemTemplate>
                    </ItemsControl>
         */

    }
}
