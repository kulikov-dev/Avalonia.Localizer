using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.Localizer.Core.Converters
{
    internal class StringLocalizationConverter : IValueConverter
    {
        /// <inheritdoc/>
        object IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string str)
            {
                throw new NotSupportedException("Value should be enum.");
            }

            return ProgramCore.Localizer[str];
        }

        /// <inheritdoc/>
        object IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
