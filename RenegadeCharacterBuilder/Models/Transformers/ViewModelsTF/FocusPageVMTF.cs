using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class FocusPageVMTF
    {
        /* Things we need
         1) the characters class : to get subclasses
        2) Subclasses
        
          */

        public string CharactersRoleToGetSubclasses { get; set; }
        public TFFocusesRoot ApplicableSubclasses { get; set; }

        public FocusPageVMTF()
        {
            CharactersRoleToGetSubclasses = TFCharacterSession.CurrentTransfomer.Role.Name;
        }

        public void GetSubClass(string currentRole)
        {
            switch (currentRole)
            {
                case "Analyst":
                    /*
                     * void LoadOrigins()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "Origins.json");
            string json = File.ReadAllText(path);
            var originRoot = JsonSerializer.Deserialize<TFOriginsRoot>(json);
            tfOrgins = originRoot.Origins;
        }
                     */
                    break;

                case "FieldCommander":
                    
                    break;

                case "Gunner":
                    
                    break;

                case "ModeMaster": //this needs its own method unfortunately
                    
                    break;

                case "Scientist":
                    
                    break;

                case "Scout":
                    
                    break;

                case "Warrior":
                    
                    break;
            }
        }
        public void GetSubclassesFromJson(string option1, string option2)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonCollection", "TranformersJson", "FocuesesTF");
            string json = File.ReadAllText(path);
            var AllSubclasses = JsonSerializer.Deserialize<TFFocusesRoot>(json);
            var filteredList = AllSubclasses.Sublcasses.Where(x => x.subclassName == option1 || x.subclassName == option2).ToList();
            ApplicableSubclasses = new TFFocusesRoot { Sublcasses = filteredList };

        }

    }
}
