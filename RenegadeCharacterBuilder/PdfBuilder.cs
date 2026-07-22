using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Documents;
using System.Xml.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder
{
    public class PdfBuilder
    {
      // use containers to fill different sections
        public string path { get; set; } // since it's here I can assign it and refference it no problem
        public int decider { get; set; }

        
        public void GenerateCharacterPDf(int decider)
        {
            // Int decider will be a number code that will determine what character object is ie transformers,mlp, powerrangers, etc
            /*
             * 0 Title
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
                    return;
                    // put other game characters here
            }
            Processpdf(path); // this will generate the pdf

        }

        private void Processpdf(string path)
        {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));

            //add content that needs to be found in deciders
            /*
                1)game Name
             
             */
            // thing about return here
            Document
                .Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.DefaultTextStyle(x => x.FontSize(20));
                        page.Margin(25);

                        page.Content()
                        .PaddingBottom(15).Column(c =>
                        {
                            c.Item().Element(GameTitle(decider); // mind you this will need amd the otherw will need row defitions in the actuall containers
                            c.Item().Element(CharracterFull);
                            c.Item().Element(CharracterFluff);
                            c.Item().Element(HAngups & Equipment);
                            c.Item().Element(CharracterFull);
                            c.Item().Element(Statblock);
                            c.Item().PageBreak();
                            c.Item().Element(PerksGearsAndBackGroundBonds);
                            c.Item().Element(WeaponsORHardPoints); // have another run at the switch case to determine which block is built gijoe and transformers are slightly diff
                            c.Item().Element(Altmodes); // run in an if statement as this is unique to transformers so don't need it in gijoe etc
                            c.Item().Element(Statblock);
                            c.Item().Element(Origin); // Other things seem to have different things at the end here
                            c.Item().PageBreak();
                            c.Item().Element(RoleAndSubclassInfo); // sheets lack this and I think they need the in depth reminder of skills and abilites with levels
                            c.Item().Element(Companion); // another if statment which would bascially be a mini sheet attached if the character
                                                         // has a companion of some sort, this is also missing from the offical release, so add pagebreakbefore


                        });
                    });

                }).GeneratePdf(path);
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });

        }


        private void GameTitle(int decider, IContainer container)
        {
            switch (decider)
            {
                case 1:
                    container.Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                        });

                        var color = TFCharacterSession.CurrentTransfomer.Faction == Alliegence.Descepticon ? Colors.Purple.Lighten1 : Colors.Red.Lighten1;

                        t.Cell()
                        .Background(color)
                        .Padding(10)
                        .Text("Transformers")
                        .FontColor(Colors.White).
                        FontSize(33).
                        Bold();

                    } );
                    return;
                
            }

        }
       
        
    }
}
