using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Documents;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class PerkApplierVMTF
    {
        public PerkBeingApplied MethodUsed{ get; set; }
        public TransfomersCharacterModel characterBeingEdited { get; set; }
        public GeneralPerkTF PerkBeingExecuted { get; set; }
        
        public object? PerkExectuionData { get; set; } //dangeours or sloppy use carefully lol store collections here
        
        public void GetDataForPerkExecution()
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
                    //make page with move types and add it to the alt mode
                    //if multiple alt modes have them choose which one first
                    break;

                case PerkBeingApplied.CT:

                    //get characters level
                    //make list of roles that aren't there own as well as the ranks under the level
                    //grab the perks
                    //display on page
                    var filter = TFCharacterSession.CurrentTransfomer.Role;
                    int lvfilter = TFCharacterSession.CurrentTransfomer.CurrentLevel;
                    var applicableRoles = LoadJson<TFRolesRoot>("Roles.json");
                    


                    break;
            }
        }
        private T? LoadJson<T>(string jsonLocation)
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", jsonLocation);
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
       
    }
    public enum PerkBeingApplied
    {
        AAM,
        ATAM,
        CT,
        HC,
        Mentor,
        OAM,
        SC

    }
}
