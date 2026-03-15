using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Numerics;
using System.Text;
using System.Windows.Media.Animation;

namespace RenegadeCharacterBuilder.Models.Transformers
{
    // these fields will be used to make a new object and the stats will mirror what's on the sheet as well
    public class TransfomersCharacterModel
    {
        public string Name { get; set; }
        public string Pronouns { get; set; }
        public TransformersOrign Orign { get; set; }
        public Role role { get; set; }
        public int Level { get; set; }
        public Alliegence Faction { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Influences { get; set; }
        public List<string> Hang_Ups { get; set; }
        public int health { get; set; }
        public int strenght { get; set; }
        public int speed { get; set; }
        public int smarts { get; set; }
        public int social { get; set; }
        //need object for attacks put them in a list

        public TransfomersCharacterModel()
        
                // redo the constructor to make it so that the optional ones are at the end and have default values;
        {
            
        }
    }

   
   public enum Alliegence
    {
        Autobot = 0,
        Descepticon = 1

    }
}
