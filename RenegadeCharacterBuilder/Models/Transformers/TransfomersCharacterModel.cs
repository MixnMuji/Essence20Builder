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
    internal class TransfomersCharacterModel
    {
        public string Name { get; set; }
        public string Pronouns { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Role { get; set; }
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

        public TransfomersCharacterModel(  string? Name, string? Pronouns, string Origin, string Role, int Level,
                                    Alliegence faction, List<string>? Languages, List<string> Influences, List<string> hangups,
                                    int health, int strenght, int speed, int smarts, int social)
        
                // redo the constructor to make it so that the optional ones are at the end and have default values;
        {
            this.Name = Name;
            this.Pronouns = Pronouns;
            this.Origin = Origin;
            this.Role = Role;
            this.Level = Level;
            Faction = faction;
            this.Languages = Languages;
            this.Influences = Influences;
            Hang_Ups = hangups;
            this.health = health;
            this.strenght = strenght;
            this.speed = speed;
            // put dirvied stats between stats like this
            this.smarts = smarts;
            this.social = social;

                //after this statments put logic for derived stats
        
        }
    }

   
   public enum Alliegence
    {
        Autobot = 0,
        Descepticon = 1

    }
}
