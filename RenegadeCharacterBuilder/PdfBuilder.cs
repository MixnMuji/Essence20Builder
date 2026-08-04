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
using RenegadeCharacterBuilder.CharacterModels.TransfomersCompaions;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder
{
    public class PdfBuilder
    {
        public int designLogic { get; set; }
        // use containers to fill different sections
        public string path { get; set; } // since it's here I can assign it and refference it no problem



        public void GenerateCharacterPDf(int decider)
        {
            designLogic = decider;
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
                            c.Item().Element(container => GameTitle(container, designLogic)); // mind you this will need amd the otherw will need row defitions in the actuall containers
                            c.Item().Element(CharracterFluff); // make text smaller
                            c.Item().Element(HangUpsAndEquipment); // figure out how to unalign/ detach colums
                            c.Item().Element(Statblock); // this is the problem I guess there's too much data
                            c.Item().PageBreak();
                            c.Item().Element(PerksGearsAndBackGroundBonds);
                            c.Item().Element(WeaponsORHardPoints); // have another run at the switch case to determine which block is built gijoe and transformers are slightly diff
                            c.Item().Element(Altmodes); // run in an if statement as this is unique to transformers so don't need it in gijoe etc
                            c.Item().Element(OriginNotes); // Other things seem to have different things at the end here
                            c.Item().PageBreak();
                            c.Item().Element(RoleAndSubclassInfo); // sheets lack this and I think they need the in depth reminder of skills and abilites with levels
                            if(TFCharacterSession.CurrentTransfomer.companions.Count != 0) 
                            {
                                for(int i = 0; i< TFCharacterSession.CurrentTransfomer.companions.Count; i++)
                                {
                                    c.Item().Element(container => Companion( container, i));
                                    c.Item().Element(container=> Statblock2(container, i));

                                } 
                            }
                                                    


                        });
                    });

                }).GeneratePdf(path);
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });

        }


        private void GameTitle(IContainer container, int designLogic)
        {
           
            switch (designLogic)
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
            switch (designLogic)
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
                            c.Item().Text($" Name: {TFCharacterSession.CurrentTransfomer.Name}").FontSize(14);
                            c.Item().Text($"Pronouns: {TFCharacterSession.CurrentTransfomer.Pronouns}").FontSize(12);
                        });

                        //section two

                        t.Cell()
                        .RowSpan(2)
                        .Border(1)
                        .Padding(5)
                        .Text($"{TFCharacterSession.CurrentTransfomer.Description}").FontSize(12);

                        t.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text("Languages").FontSize(14);


                        // row 2
                        t.Cell()
                        .Border(1)
                        .Padding(5)
                        .Column(c =>
                        {
                            c.Item().Text($" Origin: {TFCharacterSession.CurrentTransfomer.Origns[0].Name}").FontSize(14);
                            c.Item().Text($" Role: {TFCharacterSession.CurrentTransfomer.Role.Name}").FontSize(12);
                            c.Item().Text($" Level: {TFCharacterSession.CurrentTransfomer.CurrentLevel}").FontSize(12);
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
            switch (designLogic)
            {
                case 1:
                    container.Row(r =>
                    {
                        r.RelativeItem(2).Border(1).Padding(5).Column(c =>
                        {
                            c.Item().Text("Influences: ").Bold().FontSize(14);
                            foreach (var influence in TFCharacterSession.CurrentTransfomer.Influences)
                            {
                                c.Item().Text($"{influence.Name}:").FontSize(13);
                                c.Item().Text($"{influence.Name}: {influence.Perk}").FontSize(12);
                            }
                        });

                        r.RelativeItem(3).Border(1).Padding(5).Column(c =>
                        {
                            c.Item().Text("Gear").FontSize(14);
                            c.Item().Text("Name Range Attack  Effect Notes").FontSize(13); //ADD GEAR
                            /*
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
                            } */

                        });
                        r.RelativeItem(2).Border(1).Padding(5).Column(c =>
                        {
                            c.Item().Text("Hang Ups:").FontSize(14);
                            foreach (var hang in TFCharacterSession.CurrentTransfomer.Hang_Ups)
                            {
                                c.Item().Text($"{hang.Name}:").FontSize(13);
                                c.Item().Text($"{hang.Effect}").FontSize(12);
                            }
                        });
                       
                        /* 
                         {
                             c.RelativeColumn(2);
                             c.RelativeColumn(3); // middle
                             c.RelativeColumn(1);

                         });


                         t.Cell().Border(1).Padding(5).Text($"Health: {TFCharacterSession.CurrentTransfomer.Health}");

                         //gear and column 3

                         t.Cell().RowSpan(3).Border(1).Padding(5).Column(c =>
                         {
                             c.Item().Text("Gear");
                             c.Item().Text("Name Range Attack  Effect Notes");

                            /* if (TFCharacterSession.CurrentTransfomer.Gear[0].Name.Equals(""))
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
             */

                    });
                    return;
            }
       }

        private void Statblock(IContainer container) // problem container has too much info?
        {
            var convert = new DieConverter();
            

            container.Table(t =>
            {
                t.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(2);
                    c.RelativeColumn(3);
                    c.RelativeColumn(4);
                    c.RelativeColumn(1);

                });

                //column 1
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"STRENGTH {TFCharacterSession.CurrentTransfomer.Strenght.CurrentRank}").FontSize(12);
                    c.Item().Text($"TOUGHNESS: {TFCharacterSession.CurrentTransfomer.Toughness.Value}").FontSize(12);
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.Strenght.CurrentRank} +  + ").FontSize(12); // add armor and way to track perks taken
                    foreach(SkillTF skill in TFCharacterSession.CurrentTransfomer.Strenght.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}").FontSize(12); 
                    }
               
                });

                //column 2
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SPEED {TFCharacterSession.CurrentTransfomer.Speed.CurrentRank}").FontSize(12);
                    c.Item().Text($"Evasion: {TFCharacterSession.CurrentTransfomer.Evasion.Value}").FontSize(12);
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.Speed.CurrentRank} +  + ").FontSize(12); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.Speed.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}").FontSize(12);
                    }

                });

                //column 3
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SMARTS {TFCharacterSession.CurrentTransfomer.Smarts.CurrentRank}").FontSize(12);
                    c.Item().Text($"WILLPOWER: {TFCharacterSession.CurrentTransfomer.Willpower.Value}").FontSize(12);
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.Smarts.CurrentRank} +  + ").FontSize(12); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.Smarts.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}").FontSize(12);
                    }


                });

                //column 4
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SOCIAL {TFCharacterSession.CurrentTransfomer.Social.CurrentRank}").FontSize(12);
                    c.Item().Text($"Cleverness: {TFCharacterSession.CurrentTransfomer.Cleverness.Value}").FontSize(12);
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.Social.CurrentRank} +  + ").FontSize(12); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.Social.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}").FontSize(12);
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
                        c.Item().Text($"{g.Name}").Bold().FontSize(14);
                        c.Item().Text($"{g.Text}").FontSize(12);
                    }
                });
                
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text("Gear").Bold();
                    foreach (GearTF g in TFCharacterSession.CurrentTransfomer.Gear)
                    {
                        c.Item().Text($"{g.Name}").Bold().FontSize(14);
                        c.Item().Text($"{g.Notes}").FontSize(12);
                    }
                });
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text("Background Bonds").Bold().FontSize(14);
                    
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
                    c.Item().Text("Hardpoints").Bold().FontSize(14);
                    c.Item().Text("NAME   RANGE   HARDPOINT   TRAITS   EFFECTS  ALTENRATE EFFECTS").FontSize(13);
                    foreach(HardPoint H in TFCharacterSession.CurrentTransfomer.HardPointsList)
                    {
                        c.Item().Text($"{H.name}  {H.range}   {count}  {H.traits}  {H.effects}  {H.alternateEffects}").FontSize(12);
                        count++;
                    }
                });
            });
        }
        
        private void Altmodes(IContainer container)
        {
            
            container.Row(r =>
            {
                r.RelativeItem().Border(1).Padding(5).Text("ALTMODES");
                foreach (Altmode a in TFCharacterSession.CurrentTransfomer.Altmodes) 
                {
                    r.RelativeItem(2).Border(1).Padding(5).Column(c =>
                    {
                        c.Item().Text($" Origin Name: {a.OrignName}").FontSize(14); //add name to altmode
                        c.Item().Text($" Crew: {a.Crew}").FontSize(12);
                        c.Item().Text($" Size: {a.Size}").FontSize(12);
                        c.Item().Text($" Movement {a.Movement}").FontSize(12);
                        c.Item().Text($" Movement Type: {a.Type}").FontSize(12);
                        if (a.Movement2 != null)
                        {
                            c.Item().Text($"Movement 2: {a.Movement2}").FontSize(12);
                            c.Item().Text($"Movement Type: {a.Type2}").FontSize(12);
                        }
                    });
                    
                   
                }
            });

            /*t.Cell().Border(1).Padding(5).Text("ALTMODES");

            foreach (Altmode a in TFCharacterSession.CurrentTransfomer.Altmodes) // theorectically we can make a column for every altmode this way 
            {
                    t.Cell().Border(1).Padding(5).Column(c =>
                    {
                        c.Item().Text($" Origin Name: {a.OrignName}").FontSize(14); //add name to altmode
                        c.Item().Text($" Crew: {a.Crew}").FontSize(12);
                        c.Item().Text($" Size: {a.Size}").FontSize(12);
                        c.Item().Text($" Movement {a.Movement}").FontSize(12);
                        c.Item().Text($" Movement Type: {a.Type}").FontSize(12);
                        if(a.Movement2 != null) 
                        {
                            c.Item().Text($"Movement 2: {a.Movement2}").FontSize(12);
                            c.Item().Text($"Movement Type: {a.Type2}").FontSize(12);
                        }
                    });
            };

                
            }); */
        }
        private void OriginNotes(IContainer container)
        {
            container.Row(t =>
            {
                t.RelativeItem().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text("Origin Notes").FontSize(14); ;
                });
                t.RelativeItem().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text("").FontSize(14);
                });

            });
        }

        private void RoleAndSubclassInfo(IContainer container)
        {

            container.Row(r =>
            {
                r.RelativeItem(2).Border(1).Padding(5).Column(c =>
                {


                    c.Item().Text($"Role: {TFCharacterSession.CurrentTransfomer.Role.Name}").Bold().FontSize(14);
                    c.Item().Text($"CyberTronian Perk: {TFCharacterSession.CurrentTransfomer.Role.CyberTronianPerk}").FontSize(12);
                    c.Item().Text("Qualifications").Bold().FontSize(12); ;
                    c.Item().Text($"Armor: {TFCharacterSession.CurrentTransfomer.Role.Qualifications.ArmorUpgrades}").FontSize(12);
                    c.Item().Text($"Weapon: {TFCharacterSession.CurrentTransfomer.Role.Qualifications.Weapons}").FontSize(12);
                    foreach (LevelTF l in TFCharacterSession.CurrentTransfomer.Role.Levels.Where(x => x.Level <= TFCharacterSession.CurrentTransfomer.CurrentLevel))
                    {
                        c.Item().Text($"Level {l.Level}").Bold().FontSize(14);
                        if (l.Perk != null) // this line breaks things
                        {
                            foreach (Perk p in l.Perk)
                            {
                                c.Item().Text($"{p.Name}").FontSize(12);
                                c.Item().Text($"{p.Effect}").FontSize(12);

                            }
                        }
                        if (l.SpeedBoost != 0)
                        {
                            c.Item().Text($"Essance Score Increase: Speed +{l.SpeedBoost}").FontSize(12);
                        }
                        if (l.SmartsBoost != 0)
                        {
                            c.Item().Text($"Essance Score Increase: Smarts +{l.SmartsBoost}").FontSize(12);
                        }
                        if (l.StrengthBoost != 0)
                        {
                            c.Item().Text($"Essance Score Increase: Strength +{l.StrengthBoost}").FontSize(12);
                        }
                        if (l.SocialBoost != 0)
                        {
                            c.Item().Text($"Essance Score Increase: Social +{l.SocialBoost}").FontSize(12);
                        }
                    }
                });

                r.RelativeItem(1).Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"Subclasss: {TFCharacterSession.CurrentTransfomer.sub.subclassName}").Bold().FontSize(14);
                    int limit = 1;// needs to be equal to focuss progression
                    foreach (LevelTF l in TFCharacterSession.CurrentTransfomer.Role.Levels.Where(x => x.Level <= TFCharacterSession.CurrentTransfomer.CurrentLevel))
                    {
                        //basically go through all the levels until you get to the current level if they increase the subclass rank add to the limit, if not limit stays the same
                        if (l.FocusProgression != 0)
                        {
                            limit++;
                        }
                    }

                        foreach (FocusPerk r in TFCharacterSession.CurrentTransfomer.sub.ranks.Take(limit))
                        {
                            c.Item().Text($"{r.AbilityName}").FontSize(13);
                            c.Item().Text($"{r.AbilityEffect}").FontSize(12);


                        }
                    

                });


            });
            

           
        }
        private void Companion(IContainer container, int index)
        {
            container.Table(t =>
            {
                t.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(2); //left
                    c.RelativeColumn(1); // right
                });

               
                t.Cell().Border(1)
                .Padding(5)
                .Column(c =>
                 {
                  
                  c.Item().Text($" Name: {TFCharacterSession.CurrentTransfomer.companions[index].Name}").FontSize(12);
                  c.Item().Text($"Pronouns: {TFCharacterSession.CurrentTransfomer.companions[index].Pronouns}").FontSize(12);
                  });

                    //section two

                    t.Cell()
                    .RowSpan(2)
                    .Border(1)
                    .Padding(5)
                    .Text($"{TFCharacterSession.CurrentTransfomer.companions[index].Description}");
                
            });
        
        }
               
          
        private void Statblock2(IContainer container, int index)
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
                    c.Item().Text($"STRENGTH {TFCharacterSession.CurrentTransfomer.companions[index].Strenght.CurrentRank}").FontSize(14);
                    c.Item().Text($"TOUGHNESS: {TFCharacterSession.CurrentTransfomer.companions[index].Toughness.Value}").FontSize(12);
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.companions[index].Strenght.CurrentRank} +  + ").FontSize(12); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.companions[index].Strenght.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}");
                    }

                });

                //column 2
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SPEED {TFCharacterSession.CurrentTransfomer.companions[index].Speed.CurrentRank}").FontSize(14);
                    c.Item().Text($"Evasion: {TFCharacterSession.CurrentTransfomer.companions[index].Evasion.Value}").FontSize(12);
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.companions[index].Speed.CurrentRank} +  + ").FontSize(12); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.companions[index].Speed.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}");
                    }

                });

                //column 3
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SMARTS {TFCharacterSession.CurrentTransfomer.companions[index].Smarts.CurrentRank}").FontSize(14);
                    c.Item().Text($"WILLPOWER: {TFCharacterSession.CurrentTransfomer.companions[index].Willpower.Value}").FontSize(12);
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.companions[index].Smarts.CurrentRank} +  + ").FontSize(12); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.companions[index].Smarts.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}");
                    }


                });

                //column 4
                t.Cell().Border(1).Padding(5).Column(c =>
                {
                    c.Item().Text($"SOCIAL {TFCharacterSession.CurrentTransfomer.companions[index].Social.CurrentRank}").FontSize(14);
                    c.Item().Text($"Cleverness: {TFCharacterSession.CurrentTransfomer.companions[index].Cleverness.Value}").FontSize(12);
                    c.Item().Text($"10 + {TFCharacterSession.CurrentTransfomer.companions[index].Social.CurrentRank} +  + ").FontSize(12); // add armor and way to track perks taken
                    foreach (SkillTF skill in TFCharacterSession.CurrentTransfomer.companions[index].Social.CorrespondingSkills)
                    {
                        string die = (string)convert.Convert(skill.SkillScore, typeof(string), null, CultureInfo.InvariantCulture);
                        c.Item().Text($"{skill.Name.ToUpper()}: {die}");
                    }


                });
            });
        }
    }
}
