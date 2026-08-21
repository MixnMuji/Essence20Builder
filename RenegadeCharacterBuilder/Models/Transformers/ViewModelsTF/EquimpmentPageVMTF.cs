using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers.GearTFChildrenClasses;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
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

        public string RoleName { get; set; }

        public int _harpointsLimit { get; set; } // this is going to change so add on property changed

        public EquimpmentPageVMTF()
        {
            getdata = new GlobalCall();

            Weapons = new ObservableCollection<WeaponTF>();
            ArmorUpgrades = new ObservableCollection<Upgrades>();
            SupportEquipment = new ObservableCollection<SupportEqupmentTF>();
            Kits = new ObservableCollection<Upgrades>();


            RoleName = TFCharacterSession.CurrentTransfomer.Role.Name;
           


            //give page a tab to show taken equipment
            //need method to remove equipment and add them back to list
            // need method to add equipment to taken list and vice versa for all items

            //need to deserialize all 4 lists
        }
        public void GetWeapons()
        {
            var weaponsfull = getdata.LoadJson<WeaponRoot>("WeaponsCore.json", "TransformersJsons");
            switch (RoleName)
            {
                case "Analyst":
                    Weapons = GetItemsForObserablecollection<WeaponTF>(["1", "2", "Standard"], weaponsfull.weapons);
                    break;
                case "Field Commander":
                    Weapons = GetItemsForObserablecollection<WeaponTF>(["ballistic", "melee", "standard"], weaponsfull.weapons);
                    break;
                case "Gunner":
                    Weapons = GetItemsForObserablecollection<WeaponTF>(["limited", "standard", "ballistic", "projectile"], weaponsfull.weapons);
                    break;
                case "Modemaster":
                    Weapons = GetItemsForObserablecollection<WeaponTF>(["standard"], weaponsfull.weapons);
                    break;
                case "Scientist":
                    Weapons = GetItemsForObserablecollection<WeaponTF>(["electic", "explosivies"], weaponsfull.weapons);
                    break;
                case "Scout":
                    Weapons = GetItemsForObserablecollection<WeaponTF>(["silent"], weaponsfull.weapons);
                    break;
                case "Warrior":
                    Weapons = GetItemsForObserablecollection<WeaponTF>(["limited", "melee"], weaponsfull.weapons);
                    break;
            }
            Weapons = GetItemsForObserablecollection<WeaponTF>(["limited", "melee"], weaponsfull.weapons);
            
        }
        public void Getsup()
        {
            var se = getdata.LoadJson<SupportEquipRootTF>("SupEquipCore.json", "TransformersJsons"); // figure out how to shift list into observable collection maybe just shift the type in root
            foreach( var item in se.SupportEquipment)
            {
                if (item.prereq!.Contains("Extra")){
                    SupportEquipment.Add(item);
                }
            }
        }
        public void GetArmor()
        {
            var ArmorEquipment = getdata.LoadJson<ArmorRootTF>("ArmorCore.json", "TransformersJsons");
            switch (RoleName)
            {
                case "Analyst":
                    ArmorUpgrades= GetItemsForObserablecollection<Upgrades>(["Standard"], ArmorEquipment.Armors);
                    break;
                case "Field Commander":
                    ArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard", "limited"], ArmorEquipment.Armors);
                    break;
                case "Gunner":
                    ArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard", "limited"], ArmorEquipment.Armors);
                    break;
                case "Modemaster":
                    ArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard"], ArmorEquipment.Armors);
                    break;
                case "Scientist":
                    ArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard", "limited"], ArmorEquipment.Armors);
                    break;
                case "Scout":
                    ArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard", "limited"], ArmorEquipment.Armors);
                    break;
                case "Warrior":
                    ArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard","limited", " Restricted"], ArmorEquipment.Armors);
                    break;
            }
        }


        public void GetKits()
        {
            var Kts = getdata.LoadJson<KitRootTF>("KitCore.json", "TransformersJsons");
            foreach (var k in Kits.Kts)
            {
                Kits.Add(k);
            }

        }

        public ObservableCollection<T> GetItemsForObserablecollection<T>(string[] Include, List<T> root) where T : GearTF
        {
            var result = new ObservableCollection<T>();
            foreach (T item in root)
            {
                var properties = item.GetProperiesForComparison();
                if (Include.Any(keyword => properties.Contains(keyword)))
                {
                    result.Add(item);
                }

            }
            return result;
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
