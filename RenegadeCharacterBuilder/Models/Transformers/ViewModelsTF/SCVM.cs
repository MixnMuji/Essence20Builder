using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class SCVM
    {
   
            public event PropertyChangedEventHandler PropertyChanged;
            public ObservableCollection<string> sizes { get; set; } = ["Common", "Large", "Extended"];
            public List<Altmode> choices { get; set; } = TFCharacterSession.CurrentTransfomer.Altmodes;

            public ObservableCollection<string> afterSizeLogic { get; set; } = new();

            private string chosenSize;

            public string ChosenSize
            {
                get => chosenSize;
                set
                {
                    chosenSize = value;
                    OnPropertyChanged(ChosenSize);
                }
            }

            private Altmode selection;

            public Altmode Selection
            {
                get => selection;
                set
                {
                    selection = value;
                    FindlegalSizes();
                    OnPropertyChanged(nameof(Selection));

                }
            }



            public void FindlegalSizes()
            {
                afterSizeLogic = sizes;
                // set the collections equal because if we keep sizes imutable we're fine

                // we need the transfoerms botsize index
                // then we can say to remove inxe of observable collection

                string[] botSizes = ["Common", "Long", "Huge"];

                var targetOrign = TFCharacterSession.CurrentTransfomer.Origns[0];

                string sizeToRemove = targetOrign.BotMode.Size;

                int removeIndex = Array.IndexOf(botSizes, sizeToRemove);

                afterSizeLogic.RemoveAt(removeIndex); // since they share the same progresion this will remove the size of the same index, common and common are at index 0



            }

            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        
        }
    }


