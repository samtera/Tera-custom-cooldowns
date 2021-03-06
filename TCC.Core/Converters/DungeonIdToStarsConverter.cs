﻿using System;
using System.Globalization;
using System.Windows.Data;
using TCC.Data;
using TCC.Data.Databases;

namespace TCC.Converters
{
    internal class DungeonIdToStarsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = (uint)value;
            var t = SessionManager.DungeonDatabase.DungeonDefs[id].Tier;
            switch (t)
            {
                case DungeonTier.Tier2:
                    return "2";
                case DungeonTier.Tier3:
                    return "3";
                case DungeonTier.Tier4:
                    return "4";
                case DungeonTier.Tier5:
                    return "5";
                default:
                    return "-";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
