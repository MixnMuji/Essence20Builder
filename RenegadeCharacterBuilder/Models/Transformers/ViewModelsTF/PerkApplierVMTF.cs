using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Documents;
using System.Windows.Navigation;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;
using RenegadeCharacterBuilder.Models.Transformers.Enums;
namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class PerkApplierVMTF
    {
        public PerkBeingApplied MethodUsed{ get; set; }
        public GeneralPerkTF PerkBeingExecuted { get; set; }
        
        public object? PerkExectuionData { get; set; } //dangeours or sloppy use carefully lol store collections here
        
        public void GetDataForPerkExecution(PerkBeingApplied MethodUsed)
        {
            //save on code (var thing to filterout, object root, objecttypelistReturned)
            switch (MethodUsed)
            {
                case PerkBeingApplied.AAM:
                    {
                        var filterout = TFCharacterSession.CurrentTransfomer.Altmodes;
                        var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "Origins.json");
                        string json = File.ReadAllText(path);
                        var allOrigins = JsonSerializer.Deserialize<TFOriginsRoot>(json);
                        var filteredlist = allOrigins.Origins.Where(o => filterout!.Contains(o.AltMode));
                        List<Altmode> altmodeChoices = new List<Altmode>();

                        foreach (var ori in filteredlist)
                        {
                            altmodeChoices.Add(ori.AltMode);
                        }
                        PerkExectuionData = altmodeChoices;
                        break;
                    }
                case PerkBeingApplied.ATAM:
                    PerkExectuionData = new List<string>
                    {
                        "Land",
                        "Air",
                        "Aquatic",
                        "Ground"
                    };
                   
                   
                    break;

                case PerkBeingApplied.CT:
                    {
                        //get characters level
                        //make list of roles that aren't there own as well as the ranks under the level
                        //grab the perks
                        //display on page
                        /*
                        var filter = TFCharacterSession.CurrentTransfomer.Role;
                        int lvfilter = TFCharacterSession.CurrentTransfomer.CurrentLevel;
                        var Roles = LoadJson<TFRolesRoot>("Roles.json");
                        PerkExectuionData = Roles.Roles.Where(r => r.Name != filter.Name)
                        .SelectMany(r => r.Levels.Where(l => l.Level <= lvfilter && l.Perk != null)).SelectMany(l=> l.Perk).ToList();
                        */

                    }
                    break;
                    case PerkBeingApplied.HC:
                    {
                        //navigate to chracter builder page set new bools that are minicon, and human companion so that if they are sent with the data it will load a different page.
                    }
                    break;

                    case PerkBeingApplied.Mentor:
                    {
                        // have it navigate to a page and if it's this populate the list of scores and skills and have them listed to pick them
                    }
                    break;

                    case PerkBeingApplied.OAM:
                    {
                        PerkExectuionData = TFCharacterSession.CurrentTransfomer.Altmodes;
                    }
                    break;
                    case PerkBeingApplied.SC:
                    {
                        
                        List<string> sizes = ["Common", "long", "Huge"];
                        PerkExectuionData = sizes;

                    }
                    break;
            }
        }
        private T? LoadJson<T>(string jsonLocation, string gamejsonfolder)
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", jsonLocation);
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
       
    }
  
}
