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
            switch (MethodUsed)
            {
                case PerkBeingApplied.AAM:
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
