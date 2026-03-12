using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    internal class Role
    {
        public string RoleName { get; set; }
        public List<RoleLevel> levels { get; set; }

        public class RoleLevel
        {
            public int Level { get; set; }
            public string perks { get; set; }

            public int? SpeedBoost { get; set; }
            public int? SmartsBoost { get; set; }
            public int? STrengthBoost { get; set; }
            public int? SocialBoost { get; set; }
        }
    }
}