using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.Roots;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class GeneralPerksVMTF : INotifyPropertyChanged
    {
        //need a view for pagination 
        //need property changed to do that
        //need General Perk bank


        public List<GeneralPerkTF> _qualifyingPerks { get; set; } = new(); // stores what data we use for pages.
        public int resultsPerPage { get; set; } = 4;
        public int totalPages => (int)Math.Ceiling((double)_qualifyingPerks.Count / resultsPerPage);

        public ICommand NextPage { get; set; }
        public ICommand PreviousPage { get; set; }


        private ObservableCollection<GeneralPerkTF> _currentPagePerks { get; set; } = new();

        public ObservableCollection<GeneralPerkTF> CurrentPagePerks
        {
            get => _currentPagePerks;
            set
            {
                _currentPagePerks = value;
                NotifyPropertyChanged(nameof(CurrentPagePerks));
            }
        }




        private int _generalPerksPointBank { get; set; } //this will show how many points we have to spend
        public int GeneralPerksPointBank
        {
            get => _generalPerksPointBank;
            set
            {
                if (_generalPerksPointBank != value)
                {
                    _generalPerksPointBank = value;
                    NotifyPropertyChanged(nameof(GeneralPerksPointBank));

                }
            }
        }



        // THIS IS TO MONITORY WHAT PAGE WE'RE ON
        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    NotifyPropertyChanged(nameof(CurrentIndex));
                    RefreshPage();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public GeneralPerksVMTF()
        {

             TFCharacterSession.CurrentTransfomer.GetGeneralPerkPonts();
            _generalPerksPointBank = TFCharacterSession.CurrentTransfomer.ActualPerksToSpend;


            NextPage = new RelayCommand<Object>(NextPageOfResults);
            PreviousPage = new RelayCommand<Object>(PreviousPageofResults);


        }

        public void RefreshPage()
        {
            var pageItems = _qualifyingPerks
                .Skip(CurrentIndex * resultsPerPage)
                .Take(resultsPerPage);

            CurrentPagePerks = new ObservableCollection<GeneralPerkTF>(pageItems);

        }
        public void GetApplicablePerks(int charLevel, List<SkillTF> CharacterSkillsVals, List<ScoreTF> ScoreVals)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", "TransformersJsons", "GeneralPerksTF.json");
            string json = File.ReadAllText(path);
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };
            var AllGeneralPerks = JsonSerializer.Deserialize<TFGeneralPerkRoot>(json,options); //check the name
            
          
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
            _qualifyingPerks = filtered.ToList();
            CurrentIndex = 0;
            RefreshPage();


        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public void NextPageOfResults(object obj)
        {
            if (CurrentIndex < totalPages - 1)
            {
                CurrentIndex++;
            }


        }

        public void PreviousPageofResults(object obj)
        {
            if (CurrentIndex > 0)
            {
                CurrentIndex--;
            }


        }
        public bool notatePerk(object obj)
        {
            if (GeneralPerksPointBank > 0)
            {
                GeneralPerksPointBank--;
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}

