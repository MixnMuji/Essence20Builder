using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class ATAMVMTF
    {
       
        //we need the altmode list
        //we need the movetypes as a key
        //we need a display where it is the current move types that aren't in te current altmode

        public List<TransformersOrign> OriginsTogetModesFrom { get; set; } = new();
        // public string[] movetypes = ["Ground", "Aquatic", "Aerial"]; old version arrays are less mutiable no reason to use
        public List<string> mtypes = ["Ground", "Aquatic", "Aerial"];

        private List<string> aplicableMoveTypes { get; set; } = new(); // where we store the movetypes

        public ICommand GetListForLegalAltModes { get; set; }

        
        //add an Ischecked property to origins bool


       

        public ATAMVMTF()
        {
            OriginsTogetModesFrom = TFCharacterSession.CurrentTransfomer.Origns;
            GetListForLegalAltModes = new RelayCommand<List<string>>(GetApplicableNewMovmentModes);

        }

        public List<string> GetApplicableNewMovmentModes()
        {
            TransformersOrign CurrentSelection = OriginsTogetModesFrom.Single(o => o.isSelected == true);
            if(CurrentSelection == null)
            {
                return mtypes;
            }
            aplicableMoveTypes = mtypes.Where(m => m != CurrentSelection.AltMode.Type).ToList();
            return aplicableMoveTypes;
            
        }
        
    }
}
