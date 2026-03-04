using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    internal class Role
    {
        public string Name { get; set; }
        private Dictionary<int, RoleLevel> _levels;

        public Role(string name, Dictionary<int,RoleLevel> levels) 
        {
            this.Name = name;
            _levels = levels;        
        }
        public RoleLevel GetLevel(int level)
        {
            return _levels[level];
        }

    }
}
