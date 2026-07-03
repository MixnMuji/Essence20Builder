using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using RenegadeCharacterBuilder.Models.Transformers.ModelsForState;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF
{
    public class SCVM : INotifyPropertyChanged
    {
   
            public event PropertyChangedEventHandler PropertyChanged;
          
            public List<Altmode> choices { get; set; } = TFCharacterSession.CurrentTransfomer.Altmodes;

            public ObservableCollection<string> afterSizeLogic { get; set; } = new();

            private string chosenSize;

            public string ChosenSize
            {
                get => chosenSize;
                set
                {
                    chosenSize = value;
                    OnPropertyChanged(nameof(ChosenSize));
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
               afterSizeLogic.Clear();

              afterSizeLogic.Add("Common");
              afterSizeLogic.Add("Long");
              afterSizeLogic.Add("Extended");
            // set the collections equal because if we keep sizes imutable we're fine

            // we need the transfoerms botsize index
            // then we can say to remove inxe of observable collection

            string[] botSizes = ["Common", "Large", "Huge"];

                var targetOrign = TFCharacterSession.CurrentTransfomer.Origns.FirstOrDefault();

                string sizeToRemove = targetOrign.BotMode.Size;

                int removeIndex = Array.IndexOf(botSizes, sizeToRemove); // returns 0

            //here it turns to -1 why?
            
            MessageBox.Show(removeIndex.ToString()); //here it turns to -1 why?
            afterSizeLogic.RemoveAt(removeIndex); // since they share the same progresion this will remove the size of the same index, common and common are at index 0



            }

            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        
        }
    }


