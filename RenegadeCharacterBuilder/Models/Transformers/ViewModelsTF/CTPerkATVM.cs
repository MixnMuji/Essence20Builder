using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using RenegadeCharacterBuilder.GlobalMethods;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class CTPerkATVM: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<CTHelperVM> RolesWithPerks { get; set; } = new();
        public int limit = TFCharacterSession.CurrentTransfomer.CurrentLevel / 2;
        public ObservableCollection<Perk> PerksBasedoffCurrentRole { get; set; } = new();
        

        private CTHelperVM _selectedRole;
        public CTHelperVM SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                UpdateListofPerks();
                NotifyPropertyChanged(nameof(SelectedRole));
            }
        }

        private Perk _selectedPerk;
        public Perk SelectedPerk
        {
            get => _selectedPerk;
            set
            {
                _selectedPerk = value;
                NotifyPropertyChanged(nameof(SelectedPerk));
            }
        }



        public CTPerkATVM()
        {
            GetRolesAndFilter();
        }

        private void GetRolesAndFilter()
        {
            Roles filterout = TFCharacterSession.CurrentTransfomer.Role;
         
            var calldata = new GlobalCall();
            var firstList = calldata.LoadJson<TFRolesRoot>("Roles.json","TransformersJsons");
           
            var RolesToPullFrom = firstList.Roles.Where(x => x.Name != filterout.Name).ToList();
            
            foreach (Roles role in RolesToPullFrom)
            {
                RolesWithPerks.Add(new CTHelperVM
                {
                    RoleName = role.Name,
                    legalPerks = new ObservableCollection<Perk>
                    (
                        role.Levels.Where(l => l.Level <= limit)
                        .Where(l => l.Perk != null)
                        .SelectMany(l => l.Perk)
                    )

                });
            }
        
        }
        private void UpdateListofPerks()
        {
            PerksBasedoffCurrentRole.Clear();
            foreach (Perk perk in SelectedRole.legalPerks)
            {
                PerksBasedoffCurrentRole.Add(perk);
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
