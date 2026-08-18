using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers.GearTFChildrenClasses;
using RenegadeCharacterBuilder.Models.Transformers.Roots;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class EquimpmentPageVMTF
    {
        public string[] EquipmentTypeView = ["Armor", "Kits", "Weapons", "Support Equipment"]; // so basically if these are chosen we show a different obserable collection

        public ObservableCollection<Upgrades> ArmorUpgrades { get; set; }
        public ObservableCollection<Upgrades> Kits { get; set; }
        public ObservableCollection<WeaponTF> Weapons { get; set; }

        public ObservableCollection<SupportEqupmentTF> SupportEquipment { get; set; } = new();

        public ObservableCollection<Upgrades> TakenArmorUpgrades { get; set; }
        public ObservableCollection<Upgrades> TakenKits { get; set; }
        public ObservableCollection<WeaponTF> TakenWeapons { get; set; }

        public ObservableCollection<SupportEqupmentTF> TakenSupportEquipment { get; set; }

        public GlobalCall getdata { get; set; }

        public int _harpointsLimit { get; set; } // this is going to change so add on property changed

        public EquimpmentPageVMTF()
        {
            getdata = new GlobalCall();
            //give page a tab to show taken equipment
            //need method to remove equipment and add them back to list
            // need method to add equipment to taken list and vice versa for all items

            //need to deserialize all 4 lists
        }
        public void GetWeapons()
        {
            var weaponsfull = getdata.LoadJson<WeaponRoot>("WeaponsCore.json", "TransformersJsons");
            //check by class to get specificts
        }
        public void Getsup()
        {
            var SupportEquipment = getdata.LoadJson<SupportEquipRootTF>("SupEquipCore.json", "TransformersJsons"); // figure out how to shift list into observable collection maybe just shift the type in root
        }
        public void GetArmor()
        {
            var ArmorEquipment = getdata.LoadJson<ArmorRootTF>("ArmorCore.json", "TransformersJsons");
            foreach (Upgrades a in ArmorEquipment.Armors)
            {

            }
        }
        public void GetKits()
        {
            var Kits = getdata.LoadJson<KitRootTF>("KitCore.json", "TransformersJsons");
        }

        /*
        namespace RenegadeCharacterBuilder.GlobalMethods
    {
        public class GlobalCall
        {
            public T? LoadJson<T>(string jsonLocation, string gamejsonfolder)
            {
                var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", gamejsonfolder, jsonLocation);
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    } */
}
}
