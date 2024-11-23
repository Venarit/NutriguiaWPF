﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace UiNutriguia.Helpers;

public class TimeRangeConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 2 && values[0] is DateTime startDateTime && values[1] is DateTime endDateTime)
        {
            return $"{startDateTime:HH:mm} - {endDateTime:HH:mm}";       
        }
        return string.Empty;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) 
    { 
        throw new NotImplementedException(); 
    }
}

