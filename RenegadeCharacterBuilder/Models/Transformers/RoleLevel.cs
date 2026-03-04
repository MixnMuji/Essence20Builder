using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Animation;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    internal class RoleLevel
    {
        public int strenghtBonus { get; set; }
        public int speedBonus { get; set; }
        public int smartsBonus { get; set; }
        public int socialBonus { get; set; }

        public List<string> perks { get; set; }

        public RoleLevel(List<string> perks, int strenghtBonus = 0, int speedBonus=0, int smartsBonus = 0, int socialBonus = 0)
        {

        }
    }
        
}
