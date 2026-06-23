using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace RenegadeCharacterBuilder.GlobalMethods
{
    public class DieConverter : IValueConverter

        {
            private static readonly Dictionary<int, string> intToDie = new Dictionary<int, string>()
            {
                {1,"d2" },
                {2, "d4" },
                {3, "d6" },
                {4, "d8" },
                {5, "d10" },
                {6, "d12"}
            };

            public object Convert(object value, Type targetType, object perameter, CultureInfo culture)
            {
                if (value is int score && intToDie.TryGetValue(score, out var intoDie))
                    return intoDie;

                return "-";

            }
            public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
            {
                return Binding.DoNothing;
            }

        }
}



