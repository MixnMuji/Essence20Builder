using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
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
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace RenegadeCharacterBuilder
{
    /// <summary>
    /// Interaction logic for startpage.xaml
    /// </summary>
    public partial class startpage : Page
    {
       
        public startpage()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            InitializeComponent();
            ComboBox1.ItemsSource = Enum.GetValues(typeof(GameSelected));
           
            
        }
        private void proceed_Click(object sender, RoutedEventArgs e)
        {
            if(ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("you must make a selection first");
                return;
            }
            GameSelected selected = (GameSelected)ComboBox1.SelectedItem;
            // run switch cases to determine slection and then route to next page!s
            
            switch (selected)
            {
                case GameSelected.Transfomers:
                    NavigationService.Navigate(new ExpansionsTransfomersSelect());
                    break;
                case GameSelected.PowerRangers:
                    MessageBox.Show("Power Rangers not implented yet");
                    break;
                case GameSelected.Mlp:
                    MessageBox.Show("My Little Pony not implented yet");
                    break;
                case GameSelected.GiJoe:
                    MessageBox.Show("Gi Joe not Implimented yet");
                    break;
                 
            }
        }

        private void DevMode(object sender, RoutedEventArgs e)
        {
            if(devSelection.SelectedItem == null)
            {
                MessageBox.Show("Need choice");
                return;
            }
            var page = devSelection.SelectionBoxItem;
            switch (page)
            {
                case "Roles":
                    NavigationService.Navigate(new RolesTF());
                    break;
                case "Scores":
                    NavigationService.Navigate(new ScoreSkillAllocationTF());
                    break;
                case "Focus":
                    NavigationService.Navigate(new FocusPerks());
                    break;
                case "PDFTest":
                    string path = System.IO.Path.Combine("SavedCharacters",
                                  "Transformers",
                                  "Pdf",
                                  "TestCase.pdf"
                     );
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));

                    Document.Create(Container =>
                    {
                        Container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(2, Unit.Centimetre);
                            page.PageColor(QuestPDF.Helpers.Colors.White);
                            page.DefaultTextStyle(x => x.FontSize(20));

                            page.Header()
            .Text("Hello PDF!")
            .SemiBold().FontSize(36).FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

                            page.Content()
                                .PaddingVertical(1, Unit.Centimetre)
                                .Column(x =>
                                {
                                    x.Spacing(20);

                                    x.Item().Text(Placeholders.LoremIpsum());
                                    x.Item().Image(Placeholders.Image(200, 100));
                                });

                            page.Footer()
                                .AlignCenter()
                                .Text(x =>
                                {
                                    x.Span("Page ");
                                    x.CurrentPageNumber();
                                });
                        });
                    }).GeneratePdf(path);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                    
                   
                    break;
            }

        }
    }
}
