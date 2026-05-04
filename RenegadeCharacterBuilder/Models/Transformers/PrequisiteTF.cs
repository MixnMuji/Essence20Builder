using System;
using System.Collections.Generic;
using System.Text;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class PrequisiteTF
    {
        public List<int?> statRequirement { get; set; }
        public int? levelRequirement { get; set; }

        //basically our checker if characters can do a given stat

        public bool CheckIfMet(int targetNumber, List<int?> statsTocheck, int? levelReq )
        {
            if(statsTocheck != null)
            {
                foreach(int stat in statsTocheck)
                {
                    if (stat >= targetNumber)
                        return true;
                }
            }
            if (levelReq != null && levelReq >= targetNumber)
                return true;

            return false;
        }

    }
}
