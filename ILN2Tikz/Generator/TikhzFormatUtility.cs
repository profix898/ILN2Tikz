using System;
using System.Drawing;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator
{
    public static class TikzFormatUtility
    {
        #region Line

        internal static string FormatLine(Globals globals, Color color, DashStyle style, float width)
        {
            return FormattableString.Invariant($"color={globals.Colors.GetColorName(color)},{FormatDashStyle(style)},line width={width:F1}pt");
        }

        internal static string FormatLine(Globals globals, Color color, DashStyle style, int width)
        {
            return FormattableString.Invariant($"color={globals.Colors.GetColorName(color)},{FormatDashStyle(style)},line width={width}pt");
        }

        internal static string FormatDashStyle(DashStyle style)
        {
            switch (style)
            {
                case DashStyle.Solid:
                    return "solid";
                case DashStyle.Dashed:
                    return "dashed";
                case DashStyle.PointDash:
                    return "densely dashed";
                case DashStyle.Dotted:
                    return "dotted";
                default:
                    return "densely dotted"; // Fallback style
            }
        }

        #endregion

        #region Marker

        internal static string FormatMarker(Globals globals, Color color, MarkerStyle style, int size)
        {
            if (style == MarkerStyle.None)
                return "";

            return $"mark={FormatMarkerStyle(style)},mark size={size},mark options={{fill={globals.Colors.GetColorName(color)}}}";
        }

        internal static string FormatMarkerStyle(MarkerStyle style)
        {
            switch (style)
            {
                case MarkerStyle.Dot:
                    return "*";
                case MarkerStyle.Circle:
                    return "o";
                case MarkerStyle.Diamond:
                    return "diamond*";
                case MarkerStyle.Square:
                    return "square*";
                case MarkerStyle.Rectangle:
                    return "square*";
                case MarkerStyle.TriangleUp:
                    return "triangle*";
                case MarkerStyle.TriangleDown:
                    return "triangle*";
                case MarkerStyle.TriangleLeft:
                    return "pentagon*";
                case MarkerStyle.TriangleRight:
                    return "pentagon*";
                case MarkerStyle.Plus:
                    return "oplus*"; //return "+";
                case MarkerStyle.Cross:
                    return "otimes*"; //return "x";
                case MarkerStyle.None:
                    return "";
                default:
                    return "plus";
            }
        }

        #endregion
    }
}