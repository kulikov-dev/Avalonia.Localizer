using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace Avalonia.Localizer.Core.Converters
{
    /// <summary>
    /// Converter to use enum descriptions in XAML. Support localization.
    /// </summary>
    public class EnumDescriptionConverter : IValueConverter
    {
        /// <inheritdoc/>
        object IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Enum enumValue)
            {
                throw new NotSupportedException("Value should be enum.");
            }

            return GetEnumDescription(enumValue);
        }

        /// <inheritdoc/>
        object IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return string.Empty;
        }

        /// <summary>
        /// Get enum description
        /// </summary>
        /// <param name="enumValue"> Enum value </param>
        /// <returns> Enum description </returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            var enumValueStr = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValueStr);

            if (fieldInfo == null)
            {
                return enumValueStr;
            }

            var attributes = fieldInfo.GetCustomAttributes(false);

            if (attributes.Length == 0)
            {
                return enumValueStr;
            }
            
            if (attributes.FirstOrDefault(item => item is DescriptionAttribute) is DescriptionAttribute descriptionAttribute)
            {
                return ProgramCore.Localizer?[descriptionAttribute.Description] ?? enumValueStr;
            }

            return enumValueStr;
        }
    }
}
