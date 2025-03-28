using System.Text;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator;

public static class TikzColormapUtility
{
    #region Colormap

    internal static string FormatColormap(Colormap colormap)
    {
        switch (colormap.Type)
        {
            case Colormaps.Bone:
                return "colormap/hot2";
            case Colormaps.Cool:
                return "colormap/cool";
            case Colormaps.Gray:
                return "colormap/blackwhite";
            case Colormaps.Hot:
                return "colormap/hot";
            case Colormaps.ILNumerics:
                return "colormap/viridis";
            case Colormaps.Jet:
                return "colormap/jet";
            case Colormaps.Pink:
                return "colormap/violet";
            case Colormaps.Spring:
                return "colormap/greenyellow";
            case Colormaps.Custom:
            default:
                return CustomColormap(colormap);
        }
    }

    private static string CustomColormap(Colormap colormap)
    {
        var builder = new StringBuilder("colormap={custom}{");
        for (var i = 0; i < colormap.Length; i++)
        {
            var position = (int) (100.0 * colormap.Data.GetValue(i, 0));
            var red = (int) (255.0 * colormap.Data.GetValue(i, 1));
            var green = (int) (255.0 * colormap.Data.GetValue(i, 2));
            var blue = (int) (255.0 * colormap.Data.GetValue(i, 3));

            builder.Append($"{(i == 0 ? "" : " ")}rgb255({position}cm)=({red},{green},{blue})");
        }
        builder.Append("}");

        return builder.ToString();
    }

    #endregion
}