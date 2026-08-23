using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Windows.Controls.Primitives;
using Microsoft.VisualBasic;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers.GearTFChildrenClasses;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class EquimpmentPageVMTF :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string[] EquipmentTypeView = ["Armor", "Kits", "Weapons", "Support Equipment"]; // so basically if these are chosen we show a different obserable collection

        public ObservableCollection<Upgrades> TrainedArmorUpgrades { get; set; }
        public ObservableCollection<WeaponTF> TrainedWeapons { get; set; }
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
        public int HardpointLimit
        {
            get => _harpointsLimit;
            set 
            {
                _harpointsLimit = value;
                NotifyPropertyChanged(nameof(HardpointLimit));
            }
        }
        

        public EquimpmentPageVMTF()
        {
            getdata = new GlobalCall();

            TrainedWeapons = new ObservableCollection<WeaponTF>();
            TrainedArmorUpgrades = new ObservableCollection<Upgrades>();

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
       
        public void AddEquuipment(object selection)
        {
            switch (selection)
            {
                case WeaponTF weapon:
                    TakenWeapons.Add(weapon);
                    if (TrainedWeapons.Contains(weapon))
                    {
                        TrainedWeapons.Remove(weapon);
                    }
                    else
                    {
                        Weapons.Remove(weapon);
                    }
                    break;

                case Upgrades upgrade:
                    if (Kits.Contains(upgrade))
                    {
                        TakenKits.Add(upgrade);
                        Kits.Remove(upgrade);
                    }
                    if (ArmorUpgrades.Contains(upgrade))
                    {
                        TakenArmorUpgrades.Add(upgrade);
                        ArmorUpgrades.Remove(upgrade);
                    }
                    if (TrainedArmorUpgrades.Contains(upgrade))
                    {
                        TakenArmorUpgrades.Add(upgrade);
                        TrainedArmorUpgrades.Remove(upgrade);
                    }
                    break;

                case SupportEqupmentTF Se:
                    if (SupportEquipment.Contains(Se))
                    {
                        if(Se.name == "Extra Crew Capacity")
                        {
                            var addothetraits = getdata.LoadJson<SupportEquipRootTF>("SupEquipCore.json", "TransformersJsons");
                            foreach (var item in addothetraits.SupportEquipment)
                            {
                                if (item.prereq == "Extra Crew capacity") {
                                    SupportEquipment.Add(item);
                                     }
                            }
                            
                        }
                        TakenSupportEquipment.Add(Se);
                        SupportEquipment.Remove(Se);
                    }
                    break;
            }
            
          
            
        }
        public void GetWeapons()
        {
            var weaponsfull = getdata.LoadJson<WeaponRoot>("WeaponsCore.json", "TransformersJsons");
            switch (RoleName)
            {
                case "Analyst":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["1", "2", "Standard"], weaponsfull.weapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Field Commander":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["ballistic", "melee", "standard"], weaponsfull.weapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Gunner":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["limited", "standard", "ballistic", "projectile"], weaponsfull.weapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons); 
                    break;
                case "Modemaster":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["standard"], weaponsfull.weapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Scientist":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["electic", "explosivies"], weaponsfull.weapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Scout":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["silent"], weaponsfull.weapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Warrior":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["limited", "melee"], weaponsfull.weapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
            }
            
            
        }
        public void Getsup()
        {
            var se = getdata.LoadJson<SupportEquipRootTF>("SupEquipCore.json", "TransformersJsons"); // figure out how to shift list into observable collection maybe just shift the type in root
            foreach( var item in se.SupportEquipment)
            {
                if (!item.prereq.Contains("Extra")){
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
                    TrainedArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard"], ArmorEquipment.Armors);
                    ArmorUpgrades = GetNonTrainedList<Upgrades>(TrainedArmorUpgrades, ArmorEquipment.Armors);
                    break;
                case "Field Commander":
                    TrainedArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard", "limited"], ArmorEquipment.Armors);
                    ArmorUpgrades = GetNonTrainedList<Upgrades>(TrainedArmorUpgrades, ArmorEquipment.Armors);
                    break;
                case "Gunner":
                    TrainedArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard", "limited"], ArmorEquipment.Armors);
                    ArmorUpgrades = GetNonTrainedList<Upgrades>(TrainedArmorUpgrades, ArmorEquipment.Armors);
                    break;
                case "Modemaster":
                    TrainedArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard"], ArmorEquipment.Armors);
                    ArmorUpgrades = GetNonTrainedList<Upgrades>(TrainedArmorUpgrades, ArmorEquipment.Armors);
                    break;
                case "Scientist":
                    TrainedArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard", "limited"], ArmorEquipment.Armors);
                    ArmorUpgrades = GetNonTrainedList<Upgrades>(TrainedArmorUpgrades, ArmorEquipment.Armors);
                    break;
                case "Scout":
                    TrainedArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard", "limited"], ArmorEquipment.Armors);
                    ArmorUpgrades = GetNonTrainedList<Upgrades>(TrainedArmorUpgrades, ArmorEquipment.Armors);
                    break;
                case "Warrior":
                    TrainedArmorUpgrades = GetItemsForObserablecollection<Upgrades>(["Standard","limited", " Restricted"], ArmorEquipment.Armors);
                    ArmorUpgrades = GetNonTrainedList<Upgrades>(TrainedArmorUpgrades, ArmorEquipment.Armors);
                    break;
            }
        }


        public void GetKits()
        {
            var Kts = getdata.LoadJson<KitRootTF>("KitCore.json", "TransformersJsons");
            foreach (var k in Kits)
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

        public ObservableCollection<T> GetNonTrainedList<T>(ObservableCollection<T> trainedItems, List<T> root) where T : GearTF
        {
            var result = new ObservableCollection<T>();
            foreach(T item in root)
            {
                if (!trainedItems.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }



        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
