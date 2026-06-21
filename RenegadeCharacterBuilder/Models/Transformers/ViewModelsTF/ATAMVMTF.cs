using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;


namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class ATAMVMTF : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public List<TransformersOrign> OriginsTogetModesFrom { get; set; } = new();
        // public string[] movetypes = ["Ground", "Aquatic", "Aerial"]; old version arrays are less mutiable no reason to use
        public ObservableCollection<string> mtypes { get; set; } =
     new ObservableCollection<string>
     {
        "Ground",
        "Aquatic",
        "Aerial"
     };

        private TransformersOrign _selectedAltMode;

        public TransformersOrign SelectedAltMode
        {
            get => _selectedAltMode;
            set
            {
                _selectedAltMode = value;
                updateApplicableMoveTypes();
                OnPropertyChanged(nameof(SelectedAltMode));
            }
        }

        public ObservableCollection<string> aplicableMoveTypes { get; set; } = new(); // where we store the movetypes



        
        //add an Ischecked property to origins bool


       

        public ATAMVMTF()
        {
            OriginsTogetModesFrom = TFCharacterSession.CurrentTransfomer.Origns;
            aplicableMoveTypes = new ObservableCollection<string>();
         

        }

        public void updateApplicableMoveTypes()
        {
            aplicableMoveTypes.Clear();
            if (SelectedAltMode == null)
                return;
            foreach (var movetype in mtypes.Where(s=> s != _selectedAltMode.AltMode.Type))
            {
                aplicableMoveTypes.Add(movetype);
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
