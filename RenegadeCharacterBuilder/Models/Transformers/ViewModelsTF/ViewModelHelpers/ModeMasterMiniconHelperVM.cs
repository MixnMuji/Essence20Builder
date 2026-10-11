using System;
using System.Collections.Generic;
using System.Text;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers
{
    public class ModeMasterMiniconHelperVM
    {
        public List<ScoreTF> scoreValues { get; set; }

        public string MiniconName { get; set; }

        public string originalPurpose { get; set; }
        public ModeMasterMiniconHelperVM()
        {
            if (TFCharacterSession.CurrentTransfomer.companions.Count > 0)
            {
                scoreValues = TFCharacterSession.CurrentTransfomer.companions[0].fullScoreList;
                MiniconName = TFCharacterSession.CurrentTransfomer.companions[0].Name;
                originalPurpose = TFCharacterSession.CurrentTransfomer.companions[0].purpose.Name;
            }
            else
            {
                return;
            }
            
        }
    }
}
