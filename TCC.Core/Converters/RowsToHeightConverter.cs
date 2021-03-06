﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace TCC.Converters
{
    public class RowsToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rows = (int)value;
            if (rows == 0) return double.NaN;
            return 19 * rows;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
