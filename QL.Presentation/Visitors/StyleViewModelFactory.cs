using Presentation.ViewModels;
using Presentation.Visitors.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Presentation.Visitors
{
    internal static class StyleViewModelFactory
    {
        private static readonly StyleViewModel Defaults = new StyleViewModel(13, 300, "Segoe UI", Color.FromRgb(0, 0, 0));

        public static StyleViewModel CreateViewModel(IReadOnlyList<PropertyData> properties)
        {
            return new StyleViewModel(ExtractFontSize(properties), ExtractWidth(properties), ExtractFont(properties), ExtractColor(properties));
        }

        private static int ExtractWidth(IReadOnlyList<PropertyData> properties)
        {
            string width = properties.FirstOrDefault(x => x.Name == "width")?.Value;
            return width != null
                ? int.Parse(width)
                : Defaults.Width;
        }

        private static int ExtractFontSize(IReadOnlyList<PropertyData> properties)
        {
            string width = properties.FirstOrDefault(x => x.Name == "fontsize")?.Value;
            return width != null
                ? int.Parse(width)
                : Defaults.FontSize;
        }

        private static Color ExtractColor(IReadOnlyList<PropertyData> properties)
        {
            string colorString = properties.FirstOrDefault(x => x.Name == "color")?.Value;
            return colorString != null
                ? (Color)ColorConverter.ConvertFromString(colorString)
                : Defaults.Foreground.Color;
        }

        private static string ExtractFont(IReadOnlyList<PropertyData> properties)
        {
            string fontString = properties.FirstOrDefault(x => x.Name == "font")?.Value;
            return fontString != null
                ? fontString
                : Defaults.FontFamily;
        }
    }
}
