using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder
{
    public class PdfBuilder
    {
      // use containers to fill different sections
        public string path { get; set; } // since it's here I can assign it and refference it no problem
        public void GenerateCharacterPDf(int decider)
        {
            // Int decider will be a number code that will determine what character object is ie transformers,mlp, powerrangers, etc
            /*
             * 1) Define each sections as blocks  So really we need a blocks
             *  Visual representaiton of blocks  editors note || represents column devision
                [
                "Character Info: name, pronouns, Description, Level, Role, subclass, || alliegence, Languages" : Row 1
                Influences, Hang ups, Energon, Movement, Health, || Attack (This will be from weapons and that will be done after character creation: Row 2
                This row is the stat row ripped one to one from the xaml for stat: Row 3

                NewPage,

                Perk || Gear || Boned row 1

                Hardpoints (research this) Row 2

                Armor Row 3

                Altmode || Altmode Row 4
                
            
                NewPage

                General Perks Row 1

                New Page

                Companions

                Remake page 1 baiscally
                
             
             
             
             */

            /* Hierarchy of items in quest pdf is  column.Item()
        .Border(1)
        .Background(Colors.Blue.Lighten4)
        .Padding(15)
        .Text("border → background → padding");
             
             
             */
            
            switch (decider)
            {
                case 1:
                    path = System.IO.Path.Combine("SavedCharacters",
                                  "Transformers",
                                  "Pdf",
                                  $"{TFCharacterSession.CurrentTransfomer.Name}.pdf"
                                 
                     );
                    break;
            }
            
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
        }

    
    private void GreenSection(IContainer container)
        {
            container.Grid(grid =>
            {
                grid.Columns(3);
                grid.Spacing(15);

                grid.Item(3).Text("Green section")
                    .FontColor(Colors.Green.Darken2).FontSize(32).Bold();

                grid.Item(3).Text(Placeholders.Paragraph()).Light();

                foreach (var i in Enumerable.Range(0, 12))
                    grid.Item().AspectRatio(4 / 3f).Background(Colors.Green.Lighten4);
            });
        }

        private void BlueSection(IContainer container)
        {
            container.Grid(grid =>
            {
                grid.Columns(3);
                grid.Spacing(15);

                grid.Item(3).Text("Blue section")
                    .FontColor(Colors.Blue.Darken2).FontSize(32).Bold();

                grid.Item(3).Text(Placeholders.Paragraph()).Light();

                foreach (var i in Enumerable.Range(0, 18))
                    grid.Item().AspectRatio(4 / 3f).Background(Colors.Blue.Lighten4);
            });
        }
    }
}
