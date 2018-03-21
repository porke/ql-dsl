using System.Windows.Media;

namespace Presentation.ViewModels
{
    internal class StyleViewModel
    {
        public static readonly StyleViewModel DefaultStyle = new StyleViewModel(11, 400, "Arial", Color.FromRgb(0, 0, 0));

        public StyleViewModel(int fontSize, int width, string fontFamily, Color foreground)
        {
            FontSize = fontSize;
            Width = width;
            FontFamily = fontFamily;
            Foreground = new SolidColorBrush(foreground);
        }

        public int FontSize { get; }
        public int Width { get; }
        public string FontFamily { get; }
        public SolidColorBrush Foreground { get; }
    }
}
