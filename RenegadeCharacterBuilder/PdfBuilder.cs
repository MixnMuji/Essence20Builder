using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RenegadeCharacterBuilder.GlobalMethods;
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
                    break;
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
                            c.Item().Element(GameTitle); // mind you this will need amd the otherw will need row defitions in the actuall containers
                            c.Item().Element(CharracterFluff);
                            c.Item().Element(HangUpsAndEquipment);
                            c.Item().Element(Statblock);
                            c.Item().PageBreak();
                            c.Item().Element(PerksGearsAndBackGroundBonds);
                            c.Item().Element(WeaponsORHardPoints); // have another run at the switch case to determine which block is built gijoe and transformers are slightly diff
                            c.Item().Element(Altmodes); // run in an if statement as this is unique to transformers so don't need it in gijoe etc
                            c.Item().Element(OriginNotes); // Other things seem to have different things at the end here
                            c.Item().PageBreak();
                            c.Item().Element(RoleAndSubclassInfo); // sheets lack this and I think they need the in depth reminder of skills and abilites with levels
                            c.Item().Element(Companion); // another if statment which would bascially be a mini sheet attached if the character
                            c.Item().Element(Statblock2);                             // has a companion of some sort, this is also missing from the offical release, so add pagebreakbefore


                        });
                    });

                }).GeneratePdf(path);
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });

        }


        private void GameTitle(IContainer container)
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

                    });
                    return;

            }

        }
        private void CharracterFluff(IContainer container)
        {
            switch (decider)
            {
                case 1:
                    container.Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(2); //left
                            c.RelativeColumn(3); // middle
                            c.RelativeColumn(1); // right
                        });

                        //row 1
                        t.Cell().Border(1)
                        .Padding(5)
                        .Column(c =>
                        {
                            c.Item().Text($" Name: {TFCharacterSession.CurrentTransfomer.Name}");
                            c.Item().Text($"Pronouns: {TFCharacterSession.CurrentTransfomer.Pronouns}");
                        });

                        //section two

                        t.Cell()
                        .RowSpan(2)
                        .Border(1)
                        .Padding(5)
                        .Text($"{TFCharacterSession.CurrentTransfomer.Description}");

                        t.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text("Languages");


                        // row 2
                        t.Cell()
                        .Border(1)
                        .Padding(5)
                        .Column(c =>
                        {
                            c.Item().Text($" Origin: {TFCharacterSession.CurrentTransfomer.Origns[0].Name}");
                            c.Item().Text($" Role: {TFCharacterSession.CurrentTransfomer.Role.Name}");
                            c.Item().Text($" Level: {TFCharacterSession.CurrentTransfomer.CurrentLevel}");
                        });

                        t.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text($"Faction: {TFCharacterSession.CurrentTransfomer.Faction}");

                    });
                    return;
            }
        }

        private void HangUpsAndEquipment(IContainer container)
       {
            switch (decider)
            {
                case 1:
                    container.Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(2);
                            c.RelativeColumn(3); // middle
                            c.RelativeColumn(1);

                        });
                        t.Cell().Border(1).Padding(5).Column(c =>
                        {
                            c.Item().Text("Influences: ").Bold();
                            foreach (var influence in TFCharacterSession.CurrentTransfomer.Influences)
                            {
                                c.Item().Text($"{influence.Name}: {influence.Perk}");
                            }
                        });

                        t.Cell().Border(1).Padding(5).Text($"Health: {TFCharacterSession.CurrentTransfomer.Health}");

                        //gear and column 3

                        t.Cell().RowSpan(3).Border(1).Padding(5).Column(c =>
                        {
                            c.Item().Text("Gear");
                            c.Item().Text("Name Range Attack  Effect Notes");

                            if (TFCharacterSession.CurrentTransfomer.Gear[0].Name.Equals(""))
                            {
                                c.Item().Text("");
                            }
                            else
                            {
                                foreach (var gear in TFCharacterSession.CurrentTransfomer.Gear)
                                {

                                    c.Item().Text($"{gear.Name}  {gear.Range}  {gear.Attack}  {gear.Effect}  {gear.Notes}");
                                }
                            }
                        });

                        //row two
                        t.Cell().Border(1).Padding(5).Column(c =>
                        {
                            c.Item().Text("Hang Ups:");
                            foreach(var hang in TFCharacterSession.CurrentTransfomer.Hang_Ups)
                            {
                                c.Item().Text($"{hang.Name}: {hang.Effect}");
                            }
                        });

                        t.Cell().Border(1).Border(5).Text($"Movment:{TFCharacterSession.CurrentTransfomer.Origns[0].BotMode.Movement}");
                        
                        
                      
                    });
                return;
            }
       }

        private void Statblock(IContainer container)
        {
            var convert = new DieConverter();
            

            container.Table(t =>
            {
                t.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(2);
                    c.RelativeColumn(3);
                    c.RelativeColumn(4);
                    c.ConstantColumn(1);

                });

                //column 1
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"STRENGTH {TFCharacterSession.CurrentTransfomer.Strenght.CurrentRank}");
                    c.Item().Text($"TOUGHNESS: {TFCharacterSession.CurrentTransfomer.Toughness.Value}");
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.Strenght.CurrentRank} +  + "); // add armor and way to track perks taken
                    foreach(SkillTF skill in TFCharacterSession.CurrentTransfomer.Strenght.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}"); 
                    }
               
                });

                //column 2
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SPEED {TFCharacterSession.CurrentTransfomer.Speed.CurrentRank}");
                    c.Item().Text($"Evasion: {TFCharacterSession.CurrentTransfomer.Evasion.Value}");
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.Speed.CurrentRank} +  + "); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.Speed.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}");
                    }

                });

                //column 3
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SMARTS {TFCharacterSession.CurrentTransfomer.Smarts.CurrentRank}");
                    c.Item().Text($"WILLPOWER: {TFCharacterSession.CurrentTransfomer.Willpower.Value}");
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.Smarts.CurrentRank} +  + "); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.Smarts.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}");
                    }


                });

                //column 4
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SOCIAL {TFCharacterSession.CurrentTransfomer.Social.CurrentRank}");
                    c.Item().Text($"Cleverness: {TFCharacterSession.CurrentTransfomer.Cleverness.Value}");
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.Social.CurrentRank} +  + "); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.Social.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}");
                    }


                });
            });
           
        }


        private void PerksGearsAndBackGroundBonds(IContainer container)
        {
            container.Table(t =>
            {
                t.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(2);
                    c.RelativeColumn(3);
                    c.RelativeColumn(10);
                });

                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text("Perks").Bold();
                    foreach(GeneralPerkTF g in TFCharacterSession.CurrentTransfomer.PickedPerks)
                    {
                        c.Item().Text($"{g.Name}").Bold();
                        c.Item().Text($"{g.Text}");
                    }
                });
                
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text("Gear").Bold();
                    foreach (GearTF g in TFCharacterSession.CurrentTransfomer.Gear)
                    {
                        c.Item().Text($"{g.Name}").Bold();
                        c.Item().Text($"{g.Notes}");
                    }
                });
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text("Background Bonds").Bold();
                    
                });
            });
        }

        private void WeaponsORHardPoints(IContainer container)
        {
            container.Table(t =>
            {
                t.ColumnsDefinition(c =>
                {
                    c.RelativeColumn();
                });

                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    int count = 0;
                    c.Item().Text("Hardpoints").Bold();
                    c.Item().Text("NAME   RANGE   HARDPOINT   TRAITS   EFFECTS  ALTENRATE EFFECTS");
                    foreach(HardPoint H in TFCharacterSession.CurrentTransfomer.HardPointsList)
                    {
                        c.Item().Text($"{H.name}  {H.range}   {count}  {H.traits}  {H.effects}  {H.alternateEffects}");
                        count++;
                    }
                });
            });
        }
        
        private void Altmodes(IContainer container)
        {
            
            container.Table(t =>
            {
            t.ColumnsDefinition(c =>
            {
                foreach (Altmode a in TFCharacterSession.CurrentTransfomer.Altmodes) 
                {
                    c.RelativeColumn();
                   
                }
            });

            t.Cell().Border(1).Padding(5).Text("ALTMODES");

            foreach (Altmode a in TFCharacterSession.CurrentTransfomer.Altmodes) // theorectically we can make a column for every altmode this way 
            {
                    t.Cell().Border(1).Padding(5).Column(c =>
                    {
                        c.Item().Text($" Origin Name: {a.OrignName}"); //add name to altmode
                        c.Item().Text($" Crew: {a.Crew}");
                        c.Item().Text($" Size: {a.Size}");
                        c.Item().Text($" Movement {a.Movement}");
                        c.Item().Text($" Movement Type {a.Type}");
                        if(a.Movement2 != null) 
                        {
                            c.Item().Text($"Movement 2{a.Movement2}");
                            c.Item().Text($"Movement Type{a.Type2}");
                        }
                    });
            };

                
            });
        }
        private void OriginNotes(IContainer container)
        {
            container.Table(t =>
            {
                t.ColumnsDefinition(c =>
                {
                    c.RelativeColumn();
                });
                t.Cell().Border(1).Padding(5).Column(c => {
                    c.Item().Text("Origin Notes");
                    c.Item().Text(""); // need to have notes written before hand questpdf doesn't allow fillables

                });
            });
        }

        private void RoleAndSubclassInfo(IContainer container)
        {
            container.Table(t =>
            {
                t.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(2);
                    c.RelativeColumn(1);
                });

                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"Role: {TFCharacterSession.CurrentTransfomer.Role.Name}").Bold();
                    c.Item().Text($"CyberTronian Perk: {TFCharacterSession.CurrentTransfomer.Role.CyberTronianPerk}");
                    c.Item().Text("Qualifications").Bold(); ;
                    c.Item().Text($"Armor: {TFCharacterSession.CurrentTransfomer.Role.Qualifications.ArmorUpgrades}");
                    c.Item().Text($"Weapon: {TFCharacterSession.CurrentTransfomer.Role.Qualifications.Weapons}");
                    foreach (LevelTF l in TFCharacterSession.CurrentTransfomer.Role.Levels.Where(x => x.Level <= TFCharacterSession.CurrentTransfomer.CurrentLevel))
                    {
                        c.Item().Text($"Level {l.Level}").Bold();
                        if (l.Perk[0].Name != null)
                        {
                            foreach (Perk p in l.Perk)
                            {
                                c.Item().Text($"{p.Name}");
                                c.Item().Text($"{p.Effect}");

                        }
                            }
                        if(l.SpeedBoost != 0)
                        {
                            c.Item().Text($"Essance Score Increase: Speed +{l.SpeedBoost}");
                        }
                        if (l.SmartsBoost != 0)
                        {
                            c.Item().Text($"Essance Score Increase: Smarts +{l.SmartsBoost}");
                        }
                        if (l.StrengthBoost != 0)
                        {
                            c.Item().Text($"Essance Score Increase: Strength +{l.StrengthBoost}");
                        }
                        if (l.SocialBoost != 0)
                        {
                            c.Item().Text($"Essance Score Increase: Social +{l.SocialBoost}");
                        }
                    };
             
    });

                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"Subclasss: {TFCharacterSession.CurrentTransfomer.sub.subclassName}");
                    int counter = 0;
                    int limit = 0;// needs to be equal to focuss progression
                    foreach(LevelTF l in TFCharacterSession.CurrentTransfomer.Role.Levels.Where(x => x.Level <= TFCharacterSession.CurrentTransfomer.CurrentLevel)) 
                    { if(l.FocusProgression != 0)
                        {
                            limit++;
                        }
                    }
                    while (counter <= limit) 
                    {
                        foreach (FocusPerk r in TFCharacterSession.CurrentTransfomer.sub.ranks)
                        {
                            c.Item().Text($"{r.AbilityName}");
                            c.Item().Text($"{r.AbilityEffect}");
                            counter++;

                        }
                    }
                });

            });
        }
    }
}
