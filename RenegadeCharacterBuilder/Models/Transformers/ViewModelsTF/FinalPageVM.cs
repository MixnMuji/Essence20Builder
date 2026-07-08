using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using RenegadeCharacterBuilder.CharacterModels.TransfomersCompaions;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;
using RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class FinalPageVM: INotifyPropertyChanged
    {
        //what we need
        // all the character Information

        // ObservableCollection<string> viewsForpage { get; set; } make obejct to show pages
        public event PropertyChangedEventHandler PropertyChanged;
        public FinalpageViewHelper character { get; set; } //charfluff and origin
        private bool showDataSet1 = true;
        public bool ShowDataSet1
        {
            get => showDataSet1;
            set
            {
                showDataSet1 = value;
                NotifyPropertyChanged(nameof(ShowDataSet1));
            }
        }
        public bool showDataSet2  = false;
        public bool ShowDataSet2
        {
            get => showDataSet2;
            set
            {
                showDataSet1 = value;
                NotifyPropertyChanged(nameof(ShowDataSet2));
            }
        }
        public bool showDataSet3  = false;
        public bool ShowDataSet3
        {
            get => showDataSet1;
            set
            {
                showDataSet1 = value;
                NotifyPropertyChanged(nameof(ShowDataSet3));
            }
        }

        private int currentIndex;
        public int CurrentIndex
            {
            get => currentIndex;
            set { currentIndex = value;
                NotifyPropertyChanged(nameof(CurrentIndex));
            }
        }
        
        public FinalPageVM()
        {
            
            CurrentIndex = 0;
        }

        public void Nextpage()
        {
            if(CurrentIndex == 0)
            {
                CurrentIndex += 1;
                setVisibity(CurrentIndex);
            }
            if(CurrentIndex == 1)
            {
                CurrentIndex += 1;
                setVisibity(CurrentIndex);

            }
            if(CurrentIndex == 2)
            {
                currentIndex = 1;
                setVisibity(CurrentIndex);
            }
        }
        public void PreviousPage()
        {
            if (CurrentIndex == 0)
            {
                CurrentIndex = 2;
                setVisibity(CurrentIndex);


            }
            if (currentIndex == 1)
            {
                CurrentIndex -= 1;
                setVisibity(CurrentIndex);


            }
            if (currentIndex == 2)
            {
                CurrentIndex -= 1;
                setVisibity(CurrentIndex);
            }
        }
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public  void setVisibity(int currentIndex)
        {
            switch (currentIndex)
            {
                case 0:
                    ShowDataSet1 = true;
                    ShowDataSet2 = false;
                    ShowDataSet3 = false;
                    return;
                case 1:
                    ShowDataSet1 = false;
                    ShowDataSet2 = true;
                    ShowDataSet3 = false;
                    return;
                case 2:
                    ShowDataSet1 = false;
                    ShowDataSet2 = false;
                    ShowDataSet3 = true;
                    return;
            }
        }
    }
}
