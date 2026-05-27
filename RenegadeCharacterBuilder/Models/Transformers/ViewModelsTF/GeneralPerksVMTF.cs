using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class GeneralPerksVMTF
    {

        //make list here
        public GeneralPerksVMTF()
        {

        }
        public void GetApplicablePerks(int charLevel, List<SkillTF> CharacterSkillsVals, List<ScoreTF> ScoreVals)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "GeneralPerksTF.json");
            string json = File.ReadAllText(path);
            var AllGeneralPerks = JsonSerializer.Deserialize<TFGeneralPerkRoot>(json);
            var filtered = AllGeneralPerks.Gps.Where(x =>
            {
                if (x.requirment == null)
                    return true;
                if (x.requirment.levelRequirement > charLevel)
                {
                    return false;
                }
                if (x.requirment.ApplicableStats == null ||
                  x.requirment.ApplicableStats.Count == 0)
                {
                    return true;
                }
                bool meetsSkillRequirment = CharacterSkillsVals.Any(s =>
                x.requirment.ApplicableStats.Contains(s.Name) && s.SkillScore >= x.requirment.statRequirement);

                bool meetsScoreRequirment = ScoreVals.Any(s =>
                x.requirment.ApplicableStats.Contains(s.Name) && s.CurrentRank == x.requirment.statRequirement);
                return meetsSkillRequirment || meetsScoreRequirment;
            });
            var list = filtered.ToList();
            

        }
    }

    
}

/*
 stats that are checked for perks
"Might",
"Finesse",
"Targeting"
"Deception",
"Persuasion"
"Driving"
"Speed"
"Streetwise"
"Brawn"
"Smarts"
 */