using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class FocusPageVMTF: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<FocusTF> twoChoices { get; set; }
        public string CharactersRoleToGetSubclasses { get; set; }
        public ICommand DispalyNextSubclass { get; set; }
        public ICommand DisplayPreviousSubclass { get; set; }
       

        private FocusTF _currentSubclass;
        public FocusTF CurrentSubclass
        {
            get => _currentSubclass;
            set
            {
                _currentSubclass = value;
                OnPropertyChanged();
            }
        }

        private int _currentIndex;

        public int Currentindex
        {
            get => _currentIndex;
            set
            {
                if(twoChoices == null || twoChoices.Count == 0)
                {
                    return;
                }
                if(value < 0)
                {
                    _currentIndex = twoChoices.Count - 1;
                }else if(value >= twoChoices.Count)
                {
                    _currentIndex = 0;
                }
                else
                {
                    _currentIndex = value;
                }
                CurrentSubclass = twoChoices[_currentIndex];
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public FocusPageVMTF()
        {

            CharactersRoleToGetSubclasses = TFCharacterSession.CurrentTransfomer.Role.Name;

      

            if (string.IsNullOrWhiteSpace(CharactersRoleToGetSubclasses))
            {

                CharactersRoleToGetSubclasses = "Analyst";
            }

            DispalyNextSubclass = new RelayCommand<Object>(GetNextClass);
            DisplayPreviousSubclass = new RelayCommand<Object>(GetPreviousClass);

        }

        public void GetSubClass()
        {
            switch (CharactersRoleToGetSubclasses)
            {
                case "Analyst":
                    GetSubclassesFromJson("Manipulator", "Spec Ops");
                    break;

                case "FieldCommander":
                    GetSubclassesFromJson("Ambassador", "Strategist");
                    break;

                case "Gunner":
                    GetSubclassesFromJson("Gunslinger", "SharpShooter");
                    break;

                case "ModeMaster": //this needs its own method unfortunately
                    GetSubclassesFromJson("Microlnked", "Triple Changer");
                    break;

                case "Scientist":
                    GetSubclassesFromJson("Medical Officer", "Gadgeteer");
                    break;

                case "Scout":
                    GetSubclassesFromJson("Outrider", "Prowler");
                    break;
                    
                case "Warrior":
                    GetSubclassesFromJson("Wrecker", "Sentinel");
                    break;
            }
        }
        public void GetSubclassesFromJson(string option1, string option2)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "FocuesesTF.json");
            string json = File.ReadAllText(path);
            var AllSubclasses = JsonSerializer.Deserialize<TFFocusesRoot>(json);
            var filteredList = AllSubclasses.Focuses.Where(x => x.subclassName == option1 || x.subclassName == option2).ToList();
            twoChoices = new ObservableCollection<FocusTF>(filteredList);
            if (twoChoices.Count > 0)
            {
                Currentindex = 0;
            }
           
        }
       public void GetNextClass(object obj)
        {
            Currentindex++;
        }
        public void GetPreviousClass(object obj)
        {
            Currentindex--;
        }
    }

  }

