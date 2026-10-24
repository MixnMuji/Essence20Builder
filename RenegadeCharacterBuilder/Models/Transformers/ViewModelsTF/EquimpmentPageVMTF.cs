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
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
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
        public string[] EquipmentTypeView { get; } =
[
    "Armor",
    "Kits",
    "Weapons",
    "Support Equipment"
];

        private int _supportGearPointBank = 0; // this is false
        public int SupportGearPointBank
        {
            get => _supportGearPointBank;
            set
            {
                _supportGearPointBank = value;
                NotifyPropertyChanged();
            }
        }
        private IEnumerable<object> currentViewdList;
        public IEnumerable<Object> CurrentViewedList
        {
            get => currentViewdList;
            set
            {
                currentViewdList = value;
                NotifyPropertyChanged();
            }
        }   // generic for deciding current view
        
        public ObservableCollection<Upgrades> TrainedArmorUpgrades { get; set; }
        public ObservableCollection<WeaponTF> TrainedWeapons { get; set; }
        public ObservableCollection<Upgrades> ArmorUpgrades { get; set; }
        public ObservableCollection<Upgrades> Kits { get; set; }
        public ObservableCollection<WeaponTF> Weapons { get; set; }

        public ObservableCollection<WeaponTF> RefferenceForTrianedWeapons { get; set; }
        public ObservableCollection<Upgrades> RefferencedForTrainedAmmor { get; set; }

        public ObservableCollection<SupportEqupmentTF> SupportEquipment { get; set; } = new();

      
        private int _requistionPoints { get; set; } // have method on page that gets user imput and sets requistion points
        public int requistionPoints
        {
            get => _requistionPoints;
            set
            {
                _requistionPoints = value;
                NotifyPropertyChanged(nameof(requistionPoints));
            }
        }
        public Upgrades TakenArmorUpgrade { get; set; }
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

        private EquipmentTab _currentTab;
        public EquipmentTab CurrentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                NotifyPropertyChanged();
                UpdateCurrentList();
            }
        }
        private string chosenListTovView { get; set; }
        public string ChosenListToView
        {
            get => chosenListTovView;
            set
            {
                chosenListTovView = value;
                NotifyPropertyChanged(nameof(ChosenListToView));
                UpdateCurrentList();
            }
        }
        public EquimpmentPageVMTF()
        {
            getdata = new GlobalCall();

            TrainedWeapons = new ObservableCollection<WeaponTF>();
            TrainedArmorUpgrades = new ObservableCollection<Upgrades>();

            RefferencedForTrainedAmmor = new ObservableCollection<Upgrades>();
            RefferenceForTrianedWeapons = new ObservableCollection<WeaponTF>();

            Weapons = new ObservableCollection<WeaponTF>();
            ArmorUpgrades = new ObservableCollection<Upgrades>();
            SupportEquipment = new ObservableCollection<SupportEqupmentTF>();
            Kits = new ObservableCollection<Upgrades>();
            Dictionary<string, ObservableCollection<GearTF>> GearpairsTrained = new Dictionary<string, ObservableCollection<GearTF>>();
            Dictionary<string, ObservableCollection<GearTF>> GearpairsUnTrained = new Dictionary<string, ObservableCollection<GearTF>>();

            TakenWeapons = new ObservableCollection<WeaponTF>();
            TakenArmorUpgrade = new Upgrades();
            TakenKits = new ObservableCollection<Upgrades>();
            TakenSupportEquipment = new ObservableCollection<SupportEqupmentTF>();

            RoleName = TFCharacterSession.CurrentTransfomer.Role.Name;

            HardpointLimit = TFCharacterSession.CurrentTransfomer.Origns[0].AltMode.FirePoints;
            
          GetArmor();
          GetWeapons();
          RainMakerCheck();
          SupportFrameCheck();
          GetKits();
          Getsup();


            //give page a tab to show taken equipment
            //need method to remove equipment and add them back to list
            // need method to add equipment to taken list and vice versa for all items

            //need to deserialize all 4 lists
        }
      
       
       public bool checkIfAdditonISpossible(object slection)// plug into pages call
        {
            switch (slection)
            {
                case WeaponTF weapon:
                    if (TrainedWeapons.Contains(weapon))
                    {
                        if (HardpointLimit - 1 < 0) // if they're trained requistion points don't matter
                        {
                            return false;
                        }
                        else return true;

                    }
                    else { 
                    if (HardpointLimit - 1 < 0 || requistionPoints - 1 < 0)
                    {
                        return false;
                    }
                    else return true;
                    }
                    break;
                case Upgrades upgrade:  
                    if (TrainedArmorUpgrades.Contains(upgrade)) // roles can't be trained in kits so we just need to see if they qualify for the armor otherwise requsition maters
                    {
                        return true;
                    }
                    if(requistionPoints -1 < 0)
                    {
                        return false;
                    }
                    break;
                case SupportEqupmentTF se:
                    if (HardpointLimit - 1 < 0 || requistionPoints - 1 < 0)
                    {
                        return false;
                    }
                    else return true;
                    break;
                    
            }
            return true;
            
        }
        public void AddEquuipment(object selection)
        {
            if (requistionPoints == 0)
            {
                MessageBox.Show("No more Requestion Ponints to spend");
                return;
            }
          
                switch (selection)
                {
                    case WeaponTF weapon:
                    if (HardpointLimit == 0)
                    {
                        MessageBox.Show("No more Hardpoints to hold weapons");
                        return;
                    }
                    TakenWeapons.Add(weapon);
                        if (TrainedWeapons.Contains(weapon))
                        {
                            TrainedWeapons.Remove(weapon);
                            HardpointLimit -=1;
                        }
                        else
                        {
                            Weapons.Remove(weapon);
                            requistionPoints -= 1;
                            HardpointLimit -= 1;
                        }
                        break;

                    case Upgrades upgrade:
                        if (Kits.Contains(upgrade))
                        {
                            TakenKits.Add(upgrade);
                            Kits.Remove(upgrade);
                            requistionPoints -= 1;
                        }
                        if (ArmorUpgrades.Contains(upgrade))
                        {
                            if (TakenArmorUpgrade.Name == null)
                            {
                                TakenArmorUpgrade = (upgrade);
                                ArmorUpgrades.Remove(upgrade);
                                requistionPoints -= 1;
                                return;
                             }
                             else{
                            MessageBox.Show("Got to remove item for armor");
                               return;
                        }
                            
                            
                        }
                        if (TrainedArmorUpgrades.Contains(upgrade))
                        {
                            if (TakenArmorUpgrade != null)
                            {
                                return;
                            }
                            TakenArmorUpgrade = upgrade;
                            TrainedArmorUpgrades.Remove(upgrade);
                            
                        }
                        break;

                    case SupportEqupmentTF Se:
                        if (SupportEquipment.Contains(Se))
                        {
                            if (Se.name == "Extra Crew Capacity")
                            {
                                var addothetraits = getdata.LoadJson<SupportEquipRootTF>("SupEquipCore.json", "TransformersJsons");
                                foreach (var item in addothetraits.SupportEquipment)
                                {
                                    if (item.prereq == "Extra Crew capacity")
                                    {
                                        SupportEquipment.Add(item);
                                    }
                                }

                            }
                            TakenSupportEquipment.Add(Se);
                            SupportEquipment.Remove(Se);
                            if(SupportGearPointBank != 0 && TFCharacterSession.CurrentTransfomer.Origns[0].AltMode.OrignName == "Support")
                            {
                                SupportGearPointBank -= 1;
                            }
                            else 
                            {
                                HardpointLimit -= 1;
                            requistionPoints -= 1;
                            }
                        }
                        break;
                }
            
        }
        public void removeitem(object selection)
        {
            switch (selection)
            {
                case WeaponTF weapon:
                    if (TakenWeapons.Contains(weapon) && RefferenceForTrianedWeapons.Contains(weapon))
                    {
                        TakenWeapons.Remove(weapon);
                        HardpointLimit += 1;
                        // need method to see if item was trained or not.
                    }
                    if (TakenWeapons.Contains(weapon))
                    {
                        TakenWeapons.Remove(weapon);
                        HardpointLimit += 1;
                        requistionPoints += 1;
                    }
                    break;
                case Upgrades upgrade:
                    if (Kits.Contains(upgrade))
                    {
                        TakenKits.Remove(upgrade);
                        Kits.Add(upgrade);
                        requistionPoints += 1;
                    }
                    if (ArmorUpgrades.Contains(upgrade))
                    {

                        TakenArmorUpgrade = null;
                        ArmorUpgrades.Add(upgrade);
                        requistionPoints += 1;
                        
                    }
                   
                    break;

                case SupportEqupmentTF Se:
                    if (SupportEquipment.Contains(Se))
                    {
                        if (Se.name == "Extra Crew Capacity")
                        {
                            var addothetraits = getdata.LoadJson<SupportEquipRootTF>("SupEquipCore.json", "TransformersJsons");
                            foreach (var item in addothetraits.SupportEquipment)
                            {
                                if (item.prereq == "Extra Crew capacity")
                                {
                                    SupportEquipment.Add(item);
                                }
                            }

                        }
                        TakenSupportEquipment.Add(Se);
                        SupportEquipment.Remove(Se);
                        if (SupportGearPointBank >= 2 && TFCharacterSession.CurrentTransfomer.Origns[0].AltMode.OrignName == "Support") // handles support origin
                        {
                            SupportGearPointBank += 1;
                        }
                        else
                        {
                            HardpointLimit += 1;
                            requistionPoints += 1;
                        }
                        
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
                    RefferenceForTrianedWeapons = new ObservableCollection<WeaponTF>(TrainedWeapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Field Commander":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["ballistic", "melee", "standard"], weaponsfull.weapons);
                    RefferenceForTrianedWeapons = new ObservableCollection<WeaponTF>(TrainedWeapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Gunner":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["limited", "standard", "ballistic", "projectile"], weaponsfull.weapons);
                    RefferenceForTrianedWeapons = new ObservableCollection<WeaponTF>(TrainedWeapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons); 
                    break;
                case "Mode Master":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["standard"], weaponsfull.weapons);
                    RefferenceForTrianedWeapons = new ObservableCollection<WeaponTF>(TrainedWeapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Scientist":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["electic", "explosivies"], weaponsfull.weapons);
                    RefferenceForTrianedWeapons = new ObservableCollection<WeaponTF>(TrainedWeapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Scout":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["silent"], weaponsfull.weapons);
                    RefferenceForTrianedWeapons = new ObservableCollection<WeaponTF>(TrainedWeapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
                case "Warrior":
                    TrainedWeapons = GetItemsForObserablecollection<WeaponTF>(["limited", "melee"], weaponsfull.weapons);
                    RefferenceForTrianedWeapons = new ObservableCollection<WeaponTF>(TrainedWeapons);
                    Weapons = GetNonTrainedList<WeaponTF>(TrainedWeapons, weaponsfull.weapons);
                    break;
            }
            
            
        }
        public void Getsup()
        {
            var se = getdata.LoadJson<SupportEquipRootTF>("SupEquipCore.json", "TransformersJsons"); // figure out how to shift list into observable collection maybe just shift the type in root
            foreach( var item in se.SupportEquipment)
            {
                if (item.prereq==null ||!item.prereq.Contains("Extra")){
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
                case "Mode Master":
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
            foreach (var k in Kts.Kits)
            {
                Kits.Add(k);
            }

        }


        public void RainMakerCheck()
        {
            var weaponsfull = getdata.LoadJson<WeaponRoot>("WeaponsCore.json", "TransformersJsons");
            if (TFCharacterSession.CurrentTransfomer.Origns[0].Name == "Rainmaker")
            {
                HardpointLimit += 1;
                if (TFCharacterSession.CurrentTransfomer.CurrentLevel >= 14)
                {
                    var transition = GetItemsForObserablecollection<WeaponTF>(["Artillery Cannon", "Missile"], weaponsfull.weapons);
                    foreach(WeaponTF w in transition)
                    {
                        TrainedWeapons.Add(w);
                    }
                    var target = Weapons.FirstOrDefault(x => x.Name == "Artilery Cannon");
                    Weapons.Remove(target);
                    target = Weapons.FirstOrDefault(x => x.Name == "Missile");
                    Weapons.Remove(target);
                }
                if (TFCharacterSession.CurrentTransfomer.CurrentLevel >= 7)
                {

                    var transition = GetItemsForObserablecollection<WeaponTF>(["Heavy Machine Gun", "Rocket Launcher"], weaponsfull.weapons);
                    foreach (WeaponTF w in transition)
                    {
                        TrainedWeapons.Add(w);
                    }
                    var target = Weapons.FirstOrDefault(x => x.Name == "Heavy Machine Gun");
                    Weapons.Remove(target);
                    target = Weapons.FirstOrDefault(x => x.Name == "Rocket Launcher");
                    Weapons.Remove(target);

                }
            }
        }

        public void SupportFrameCheck()
        {
            if (TFCharacterSession.CurrentTransfomer.Origns[0].Name == "Support")
            {
                _supportGearPointBank += 2;
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

        public void UpdateCurrentList()
        {
            if (string.IsNullOrEmpty(chosenListTovView))
                return;


            if (string.IsNullOrEmpty(ChosenListToView))
                return;
            ObservableCollection<Upgrades> transition = new();
            transition.Add(TakenArmorUpgrade);
            CurrentViewedList = CurrentTab switch
            {
                EquipmentTab.Trained => ChosenListToView switch
                {
                    "Armor" => TrainedArmorUpgrades,
                    "Weapons" => TrainedWeapons,
                    "Kits" => [],
                    "Support Equipment" => [],
                    _ => []
                },

                EquipmentTab.Untrained => ChosenListToView switch
                {
                    "Armor" => ArmorUpgrades,
                    "Weapons" => Weapons,
                    "Kits" => Kits,
                    "Support Equipment" => SupportEquipment,
                    _ => []
                },

                EquipmentTab.Taken => ChosenListToView switch
                {
                     
                    "Armor" => transition,
                    "Weapons" => TakenWeapons,
                    "Kits" => TakenKits,
                    "Support Equipment" => TakenSupportEquipment,
                    _ => []
                },

                _ => []
            };

        }
            

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum EquipmentTab
        {
            Trained,
            Untrained,
            Taken
        }
    }
}
