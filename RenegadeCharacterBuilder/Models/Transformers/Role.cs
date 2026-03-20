using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    public class Role
    {
        public string Name { get; set; }
        public string CyberTronianPerk { get; set }
        public string? Focus { get; set; }
        
        public TrainingTF Training { get; set; }
        
        public QualificationsTF Qualifications { get; set; }
        
        public List<LevelTF> Levels { get; set; }

        

    }
}